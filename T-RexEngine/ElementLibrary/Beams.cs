using System.Collections.Generic;
using Rhino.Geometry;
using T_RexEngine.Enums;
using Xbim.Ifc;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.StructuralElementsDomain;

namespace T_RexEngine.ElementLibrary
{
    public class Beams : ElementGroup
    {
        public Beams(List<Plane> insertPlanes, Profile profile)
        {
            ElementType = ElementType.Beams;
        }
        public override List<IfcBuildingElement> ToBuildingElementIfc(IfcStore model)
        {
            throw new System.NotImplementedException();
        }

        public override List<IfcReinforcingElement> ToReinforcingElementIfc(IfcStore model)
        {
            throw new System.NotImplementedException();
        }
    }
}