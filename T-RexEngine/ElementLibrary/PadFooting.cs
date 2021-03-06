using System.Linq;
using Rhino.Geometry;
using Rhino.UI;
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
using IIfcElement = T_RexEngine.Interfaces.IIfcElement;

namespace T_RexEngine.ElementLibrary
{
    public class PadFooting: Element
    {
        public PadFooting(Point3d insertPoint, double height, double width, double length, Material material)
        {
            InsertPoint = insertPoint;
            Height = height;
            Width = width;
            Length = length;

            Box padFooting = new Box(new Plane(InsertPoint, Vector3d.XAxis, Vector3d.YAxis), new Interval(-Length/2.0, Length/2.0), new Interval(-Width/2.0, Width/2.0), new Interval(0, Height));
            Brep = padFooting.ToBrep();
            Material = material;
            ElementType = ElementType.PadFooting;
        }

        public override IfcBuildingElement ToIfc(IfcStore model)
        {
            using (var transaction = model.BeginTransaction("Create Wall"))
            {
                var footing = model.Instances.New<IfcFooting>();
                footing.Name = "Pad Footing";
                
                //Create rectangle profile
                var rectangleProfile = model.Instances.New<IfcRectangleProfileDef>();
                rectangleProfile.ProfileType = IfcProfileTypeEnum.AREA;
                rectangleProfile.XDim = Length;
                rectangleProfile.YDim = Width;
                
                //Insert profile
                var insertPoint = model.Instances.New<IfcCartesianPoint>();
                insertPoint.SetXYZ(InsertPoint.X, InsertPoint.Y, InsertPoint.Z);
                rectangleProfile.Position = model.Instances.New<IfcAxis2Placement2D>();
                rectangleProfile.Position.Location = insertPoint;

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
                
                // Add geometry to footing
                var representation = model.Instances.New<IfcProductDefinitionShape>();
                representation.Representations.Add(shape);
                footing.Representation = representation;
                
                // Place footing in model
                var localPlacement = model.Instances.New<IfcLocalPlacement>();
                var ax3D = model.Instances.New<IfcAxis2Placement3D>();
                ax3D.Location = origin;
                ax3D.RefDirection = model.Instances.New<IfcDirection>();
                ax3D.RefDirection.SetXYZ(0, 1, 0);
                ax3D.Axis = model.Instances.New<IfcDirection>();
                ax3D.Axis.SetXYZ(0, 0, 1);
                localPlacement.RelativePlacement = ax3D;
                footing.ObjectPlacement = localPlacement;
                
                // Add material to footing
                var material = model.Instances.New<IfcMaterial>();
                material.Name = Material.Name;
                var ifcRelAssociatesMaterial = model.Instances.New<IfcRelAssociatesMaterial>();
                ifcRelAssociatesMaterial.RelatingMaterial = material;
                ifcRelAssociatesMaterial.RelatedObjects.Add(footing);
                
                transaction.Commit();
                return footing;
            }
        }
        
        private Point3d InsertPoint { get; }
        private double Height { get; }
        private double Width { get; }
        private double Length { get; }
    }
}