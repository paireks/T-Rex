using System;
using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry;
using T_RexEngine.Enums;
using Xbim.Ifc;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.MaterialResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.ProfileResource;
using Xbim.Ifc4.RepresentationResource;
using Xbim.Ifc4.StructuralElementsDomain;

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

            double volume = 0;
            double surfaceArea = elementProfile.BoundarySurfaces[0].GetArea();
            foreach (var line in insertLines)
            {
                volume += surfaceArea * line.Length;
            }

            Volume = volume;
            Mass = Volume * material.Density;
            
            switch (type)
            {
                case 0:
                    ElementType = ElementType.PadFooting;
                    break;
                case 1:
                    ElementType = ElementType.StripFootings;
                    break;
                default:
                    throw new ArgumentException("Element type not recognized");
            }
            
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
                
                Breps.Add(Brep.CreateFromSweep(line.ToNurbsCurve(), duplicateCurve, true, elementProfile.Tolerance)[0]);
            }
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

                IfcFootingTypeEnum predefinedTypeEnum;
                
                switch (ElementType)
                {
                    case ElementType.PadFooting:
                        predefinedTypeEnum = IfcFootingTypeEnum.PAD_FOOTING;
                        break;
                    case ElementType.StripFootings:
                        predefinedTypeEnum = IfcFootingTypeEnum.STRIP_FOOTING;
                        break;
                    default:
                        throw new ArgumentException("Element type not recognized");
                }

                var elements = new List<IfcBuildingElement>();

                for (int i = 0; i < ElementLines.Count; i++)
                {
                    var element = model.Instances.New<IfcFooting>();
                    element.Name = Name;
                    element.PredefinedType = predefinedTypeEnum;

                    var body = model.Instances.New<IfcExtrudedAreaSolid>();
                    body.Depth = ElementLines[i].Length;
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
                    
                    var representation = model.Instances.New<IfcProductDefinitionShape>();
                    representation.Representations.Add(shape);
                    element.Representation = representation;

                    var localPlacement = IfcTools.CreateLocalPlacement(model, SectionInsertPlanes[i]);
                    element.ObjectPlacement = localPlacement;

                    ifcRelAssociatesMaterial.RelatedObjects.Add(element);
                
                    elements.Add(element);
                }

                
                transaction.Commit();
                return elements;
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