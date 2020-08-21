using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper;

namespace T_RexEngine
{
    public class RebarMeshRepresentation
    {
        public RebarMeshRepresentation(int diameter)
        {
            Diameter = diameter;

            double[][] sectionCoordinates = new double[2][];

            sectionCoordinates[0] = new double[] 
            {0.433*Diameter,
             0.250*Diameter,
             0.000, 
             -0.250*Diameter,
             -0.433*Diameter,
             -0.500*Diameter,
             -0.433*Diameter,
             -0.250*Diameter,
             0.000,
             0.250*Diameter,
             0.433*Diameter,
             0.500*Diameter};

            sectionCoordinates[1] = new double[] 
            {0.250*Diameter,
             0.433*Diameter,
             0.500*Diameter,
             0.433*Diameter,
             0.250*Diameter,
             0.000,
             -0.250*Diameter,
             -0.433*Diameter,
             -0.500*Diameter,
             -0.433*Diameter,
             -0.250*Diameter,
             0.000};

            SectionCoordinates = sectionCoordinates;

        }

        public int Diameter { get; }
        public double[][] SectionCoordinates { get; }
    }
}
