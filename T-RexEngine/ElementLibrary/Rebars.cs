using System;
using System.Collections.Generic;
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

namespace T_RexEngine.ElementLibrary
{
    public class Rebars: ElementGroup
    {
        public Rebars(RebarGroup rebarGroup)
        {
            Mesh = rebarGroup.OriginRebarShape.RebarMesh;
            RebarGroup = rebarGroup;
            Material = rebarGroup.Material;
            ElementType = ElementType.Rebar;
        }

        public override List<IfcBuildingElement> ToBuildingElementIfc(IfcStore model)
        {
            throw new ArgumentException("Rebars should be converted to IfcReinforcingElement");
        }

        public override List<IfcReinforcingElement> ToReinforcingElementIfc(IfcStore model)
        {
            using (var transaction = model.BeginTransaction("Create Mesh Element"))
            {
                MeshFaceList faces = Mesh.Faces;
                MeshVertexList vertices = Mesh.Vertices;
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
                double nominalDiameter = RebarGroup.Diameter;
                double barLength = (int)Math.Round(RebarGroup.OriginRebarShape.RebarCurve.GetLength());

                foreach (var insertPlane in RebarGroup.RebarInsertPlanes)
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
        private Mesh Mesh { get; }
        private RebarGroup RebarGroup { get;}
    }
}