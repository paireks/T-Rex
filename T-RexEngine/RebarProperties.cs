using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_RexEngine
{
    public class RebarProperties
    {
        public RebarProperties(double diameter, Material material)
        {
            Diameter = diameter;
            Radius = diameter / 2.0;

            Material = material;
        }

        public override string ToString()
        {
            return $"Diameter: {Diameter}\r\nMaterial: {Material.Name}";
        }

        public double Radius { get; set; }
        public double Diameter { get; set; }
        public Material Material { get; set; }
    }
}
