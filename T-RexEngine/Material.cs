using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.GUI.Gradient;

namespace T_RexEngine
{
    public class Material
    {
        public Material(string name, string grade, int density, double priceOfKilo)
        {
            Name = name;
            Grade = grade;
            Density = density;
            PriceOfKilo = priceOfKilo;
        }

        public string Name { get; set; }
        public string Grade { get; set; }
        public int Density { get; set; }
        public double PriceOfKilo { get; set; }
    }
}
