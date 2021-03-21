using System.Collections.Generic;
using T_RexEngine;
using T_RexEngine.ElementLibrary;
using System;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace T_Rex
{
    public class PadFootingsGH : GH_Component
    {
        public PadFootingsGH()
          : base("Pad Footings", "Pad Footings",
              "Create Pad Footings",
              "T-Rex", "Concrete")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("Planes", "Planes", "Insert planes", GH_ParamAccess.list, Plane.WorldXY);
            pManager.AddNumberParameter("Height", "Height", "Height of the footing", GH_ParamAccess.item);
            pManager.AddNumberParameter("Width", "Width", "Width of the footing", GH_ParamAccess.item);
            pManager.AddNumberParameter("Length", "Length", "Length of the footing", GH_ParamAccess.item);
            pManager.AddGenericParameter("Material", "Material", "Concrete element material", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element Group", "Element Group", "Concrete elements", GH_ParamAccess.list);
            pManager.AddBrepParameter("Breps", "Breps", "Breps that represent concrete elements", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Plane> planes = new List<Plane>();
            double height = double.NaN;
            double width = double.NaN;
            double length = double.NaN;
            Material material = null;

            DA.GetDataList(0, planes);
            DA.GetData(1, ref height);
            DA.GetData(2, ref width);
            DA.GetData(3, ref length);
            DA.GetData(4, ref material);

            PadFootings padFootings = new PadFootings(planes, height, width, length, material);

            DA.SetData(0, padFootings);
            DA.SetDataList(1, padFootings.Breps);
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