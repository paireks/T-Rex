using System;
using Rhino.Geometry;
using T_RexEngine.Enums;
using T_RexEngine.Interfaces;
using Xbim.Ifc;
using Xbim.Ifc4.ProductExtension;

namespace T_RexEngine
{
    public class Element : IIfcElement
    {
        public Brep Brep { get; set; }
        
        public Mesh Mesh { get; set; }
        public Material Material { get; set; }
        public ElementType ElementType { get; set; }

        public virtual IfcBuildingElement ToIfc(IfcStore model)
        {
            throw new NotImplementedException();
        }
    }
}