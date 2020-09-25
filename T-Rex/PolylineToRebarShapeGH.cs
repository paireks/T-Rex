using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class PolylineToRebarShapeGH : GH_Component
    {
        public PolylineToRebarShapeGH()
          : base("Polyline To Rebar Shape", "Polyline To Rebar Shape",
              "Convert single polyline curve to reinforcement bar shape",
              "T-Rex", "Rebar Shape")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Polyline", "Polyline", "Polyline needed to create a reinforcement bar shape",
                GH_ParamAccess.item);
            pManager.AddNumberParameter("Bending Roller Diameter", "Bending Roller Diameter",
                "Bending roller diameter as integer", GH_ParamAccess.item);
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
            double bendingRollerDiameter = 0;
            RebarProperties props = null;

            DA.GetData(0, ref rebarCurve);
            DA.GetData(1, ref bendingRollerDiameter);
            DA.GetData(2, ref props);

            RebarShape rebarShape = new RebarShape(props);
            rebarShape.PolylineToRebarShape(rebarCurve, bendingRollerDiameter);

            DA.SetData(0, rebarShape);
            DA.SetData(1, rebarShape.RebarMesh);
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
            get { return new Guid("ca05a488-e6a3-40ee-89c4-1563d3ec0a64"); }
        }
    }
}