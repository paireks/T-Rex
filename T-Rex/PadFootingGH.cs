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
          : base("Pad Footing", "Pad Footing",
              "Create Pad Footing",
              "T-Rex", "Concrete")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("Plane", "Plane", "Insert plane", GH_ParamAccess.item, Plane.WorldXY);
            pManager.AddNumberParameter("Height", "Height", "Height of the footing", GH_ParamAccess.item);
            pManager.AddNumberParameter("Width", "Width", "Width of the footing", GH_ParamAccess.item);
            pManager.AddNumberParameter("Length", "Length", "Length of the footing", GH_ParamAccess.item);
            pManager.AddGenericParameter("Material", "Material", "Concrete element material", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Concrete Element", "Concrete Element", "Concrete element", GH_ParamAccess.item);
            pManager.AddBrepParameter("Brep", "Brep", "Brep that represents concrete element", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Plane plane = Plane.Unset;
            double height = double.NaN;
            double width = double.NaN;
            double length = double.NaN;

            DA.GetData(0, ref plane);
            DA.GetData(1, ref height);
            DA.GetData(2, ref width);
            DA.GetData(3, ref length);

            Box padFooting = new Box(plane, new Interval(-length/2.0, length/2.0), new Interval(-width/2.0, width/2.0), new Interval(0, height));

            DA.SetData(1, padFooting.ToBrep());
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