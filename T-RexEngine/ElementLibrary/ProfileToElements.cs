﻿using System;
using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry;
using Xbim.Ifc;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.MaterialResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.ProfileResource;
using Xbim.Ifc4.RepresentationResource;
using Xbim.Ifc4.StructuralElementsDomain;
using Color = System.Drawing.Color;

namespace T_RexEngine.ElementLibrary
{
    public class ProfileToElements: ElementGroup
    {
        public ProfileToElements(string name, Profile elementProfile, List<Line> insertLines, double angle, Material material, int type)
        {
            Name = name;
            Material = material;
            Profile = elementProfile;
            ElementLines = insertLines;
            Amount = insertLines.Count;
            ElementType = IfcTools.IntToType(type);

            double surfaceArea = elementProfile.BoundarySurfaces[0].GetArea();
            Volume = insertLines.Sum(line => surfaceArea * line.Length);
            
            Mass = Volume * material.Density;
            SectionInsertPlanes = new List<Plane>();
            Breps = new List<Brep>();
            
            foreach (var line in insertLines)
            {
                Curve lineCurve = line.ToNurbsCurve();
                double[] divisionParameters = lineCurve.DivideByCount(1, true);
                Plane[] perpendicularPlanes = lineCurve.GetPerpendicularFrames(divisionParameters);
                Plane sectionInsertPlane = perpendicularPlanes[0].Clone();
                sectionInsertPlane.Rotate(angle, sectionInsertPlane.ZAxis);
                SectionInsertPlanes.Add(sectionInsertPlane);
            
                Transform planeToPlane = Transform.PlaneToPlane(Plane.WorldXY, sectionInsertPlane);
                Curve duplicateCurve = elementProfile.ProfileCurve.DuplicateCurve();
                duplicateCurve.Transform(planeToPlane);

                var sweep = Brep.CreateFromSweep(line.ToNurbsCurve(), duplicateCurve, true,
                    elementProfile.Tolerance)[0];
                var closed = sweep.CapPlanarHoles(elementProfile.Tolerance);
                Breps.Add(closed);
            }
        }

        public override string ToString()
        {
            return $"Element Group{Environment.NewLine}" + $"Count: {Amount}";
        }

        public override List<BimElementSet> ToElementSetList()
        {
            List<BimElementSet> bimElementSets = new List<BimElementSet>();
            foreach (var brep in Breps)
            {
                var meshes = Mesh.CreateFromBrep(brep, MeshingParameters.FastRenderMesh);
                Mesh joinedMesh = meshes[0];
                for (int i = 1; i < meshes.Length; i++)
                {
                    joinedMesh.Append(meshes[i]);    
                }
                Mesh triangulatedMesh = joinedMesh.DuplicateMesh();
                triangulatedMesh.Faces.ConvertQuadsToTriangles();
                BimElement bimElement =
                    new BimElement(triangulatedMesh, ElementType.ToString(), Color.FromArgb(100,255,255,255), new Dictionary<string, string>
                    {
                        {"Name", Name},
                        {"Profile Name", Profile.Name},
                        {"Material Name", Material.Name},
                        {"Material Grade", Material.Grade},
                    });
                bimElementSets.Add(bimElement.ToElementSet());
            }

            return bimElementSets;
        }

        public override List<IfcBuildingElement> ToBuildingElementIfc(IfcStore model)
        {
            using (var transaction = model.BeginTransaction("Create Profile Element"))
            {
                var material = model.Instances.New<IfcMaterial>();
                material.Category = Material.Name;
                material.Name = Material.Grade;
                var ifcRelAssociatesMaterial = model.Instances.New<IfcRelAssociatesMaterial>();
                ifcRelAssociatesMaterial.RelatingMaterial = material;
                
                var ifcCartesianPoints = IfcTools.PointsToIfcCartesianPoints(model, Profile.ProfilePoints, true);
                
                var polyline = model.Instances.New<IfcPolyline>();
                polyline.Points.AddRange(ifcCartesianPoints);
                
                var profile = model.Instances.New<IfcArbitraryClosedProfileDef>();
                profile.OuterCurve = polyline;
                profile.ProfileName = Profile.Name;
                profile.ProfileType = IfcProfileTypeEnum.AREA;

                List<IfcShapeRepresentation> shapes = new List<IfcShapeRepresentation>();

                foreach (var t in ElementLines)
                {
                    var body = model.Instances.New<IfcExtrudedAreaSolid>();
                    body.Depth = t.Length;
                    body.SweptArea = profile;
                    body.ExtrudedDirection = model.Instances.New<IfcDirection>();
                    body.ExtrudedDirection.SetXYZ(0, 0, 1);
                
                    var origin = model.Instances.New<IfcCartesianPoint>();
                    origin.SetXYZ(0, 0, 0);
                    body.Position = model.Instances.New<IfcAxis2Placement3D>();
                    body.Position.Location = origin;
                
                    var shape = model.Instances.New<IfcShapeRepresentation>();
                    var modelContext = model.Instances.OfType<IfcGeometricRepresentationContext>().FirstOrDefault();
                    shape.ContextOfItems = modelContext;
                    shape.RepresentationType = "SweptSolid";
                    shape.RepresentationIdentifier = "Body";
                    shape.Items.Add(body);
                    
                    shapes.Add(shape);
                }

                var buildingElements = IfcTools.CreateBuildingElements(model, ElementType, Name, shapes, SectionInsertPlanes,
                    ifcRelAssociatesMaterial);
                
                transaction.Commit();
                
                return buildingElements;
            }
        }

        public override List<IfcReinforcingElement> ToReinforcingElementIfc(IfcStore model)
        {
            throw new ArgumentException("Profile elements should be converted to IfcBuildingElement");
        }
        
        public string Name { get; }
        public Profile Profile { get; }
        public List<Plane> SectionInsertPlanes { get; }
        public List<Line> ElementLines { get; }
        public List<Brep> Breps { get; }
    }
}