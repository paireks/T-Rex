using Xbim.Ifc;
using Xbim.Ifc4.ProductExtension;

namespace T_RexEngine.Interfaces
{
    public interface IIfcElement
    {
        IfcBuildingElement ToIfc(IfcStore model);
    }
}