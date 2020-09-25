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
        public Material(string name, string grade)
        {
            Name = name;
            Grade = grade;
        }

        public string Name { get; set; }
        public string Grade { get; set; }
    }
}
