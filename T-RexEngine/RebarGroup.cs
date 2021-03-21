using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using T_RexEngine.Enums;
using Xbim.Ifc;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.MaterialResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.RepresentationResource;
using Xbim.Ifc4.StructuralElementsDomain;
using Xbim.Ifc4.TopologyResource;

namespace T_RexEngine
{
    public class RebarGroup : ElementGroup
    {
        private int _id;

        public RebarGroup(int id, RebarSpacing rebarSpacing)
        {
            Id = id;
            OriginRebarShape = rebarSpacing.OriginRebarShape;
            Count = rebarSpacing.Count;
            Volume = rebarSpacing.Volume;
            Weight = rebarSpacing.Weight;
            RebarGroupMesh = rebarSpacing.RebarGroupMesh;
            RebarGroupCurves = rebarSpacing.RebarGroupCurves;
            RebarInsertPlanes = rebarSpacing.RebarInsertPlanes;
            Diameter = rebarSpacing.OriginRebarShape.Props.Diameter;
            Material = rebarSpacing.OriginRebarShape.Props.Material;
            ElementType = ElementType.Rebar;
        }
        public RebarGroup(int id, List<RebarShape> rebarShapes)
        {
            Id = id;
            Count = rebarShapes.Count;
            RebarGroupMesh = new List<Mesh>();
            RebarGroupCurves = new List<Curve>();
            Volume = 0.0;
            Weight = 0.0;
            Diameter = rebarShapes[0].Props.Diameter;
            Material = rebarShapes[0].Props.Material;

            foreach (var rebarShape in rebarShapes)
            {
                if (Material.ToString() != rebarShape.Props.Material.ToString())
                {
                    throw new ArgumentException("You can't add bars with different materials to one group");
                }
                if (Diameter.ToString(CultureInfo.InvariantCulture) != rebarShape.Props.Diameter.ToString(CultureInfo.InvariantCulture))
                {
                    throw new ArgumentException("You can't add bars with different diameters to one group");
                }
                
                RebarGroupMesh.Add(rebarShape.RebarMesh);
                RebarGroupCurves.Add(rebarShape.RebarCurve);

                double currentRebarVolume = rebarShape.RebarCurve.GetLength() * Math.PI * Math.Pow(rebarShape.Props.Radius, 2.0);
                
                Volume += currentRebarVolume;
                Weight += currentRebarVolume * rebarShape.Props.Material.Density;
            }
        }
        public override string ToString()
        {
            return String.Format("Rebar Group{0}" +
                                 "Id: {1}{0}" +
                                 "Count: {2}",
                Environment.NewLine, Id, Count);
        }
        public int Id
        {
            get { return _id; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Id can't be < 0");
                }

                _id = value;
            }
        }
        public override List<IfcBuildingElement> ToBuildingElementIfc(IfcStore model)
        {
            throw new ArgumentException("Rebars should be converted to IfcReinforcingElement");
        }

        public override List<IfcReinforcingElement> ToReinforcingElementIfc(IfcStore model)
        {
            using (var transaction = model.BeginTransaction("Create Mesh Element"))
            {
                MeshFaceList faces = OriginRebarShape.RebarMesh.Faces;
                MeshVertexList vertices = OriginRebarShape.RebarMesh.Vertices;
                List<IfcCartesianPoint> ifcVertices = new List<IfcCartesianPoint>();

                foreach (var point in vertices)
                {
                    IfcCartesianPoint currentVertex = model.Instances.New<IfcCartesianPoint>();
                    currentVertex.SetXYZ(point.X, point.Y, point.Z);
                    ifcVertices.Add(currentVertex);
                }

                var faceSet = model.Instances.New<IfcConnectedFaceSet>();

                foreach (var meshFace in faces)
                {
                    List<IfcCartesianPoint> points = new List<IfcCartesianPoint>();

                    points.Add(ifcVertices[meshFace.A]);
                    points.Add(ifcVertices[meshFace.B]);
                    points.Add(ifcVertices[meshFace.C]);

                    if (meshFace.C != meshFace.D)
                    {
                        points.Add(ifcVertices[meshFace.D]);
                    }

                    var polyLoop = model.Instances.New<IfcPolyLoop>();
                    polyLoop.Polygon.AddRange(points);
                    var bound = model.Instances.New<IfcFaceOuterBound>();
                    bound.Bound = polyLoop;
                    var face = model.Instances.New<IfcFace>();
                    face.Bounds.Add(bound);

                    faceSet.CfsFaces.Add(face);
                }

                var faceBasedSurfaceModel = model.Instances.New<IfcFaceBasedSurfaceModel>();
                faceBasedSurfaceModel.FbsmFaces.Add(faceSet);

                // Create shape that holds geometry
                var shape = model.Instances.New<IfcShapeRepresentation>();
                var modelContext = model.Instances.OfType<IfcGeometricRepresentationContext>().FirstOrDefault();
                shape.ContextOfItems = modelContext;
                shape.RepresentationType = "Mesh";
                shape.RepresentationIdentifier = "Mesh";
                shape.Items.Add(faceBasedSurfaceModel);

                // Create material
                var material = model.Instances.New<IfcMaterial>();
                material.Category = Material.Name;
                material.Name = Material.Grade;
                var ifcRelAssociatesMaterial = model.Instances.New<IfcRelAssociatesMaterial>();
                ifcRelAssociatesMaterial.RelatingMaterial = material;

                // Create rebars
                var rebars = new List<IfcReinforcingElement>();

                // Rebar quantities
                double nominalDiameter = Diameter;
                double barLength = (int) Math.Round(OriginRebarShape.RebarCurve.GetLength());

                foreach (var insertPlane in RebarInsertPlanes)
                {
                    var rebar = model.Instances.New<IfcReinforcingBar>();
                    rebar.Name = "Rebar";
                    rebar.NominalDiameter = nominalDiameter;
                    rebar.BarLength = barLength;
                    rebar.SteelGrade = Material.Grade;

                    // Add geometry to footing
                    var representation = model.Instances.New<IfcProductDefinitionShape>();
                    representation.Representations.Add(shape);
                    rebar.Representation = representation;

                    // Place footing in model
                    var localPlacement = model.Instances.New<IfcLocalPlacement>();
                    var ax3D = model.Instances.New<IfcAxis2Placement3D>();

                    var location = model.Instances.New<IfcCartesianPoint>();
                    location.SetXYZ(insertPlane.OriginX, insertPlane.OriginY, insertPlane.OriginZ);
                    ax3D.Location = location;

                    ax3D.RefDirection = model.Instances.New<IfcDirection>();
                    ax3D.RefDirection.SetXYZ(insertPlane.XAxis.X, insertPlane.XAxis.Y, insertPlane.XAxis.Z);
                    ax3D.Axis = model.Instances.New<IfcDirection>();
                    ax3D.Axis.SetXYZ(insertPlane.ZAxis.X, insertPlane.ZAxis.Y, insertPlane.ZAxis.Z);
                    localPlacement.RelativePlacement = ax3D;
                    rebar.ObjectPlacement = localPlacement;


                    ifcRelAssociatesMaterial.RelatedObjects.Add(rebar);

                    rebars.Add(rebar);
                }

                transaction.Commit();

                return rebars;
            }
        }

        public List<Mesh> RebarGroupMesh { get; }
        public List<Curve> RebarGroupCurves { get; }
        public List<Plane> RebarInsertPlanes { get; }
        public int Count { get; }
        public double Volume { get; }
        public double Weight { get; }
        public RebarShape OriginRebarShape { get; }
        public double Diameter { get; }
    }
}
