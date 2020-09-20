using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_RexEngine
{
    public class RebarProperties
    {
        public RebarProperties(int diameter, Material material)
        {
            Diameter = diameter;
            Material = material;
        }

        public override string ToString()
        {
            return $"Diameter: {Diameter}\r\nMaterial: {Material.Name}";
        }

        public int Diameter { get; set; }
        public Material Material { get; set; }
    }
}
