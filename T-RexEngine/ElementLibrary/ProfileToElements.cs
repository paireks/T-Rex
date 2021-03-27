using System;
using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry;
using T_RexEngine.Enums;
using Xbim.Ifc;
using Xbim.Ifc4.GeometricConstraintResource;
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
        public ProfileToElements(Profile elementProfile, Line line, double angle, Material material, int type)
        {
            Material = material;
            Profile = elementProfile;
            
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
            
            Curve lineCurve = line.ToNurbsCurve();
            double[] divisionParameters = lineCurve.DivideByCount(1, true);
            Plane[] perpendicularPlanes = lineCurve.GetPerpendicularFrames(divisionParameters);
            Plane insertPlane = perpendicularPlanes[0].Clone();
            insertPlane.Rotate(angle, insertPlane.ZAxis);
            InsertPlane = insertPlane;
            
            Transform planeToPlane = Transform.PlaneToPlane(Plane.WorldXY, InsertPlane);
            Curve duplicateCurve = elementProfile.ProfileCurve.DuplicateCurve();
            duplicateCurve.Transform(planeToPlane);
            
            Breps = Brep.CreateFromSweep(line.ToNurbsCurve(), duplicateCurve, true, 0.001);
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

                var elements = new List<IfcBuildingElement>();

                var element = model.Instances.New<IfcFooting>();
                element.Name = "Element";

                var body = model.Instances.New<IfcExtrudedAreaSolid>();
                body.Depth = ElementLine.Length;
                body.SweptArea = profile;
                body.ExtrudedDirection = model.Instances.New<IfcDirection>();
                body.ExtrudedDirection.SetXYZ(0, 0, 1);
                
                var origin = model.Instances.New<IfcCartesianPoint>();
                origin.SetXYZ(0, 0, 0);
                body.Position = model.Instances.New<IfcAxis2Placement3D>();
                body.Position.Location = origin;
                
                // Create shape that holds geometry
                var shape = model.Instances.New<IfcShapeRepresentation>();
                var modelContext = model.Instances.OfType<IfcGeometricRepresentationContext>().FirstOrDefault();
                shape.ContextOfItems = modelContext;
                shape.RepresentationType = "SweptSolid";
                shape.RepresentationIdentifier = "Body";
                shape.Items.Add(body);
                    
                // Add geometry to footing
                var representation = model.Instances.New<IfcProductDefinitionShape>();
                representation.Representations.Add(shape);
                element.Representation = representation;
                
                // Place footing in model
                var localPlacement = model.Instances.New<IfcLocalPlacement>();
                var ax3D = model.Instances.New<IfcAxis2Placement3D>();
                    
                var location = model.Instances.New<IfcCartesianPoint>();
                location.SetXYZ(InsertPlane.OriginX, InsertPlane.OriginY, InsertPlane.OriginZ);
                ax3D.Location = location;
                
                ax3D.RefDirection = model.Instances.New<IfcDirection>();
                ax3D.RefDirection.SetXYZ(1, 0, 0);
                ax3D.Axis = model.Instances.New<IfcDirection>();
                ax3D.Axis.SetXYZ(0, 0, 1);
                localPlacement.RelativePlacement = ax3D;
                element.ObjectPlacement = localPlacement;

                ifcRelAssociatesMaterial.RelatedObjects.Add(element);
                
                elements.Add(element);
                
                transaction.Commit();
                return elements;
            }
        }

        public override List<IfcReinforcingElement> ToReinforcingElementIfc(IfcStore model)
        {
            throw new ArgumentException("Profile elements should be converted to IfcBuildingElement");
        }
        
        public Profile Profile { get; }
        public Plane InsertPlane { get; }
        public Line ElementLine { get; }
        public Brep[] Breps { get; }
    }
}