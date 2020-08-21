using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_RexEngine
{
    public class RebarProperties
    {
        public RebarProperties(int diameter, string material, int bendingRoller)
        {
            Diameter = diameter;
            Material = material;
            BendingRoller = bendingRoller;
        }

        public override string ToString()
        {
            return $"Diameter: {Diameter}\r\nMaterial: {Material}\r\nBending Roller: {BendingRoller}";
        }

        public int Diameter { get; set; }
        public string Material { get; set; }
        public int BendingRoller { get; set; }
    }
}
