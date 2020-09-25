using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class CoverDimensionsGH : GH_Component
    {
        public CoverDimensionsGH()
          : base("Cover Dimensions", "Cover Dimensions",
              "Concrete cover dimensions, useful for Rebar Shapes that require rectangle as input.",
              "T-Rex", "Properties")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Left", "Left", "Left concrete cover dimension", GH_ParamAccess.item, 40);
            pManager.AddNumberParameter("Right", "Right", "Right concrete cover dimension", GH_ParamAccess.item, 40);
            pManager.AddNumberParameter("Top", "Top", "Top concrete cover dimension", GH_ParamAccess.item, 40);
            pManager.AddNumberParameter("Bottom", "Bottom", "Bottom concrete cover dimension", GH_ParamAccess.item, 40);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Cover Dimensions", "Cover Dimensions", "Created concrete cover dimensions",
                GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double left = 0.0;
            double right = 0.0;
            double top = 0.0;
            double bottom = 0.0;

            DA.GetData(0, ref left);
            DA.GetData(1, ref right);
            DA.GetData(2, ref top);
            DA.GetData(3, ref bottom);

            CoverDimensions cover = new CoverDimensions(left, right, top, bottom);

            DA.SetData(0, cover);
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
            get { return new Guid("b6b516e2-d5e8-4dee-b279-44be119823de"); }
        }
    }
}