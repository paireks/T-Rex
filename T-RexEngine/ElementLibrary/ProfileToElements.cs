using System;
using System.Collections.Generic;
using Rhino.Geometry;
using Xbim.Ifc;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.StructuralElementsDomain;

namespace T_RexEngine.ElementLibrary
{
    public class ProfileToElements: ElementGroup
    {
        public ProfileToElements(ElementProfile elementProfile, Line line, Material material, int type)
        {
            Curve lineCurve = line.ToNurbsCurve();
            double[] divisionParameters = lineCurve.DivideByCount(1, true);
            Plane[] perpendicularPlanes = lineCurve.GetPerpendicularFrames(divisionParameters);
            Planes = perpendicularPlanes;
            Transform planeToPlane = Transform.PlaneToPlane(Plane.WorldXY, perpendicularPlanes[0]);
            Curve duplicateCurve = elementProfile.ProfileCurve.DuplicateCurve();
            duplicateCurve.Transform(planeToPlane);
            Breps = Brep.CreateFromSweep(line.ToNurbsCurve(), duplicateCurve, true, 0.001);
        }
        public override List<IfcBuildingElement> ToBuildingElementIfc(IfcStore model)
        {
            throw new System.NotImplementedException();
        }

        public override List<IfcReinforcingElement> ToReinforcingElementIfc(IfcStore model)
        {
            throw new ArgumentException("Profile elements should be converted to IfcBuildingElement");
        }
        
        public Brep[] Breps { get; }
        public Plane[] Planes { get; }
    }
}