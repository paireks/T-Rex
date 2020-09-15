using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_RexEngine
{
    public class RebarProperties
    {
        public RebarProperties(int diameter, string material)
        {
            Diameter = diameter;
            Material = material;
        }

        public override string ToString()
        {
            return $"Diameter: {Diameter}\r\nMaterial: {Material}";
        }

        public int Diameter { get; set; }
        public string Material { get; set; }
    }
}
