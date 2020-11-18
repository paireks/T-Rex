using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class RebarGroupInfoGH : GH_Component
    {
        public RebarGroupInfoGH()
          : base("Rebar Group Info", "Rebar Group Info",
              "Creates information about given rebar group",
              "T-Rex", "Tools")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Group", "Rebar Group", "Group of reinforcement bars",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Id", "Id", "Id of the group", GH_ParamAccess.item);
            pManager.AddNumberParameter("Diameter", "Diameter", "Diameter of the rebar", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Amount", "Amount", "How many bars in a group", GH_ParamAccess.item);
            pManager.AddGenericParameter("Material", "Material", "Material of a group of rebars", GH_ParamAccess.item);
            pManager.AddNumberParameter("Volume", "Volume", "Volume of all the rebars in a given group.", GH_ParamAccess.item);
            pManager.AddNumberParameter("Weight", "Weight", "Weight of all the rebars in a given group. Calculated by multiplying given density and calculated volume.", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            RebarGroup rebarGroup = null;

            DA.GetData(0, ref rebarGroup);

            DA.SetData(0, rebarGroup.Id);
            DA.SetData(1, rebarGroup.Diameter);
            DA.SetData(2, rebarGroup.Count);
            DA.SetData(3, rebarGroup.Material);
            DA.SetData(4, rebarGroup.Volume);
            DA.SetData(5, rebarGroup.Weight);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.RebarInfo;
            }
        }
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("548419ce-fdbb-4de1-8891-386275ee24de"); }
        }
    }
}