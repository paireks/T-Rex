using System.Collections.Generic;
using T_RexEngine;
using T_RexEngine.ElementLibrary;
using System;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace T_Rex
{
    public class StripFoundationsGH : GH_Component
    {
        public StripFoundationsGH()
          : base("Strip Foundations", "Strip Foundations",
              "Create Strip Foundations",
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
            pManager.AddGenericParameter("Elements", "Elements", "Concrete elements", GH_ParamAccess.list);
            pManager.AddBrepParameter("Brep", "Brep", "Brep that represents concrete element", GH_ParamAccess.list);
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

            StripFoundations stripFoundations = new StripFoundations(lines, height, width, material);

            DA.SetData(0, stripFoundations);
            DA.SetDataList(1, stripFoundations.Brep);
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
            get { return new Guid("be9bb4a3-9e9d-4550-8766-2f05a46dfd0e"); }
        }
    }
}