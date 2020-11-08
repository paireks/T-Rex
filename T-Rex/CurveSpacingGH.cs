using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class CurveSpacingGH : GH_Component
    {
        private bool _useDegrees = false;
        public CurveSpacingGH()
          : base("Curve Spacing", "Curve Spacing",
              "Creates Rebar Group with spacing along a curve",
              "T-Rex", "Rebar Spacing")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Id", "Id", "Id as an integer for Rebar Group", GH_ParamAccess.item);
            pManager.AddGenericParameter("Rebar Shape", "Rebar Shape", "Rebar Shape to create Rebar Group",
                GH_ParamAccess.item);
            pManager.AddPlaneParameter("Shape's Origin Plane", "Shape's Origin Plane",
                "This plane will be treated as origin, while the target planes will be the planes on the curve.", GH_ParamAccess.item);
            pManager.AddCurveParameter("Curve", "Curve", "Curve to divide and create Rebar Group along this curve",
                GH_ParamAccess.item);
            pManager.AddIntegerParameter("Count", "Count", "Set how many bars should be in the group",
                GH_ParamAccess.item);
            pManager.AddAngleParameter("Rotation Angle", "Rotation Angle", "Set rotation angle for all of the bars",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Group", "Rebar Group", "Group of reinforcement bars",
                GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh that represents reinforcement", GH_ParamAccess.list);
        }

        protected override void BeforeSolveInstance()
        {
            base.BeforeSolveInstance();
            _useDegrees = false;
            if (Params.Input[3] is Param_Number angleParameter)
                _useDegrees = angleParameter.UseDegrees;
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int id = 0;
            RebarShape rebarShape = null;
            Plane plane = Plane.Unset;
            int count = 0;
            Curve curve = null;
            double angle = 0.0;
            
            DA.GetData(0, ref id);
            DA.GetData(1, ref rebarShape);
            DA.GetData(2, ref plane);
            DA.GetData(3, ref curve);
            DA.GetData(4, ref count);
            if (!DA.GetData(5, ref angle)) return;
            if (_useDegrees)
                angle = RhinoMath.ToRadians(angle);

            RebarGroup rebarGroup = new RebarGroup(id, rebarShape);
            rebarGroup.UseCurveSpacing(plane, count, curve, angle);

            DA.SetData(0, rebarGroup);
            DA.SetDataList(1, rebarGroup.RebarGroupMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.CurveSpacingShape;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("5eef3b17-2baf-40f8-866b-9b97f93379b9"); }
        }
    }
}