using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class BendingRollerGH : GH_Component
    {
        public BendingRollerGH()
          : base("Bending Roller", "Bending Roller",
              "Bending roller settings. Tolerances are used when fillets are being made to create Rebar Shapes",
              "T-Rex", "Properties")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Diameter", "Diameter", "Diameter of bending roller", GH_ParamAccess.item);
            pManager.AddNumberParameter("Tolerance", "Tolerance", "Tolerance for filleting", GH_ParamAccess.item, 0.0001);
            pManager.AddNumberParameter("Angle Tolerance", "Angle Tolerance",
                "Angle tolerance for filleting in radians", GH_ParamAccess.item,0.0175);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bending Roller", "Bending Roller", "Created bending roller",
                GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double diameter = 0.0;
            double tolerance = 0.0001;
            double angleTolerance = 0.0175;

            DA.GetData(0, ref diameter);
            DA.GetData(1, ref tolerance);
            DA.GetData(2, ref angleTolerance);

            BendingRoller bendingRoller = new BendingRoller(diameter, tolerance, angleTolerance);

            DA.SetData(0, bendingRoller);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.BendingRoller;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("9f001323-5b45-4594-b209-8e35f3cc2e3d"); }
        }
    }
}