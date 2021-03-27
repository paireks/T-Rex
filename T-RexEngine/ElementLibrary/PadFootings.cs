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
    public class PadFootings: ElementGroup
    {
        public PadFootings(List<Plane> insertPlanes, double height, double width, double length, Material material)
        {
            Height = height;
            Width = width;
            Length = length;
            InsertPlanes = insertPlanes;
            
            Breps = new List<Brep>();

            foreach (var plane in InsertPlanes)
            {
                Interval lengthInterval = new Interval(-Length/2.0, Length/2.0);
                Interval widthInterval = new Interval(-Width/2.0, Width/2.0);
                Interval heightInterval = new Interval(0, Height);
                
                Box box = new Box(plane, lengthInterval, widthInterval, heightInterval);
                Breps.Add(box.ToBrep());
            }
            
            Material = material;
            ElementType = ElementType.PadFooting;
        }

        public override List<IfcReinforcingElement> ToReinforcingElementIfc(IfcStore model)
        {
            throw new System.NotImplementedException("Pad footing should be converted to IfcBuildingElement");
        }

        public override List<IfcBuildingElement> ToBuildingElementIfc(IfcStore model)
        {
            using (var transaction = model.BeginTransaction("Create Pad Footing"))
            {
                //Create rectangle profile
                var rectangleProfile = model.Instances.New<IfcRectangleProfileDef>();
                rectangleProfile.ProfileType = IfcProfileTypeEnum.AREA;
                rectangleProfile.XDim = Length;
                rectangleProfile.YDim = Width;
                
                //Insert profile
                var profileInsertPoint = model.Instances.New<IfcCartesianPoint>();
                profileInsertPoint.SetXY(0, 0);
                rectangleProfile.Position = model.Instances.New<IfcAxis2Placement2D>();
                rectangleProfile.Position.Location = profileInsertPoint;

                //Model as a swept area solid
                var body = model.Instances.New<IfcExtrudedAreaSolid>();
                body.Depth = Height;
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
                
                // Create material
                var material = model.Instances.New<IfcMaterial>();
                material.Category = Material.Name;
                material.Name = Material.Grade;
                var ifcRelAssociatesMaterial = model.Instances.New<IfcRelAssociatesMaterial>();
                ifcRelAssociatesMaterial.RelatingMaterial = material;
                
                // Create footings
                var footings = new List<IfcBuildingElement>();
                
                foreach (var insertPlane in InsertPlanes)
                {
                    var footing = model.Instances.New<IfcFooting>();
                    footing.Name = "Pad Footing";
                    footing.PredefinedType = IfcFootingTypeEnum.PAD_FOOTING;

                    // Add geometry to footing
                    var representation = model.Instances.New<IfcProductDefinitionShape>();
                    representation.Representations.Add(shape);
                    footing.Representation = representation;
                
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
                    footing.ObjectPlacement = localPlacement;
                

                    ifcRelAssociatesMaterial.RelatedObjects.Add(footing);
                    
                    footings.Add(footing);
                }

                transaction.Commit();
                return footings;
            }
        }
        public List<Brep> Breps{ get; }
        private double Height { get; }
        private double Width { get; }
        private double Length { get; }
        private List<Plane> InsertPlanes { get; }
    }
}