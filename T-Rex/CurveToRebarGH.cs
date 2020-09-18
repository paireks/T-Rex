using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel;
using Rhino.Display;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class CurveToRebarGH : GH_Component
    {
        public CurveToRebarGH()
          : base("Curve To Rebar Shape", "Curve To Rebar Shape",
              "Convert single curve to reinforcement bar shape",
              "T-Rex", "Rebar Shape")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Curve", "Curve", "Curve needed to create a reinforcement bar shape",
                GH_ParamAccess.item);
            pManager.AddGenericParameter("Properties", "Properties", "Reinforcement properties", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Shape", "Rebar Shape", "Reinforcement bar shape", GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Desc", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve rebarCurve = null;
            RebarProperties props = null;

            DA.GetData(0, ref rebarCurve);
            DA.GetData(1, ref props);

            CurveToRebar newShape = new CurveToRebar(rebarCurve, props);

            DA.SetData(0, newShape);
            DA.SetData(1, newShape.RebarMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("87d954f3-f567-4d80-bbdd-78caaabec4da"); }
        }
    }
}
