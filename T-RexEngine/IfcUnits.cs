/*using System.Collections.Generic;
using T_RexEngine.Enums;
using Xbim.Common;
using Xbim.Common.Model;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.MeasureResource;
using Xbim.IO.Memory;

namespace T_RexEngine
{
    public class IfcUnits
    {
        public IfcUnits(IfcStore model, UnitPrefix lengthPrefix, UnitPrefix volumePrefix, UnitPrefix weightPrefix)
        {
            IfcSIUnit lenghtUnit = model.Instances.New<IfcSIUnit>();
            lenghtUnit.UnitType = IfcUnitEnum.LENGTHUNIT;
            lenghtUnit.Name = IfcSIUnitName.METRE;
            SetPrefixToUnit(lenghtUnit, lengthPrefix);

            IfcSIUnit volumeUnit = model.Instances.New<IfcSIUnit>();
            volumeUnit.UnitType = IfcUnitEnum.VOLUMEUNIT;
            volumeUnit.Name = IfcSIUnitName.CUBIC_METRE;
            SetPrefixToUnit(volumeUnit, volumePrefix);

            IfcSIUnit weightUnit = model.Instances.New<IfcSIUnit>();
            weightUnit.UnitType = IfcUnitEnum.MASSUNIT;
            weightUnit.Name = IfcSIUnitName.GRAM;
            SetPrefixToUnit(weightUnit, weightPrefix);

            
        }

        private void SetPrefixToUnit(IfcSIUnit unit, UnitPrefix prefix)
        {
            switch (prefix)
            {
                case UnitPrefix.Milli:
                    unit.Prefix = IfcSIPrefix.MILLI;
                    break;
                case UnitPrefix.Centi:
                    unit.Prefix = IfcSIPrefix.CENTI;
                    break;
                case UnitPrefix.None:
                    break;
            }
        }

        public ProjectUnits ProjectUnits { get; }
    }
}*/