using System.Collections.Generic;
using T_RexEngine;
using T_RexEngine.ElementLibrary;
using System;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace T_Rex
{
    public class StripFootingsGH : GH_Component
    {
        public StripFootingsGH()
          : base("Strip Footings", "Strip Footings",
              "Create Strip Footings",
              "T-Rex", "Concrete")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddLineParameter("Lines", "Lines", "Insert lines", GH_ParamAccess.list);
            pManager.AddNumberParameter("Height", "Height", "Height of the footing", GH_ParamAccess.item);
            pManager.AddNumberParameter("Width", "Width", "Width of the footing", GH_ParamAccess.item);
            pManager.AddGenericParameter("Material", "Material", "Concrete element material", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element Group", "Element Group", "Concrete elements", GH_ParamAccess.list);
            pManager.AddBrepParameter("Breps", "Breps", "Breps that represent concrete elements", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Line> lines = new List<Line>();
            double height = double.NaN;
            double width = double.NaN;
            Material material = null;

            DA.GetDataList(0, lines);
            DA.GetData(1, ref height);
            DA.GetData(2, ref width);
            DA.GetData(3, ref material);

            StripFootings stripFootings = new StripFootings(lines, height, width, material);

            DA.SetData(0, stripFootings);
            DA.SetDataList(1, stripFootings.Breps);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("be9bb4a3-9e9d-4550-8766-2f05a46dfd0e"); }
        }
    }
}