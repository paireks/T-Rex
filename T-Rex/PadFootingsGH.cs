using System.Collections.Generic;
using T_RexEngine;
using T_RexEngine.ElementLibrary;

namespace T_Rex
{
using System;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace T_Rex
{
    public class PadFootingGH : GH_Component
    {
        public PadFootingGH()
          : base("Pad Footings", "Pad Footings",
              "Create Pad Footings",
              "T-Rex", "Concrete")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "Points", "Insert points", GH_ParamAccess.list, Point3d.Origin);
            pManager.AddNumberParameter("Height", "Height", "Height of the footing", GH_ParamAccess.item);
            pManager.AddNumberParameter("Width", "Width", "Width of the footing", GH_ParamAccess.item);
            pManager.AddNumberParameter("Length", "Length", "Length of the footing", GH_ParamAccess.item);
            pManager.AddGenericParameter("Material", "Material", "Concrete element material", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Elements", "Elements", "Concrete elements", GH_ParamAccess.list);
            pManager.AddBrepParameter("Brep", "Brep", "Brep that represents concrete element", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Point3d> points = new List<Point3d>();
            double height = double.NaN;
            double width = double.NaN;
            double length = double.NaN;
            Material material = null;

            DA.GetDataList(0, points);
            DA.GetData(1, ref height);
            DA.GetData(2, ref width);
            DA.GetData(3, ref length);
            DA.GetData(4, ref material);

            PadFootings padFootings = new PadFootings(points, height, width, length, material);

            DA.SetData(0, padFootings);
            DA.SetDataList(1, padFootings.Brep);
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
            get { return new Guid("99428fca-2817-4bb9-a97d-5de802d77b38"); }
        }
    }
}
}