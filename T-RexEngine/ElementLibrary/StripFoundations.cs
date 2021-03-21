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
    public class StripFoundations: ElementGroup
    {
        public StripFoundations(List<Line> insertLines, double height, double width, Material material)
        {
            Height = height;
            Width = width;
            InsertLines = insertLines;
            
            Breps = new List<Brep>();
            Planes = new List<Plane>();

            foreach (var line in InsertLines)
            {
                Line duplicateLine = new Line(line.From, line.To);
                duplicateLine.Transform(new Transform(Transform.Rotation(Math.PI / 2, Vector3d.ZAxis, line.From)));
                Plane insertPlane = new Plane(line.From, line.Direction, duplicateLine.Direction);
                Planes.Add(insertPlane);
                
                Interval lengthInterval = new Interval(0, line.Length);
                Interval widthInterval = new Interval(-Width/2.0, Width/2.0);
                Interval heightInterval = new Interval(0, Height);
                
                Box box = new Box(insertPlane, lengthInterval, widthInterval, heightInterval);
                Breps.Add(box.ToBrep());
            }
            
            Material = material;
            ElementType = ElementType.StripFoundation;
        }

        public override List<IfcReinforcingElement> ToReinforcingElementIfc(IfcStore model)
        {
            throw new Exception("Strip foundation should be converted to IfcBuildingElement");
        }

        public override List<IfcBuildingElement> ToBuildingElementIfc(IfcStore model)
        {
            using (var transaction = model.BeginTransaction("Create Pad Footing"))
            {
                //Create rectangle profile
                var rectangleProfile = model.Instances.New<IfcRectangleProfileDef>();
                rectangleProfile.ProfileType = IfcProfileTypeEnum.AREA;
                rectangleProfile.XDim = Width;
                rectangleProfile.YDim = Height;
                
                //Insert profile
                var profileInsertPoint = model.Instances.New<IfcCartesianPoint>();
                profileInsertPoint.SetXY(0, Height/2);
                rectangleProfile.Position = model.Instances.New<IfcAxis2Placement2D>();
                rectangleProfile.Position.Location = profileInsertPoint;

                // Create material
                var material = model.Instances.New<IfcMaterial>();
                material.Category = Material.Name;
                material.Name = Material.Grade;
                var ifcRelAssociatesMaterial = model.Instances.New<IfcRelAssociatesMaterial>();
                ifcRelAssociatesMaterial.RelatingMaterial = material;
                
                // Create footings
                var footings = new List<IfcBuildingElement>();

                for (int i = 0; i < InsertLines.Count; i++)
                {
                    var footing = model.Instances.New<IfcFooting>();
                    footing.Name = "Strip Foundation";
                    
                    //Model as a swept area solid
                    var body = model.Instances.New<IfcExtrudedAreaSolid>();
                    body.Depth = InsertLines[i].Length;
                    body.SweptArea = rectangleProfile;
                    body.ExtrudedDirection = model.Instances.New<IfcDirection>();
                    body.ExtrudedDirection.SetXYZ(0, 0, 1);
                    
                    // Parameters to insert the geometry in the model
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
                    footing.Representation = representation;
                
                    // Place footing in model
                    var localPlacement = model.Instances.New<IfcLocalPlacement>();
                    var ax3D = model.Instances.New<IfcAxis2Placement3D>();
                    
                    var location = model.Instances.New<IfcCartesianPoint>();
                    location.SetXYZ(Planes[i].OriginX, Planes[i].OriginY, Planes[i].OriginZ);
                    ax3D.Location = location;
                
                    ax3D.RefDirection = model.Instances.New<IfcDirection>();
                    ax3D.RefDirection.SetXYZ(Planes[i].YAxis.X, Planes[i].YAxis.Y, Planes[i].YAxis.Z);
                    ax3D.Axis = model.Instances.New<IfcDirection>();
                    ax3D.Axis.SetXYZ(Planes[i].XAxis.X, Planes[i].XAxis.Y, Planes[i].XAxis.Z);
                    localPlacement.RelativePlacement = ax3D;
                    footing.ObjectPlacement = localPlacement;
                

                    ifcRelAssociatesMaterial.RelatedObjects.Add(footing);
                    
                    footings.Add(footing);
                }

                transaction.Commit();
                return footings;
            }
        }
        public List<Brep> Breps { get; }
        private double Height { get; }
        private double Width { get; }
        private List<Line> InsertLines { get; }
        private List<Plane> Planes { get; }
    }
}