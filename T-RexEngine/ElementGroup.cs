using System;
using System.Collections.Generic;
using Rhino.Geometry;
using T_RexEngine.Enums;
using T_RexEngine.Interfaces;
using Xbim.Ifc;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.StructuralElementsDomain;

namespace T_RexEngine
{
    public abstract class ElementGroup: IElementSetConvertable
    {
        public Material Material { get; set; }
        public ElementType ElementType { get; set; }
        public abstract List<IfcBuildingElement> ToBuildingElementIfc(IfcStore model);
        public abstract List<IfcReinforcingElement> ToReinforcingElementIfc(IfcStore model);
        public int Amount { get; set; }
        public double Volume { get; set; }
        public double Mass { get; set; }
        public abstract BimElementSet ToElementSet();
    }
}