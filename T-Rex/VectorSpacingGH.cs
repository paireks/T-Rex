using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class VectorSpacingGH : GH_Component
    {
        public VectorSpacingGH()
          : base("Vector Spacing", "Vector Spacing",
              "Creates Rebar Group with vector spacing between each rebar",
              "T-Rex", "Rebar Group")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Shape", "Rebar Shape", "Rebar Shape to create Rebar Group",
                GH_ParamAccess.item);
            pManager.AddVectorParameter("Vector", "Vector", "Set spacing between bars as a vector",
                GH_ParamAccess.item);
            pManager.AddIntegerParameter("Count", "Count", "Set how many bars should be in the group",
                GH_ParamAccess.item);

        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Group", "Rebar Group", "Group of reinforcement bars",
                GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh group representation", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            RebarShape rebarShape = null;
            int count = 0;
            Vector3d spaceVector = new Vector3d();

            DA.GetData(0, ref rebarShape);
            DA.GetData(1, ref spaceVector);
            DA.GetData(2, ref count);


            RebarGroup rebarGroup = new RebarGroup(rebarShape);
            rebarGroup.VectorSpacing(count, spaceVector);

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
            get { return new Guid("9e5f8c11-d3bb-4934-b7c2-6b096e3dd6da"); }
        }
    }
}