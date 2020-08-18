using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class FreeShapeGH : GH_Component
    {
        public FreeShapeGH()
          : base("Free Shape", "Free Shape",
              "Create free shape for reinforcement",
              "T-Rex", "Shapes")
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
            pManager.AddBrepParameter("Shape", "Shape", "Shape of the reinforcement bar", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Point3d> vertices = new List<Point3d>();
            List<Brep> breps = new List<Brep>();

            DA.GetDataList(0, vertices);

            FreeShape newShape = new FreeShape(vertices);

            foreach (var brep in newShape.RebarBrep)
            {
                breps.Add(brep);
            }

            DA.SetDataList(0, breps);
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
