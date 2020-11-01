using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class CustomRebarGroupInfoGH : GH_Component
    {
        public CustomRebarGroupInfoGH()
          : base("Custom Rebar Group Info", "Custom Rebar Group Info",
              "Creates information about given custom rebar group",
              "T-Rex", "Rebar Group")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Custom Rebar Group", "Custom Rebar Group", "Custom Group of reinforcement bars",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Amount", "Amount", "How many bars in a group", GH_ParamAccess.item);
            pManager.AddGenericParameter("Material", "Material", "Material of a group of rebars", GH_ParamAccess.item);
            pManager.AddNumberParameter("Volume", "Volume", "Volume of all the rebars in a given group.", GH_ParamAccess.item);
            pManager.AddNumberParameter("Weight", "Weight", "Weight of all the rebars in a given group. Calculated by multiplying given density and calculated volume.", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            CustomRebarGroup customRebarGroup = null;

            DA.GetData(0, ref customRebarGroup);

            DA.SetData(0, customRebarGroup.Count);
            DA.SetData(1, customRebarGroup.RebarShapes[0].Props.Material);
            DA.SetData(2, customRebarGroup.Volume);
            DA.SetData(3, customRebarGroup.Weight);
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
            get { return new Guid("e54b7ef9-26b6-4a43-a6d3-34cb36bc62cb"); }
        }
    }
}