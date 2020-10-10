using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class CurveSpacingGH : GH_Component
    {
        public CurveSpacingGH()
          : base("Curve Spacing", "Curve Spacing",
              "Creates Rebar Group with spacing along a curve",
              "T-Rex", "Rebar Group")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Shape", "Rebar Shape", "Rebar Shape to create Rebar Group",
                GH_ParamAccess.item);
            pManager.AddCurveParameter("Curve", "Curve", "Curve to divide and create Rebar Group along this curve",
                GH_ParamAccess.item);
            pManager.AddIntegerParameter("Count", "Count", "Set how many bars should be in the group",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Group", "Rebar Group", "Group of reinforcement bars",
                GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh that represents reinforcement", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            RebarShape rebarShape = null;
            int count = 0;
            Curve curve = null;
            Plane plane = Plane.Unset;

            DA.GetData(0, ref rebarShape);
            DA.GetData(1, ref curve);
            DA.GetData(2, ref count);

            RebarGroup rebarGroup = new RebarGroup(rebarShape);
            rebarGroup.CurveSpacing(count, curve);

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
            get { return new Guid("5eef3b17-2baf-40f8-866b-9b97f93379b9"); }
        }
    }
}