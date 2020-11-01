using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class WithoutSpacingGH : GH_Component
    {
        public WithoutSpacingGH()
          : base("Without Spacing", "Without Spacing",
              "Creates Custom Rebar Group without any spacing. Add 1 or more Rebar Shapes to create a custom group.",
              "T-Rex", "Rebar Group")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Shapes", "Rebar Shapes", "Rebar Shapes to create Rebar Group",
                GH_ParamAccess.list);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Custom Rebar Group", "Custom Rebar Group", "Custom group of reinforcement bars",
                GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh group representation", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<RebarShape> rebarShapes = new List<RebarShape>();

            DA.GetDataList(0, rebarShapes);

            CustomRebarGroup rebarGroup = new CustomRebarGroup(rebarShapes);

            DA.SetData(0, rebarGroup);
            DA.SetDataList(1, rebarGroup.RebarGroupMesh);
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
            get { return new Guid("ceb38659-6e1c-48c5-b12c-7046343226cc"); }
        }
    }
}