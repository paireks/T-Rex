using System;
using Grasshopper.Kernel;
using T_RexEngine;

namespace T_Rex
{
    public class ElementGroupInfoGH : GH_Component
    {
        public ElementGroupInfoGH()
          : base("Element Group Info", "Element Group Info",
              "Creates information about given element group",
              "T-Rex", "Concrete")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Element Group", "Element Group", "Group of elements",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Amount", "Amount", "How many elements in a group", GH_ParamAccess.item);
            pManager.AddGenericParameter("Material", "Material", "Material of elements in a group", GH_ParamAccess.item);
            pManager.AddNumberParameter("Volume", "Volume", "Volume of all of the elements in a given group.", GH_ParamAccess.item);
            pManager.AddNumberParameter("Mass", "Mass", "Mass of all of the elements in a given group. Calculated by multiplying given density and calculated volume.", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            ElementGroup elementGroup = null;

            DA.GetData(0, ref elementGroup);

            DA.SetData(0, elementGroup.Amount);
            DA.SetData(1, elementGroup.Material);
            DA.SetData(2, elementGroup.Volume);
            DA.SetData(3, elementGroup.Mass);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.ElementInfo;
            }
        }
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("43fb2f8d-ace9-4fb5-bd31-b3a00d68cfb1"); }
        }
    }
}