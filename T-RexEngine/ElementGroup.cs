using System;
using System.Collections.Generic;
using Rhino.Geometry;
using T_RexEngine.Enums;
using Xbim.Ifc;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.StructuralElementsDomain;

namespace T_RexEngine
{
    public abstract class ElementGroup
    {
        public Material Material { get; set; }
        public ElementType ElementType { get; set; }
        public abstract List<IfcBuildingElement> ToBuildingElementIfc(IfcStore model);
        public abstract List<IfcReinforcingElement> ToReinforcingElementIfc(IfcStore model);
    }
}