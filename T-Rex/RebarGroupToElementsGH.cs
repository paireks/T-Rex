using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;
using T_RexEngine.ElementLibrary;

namespace T_Rex
{
    public class RebarGroupToElementsGH : GH_Component
    {
        public RebarGroupToElementsGH()
          : base("Rebar Group To Elements", "Rebar Group To Elements",
              "Converts Rebar Group to Elements that can be used to export IFC",
              "T-Rex", "Rebar Spacing")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Group", "Rebar Group", "Group of reinforcement bars",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Elements", "Elements", "Rebar elements", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            RebarGroup rebarGroup = null;

            DA.GetData(0, ref rebarGroup);

            Rebars rebars = new Rebars(rebarGroup);

            DA.SetData(0, rebars);
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
            get { return new Guid("d97035d9-7866-4059-a221-a367be9fe3fc"); }
        }
    }
}