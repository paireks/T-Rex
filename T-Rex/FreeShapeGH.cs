using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel;
using Rhino.Display;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class FreeShapeGH : GH_Component
    {
        public FreeShapeGH()
          : base("Free Shape", "Free Shape",
              "Create free shape for reinforcement",
              "T-Rex", "Rebar")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Vertices", "Vertices", "Vertices needed to create a reinforcement bar shape, as list",
                GH_ParamAccess.list);
            pManager.AddGenericParameter("Properties", "Properties", "Reinforcement properties", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Mesh Points", "Mesh Points", "Desc", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Point3d> vertices = new List<Point3d>();
            RebarProperties props = null;

            DA.GetDataList(0, vertices);
            DA.GetData(1, ref props);

            FreeShape newShape = new FreeShape(vertices, props);

            DA.SetDataList(0, newShape.MeshPoints);
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
