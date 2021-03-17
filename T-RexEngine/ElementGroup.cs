using System;
using System.Collections.Generic;
using Rhino.Geometry;
using T_RexEngine.Enums;
using Xbim.Ifc;
using Xbim.Ifc4.ProductExtension;

namespace T_RexEngine
{
    public abstract class ElementGroup
    {
        public Material Material { get; set; }
        public ElementType ElementType { get; set; }
        public abstract List<IfcBuildingElement> ToIfc(IfcStore model);
    }
}