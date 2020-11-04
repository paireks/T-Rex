using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class VectorCountSpacingGH : GH_Component
    {
        public VectorCountSpacingGH()
          : base("Vector Count Spacing", "Vector Count Spacing",
              "Creates Rebar Group with vector and rebar count. Start begin where the rebar shape is.",
              "T-Rex", "Rebar Spacing")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Id", "Id", "Id as an integer for Rebar Group", GH_ParamAccess.item);
            pManager.AddGenericParameter("Rebar Shape", "Rebar Shape", "Rebar Shape to create Rebar Group",
                GH_ParamAccess.item);
            pManager.AddVectorParameter("Vector", "Vector", "Vector that defines direction and distance where all rebars will be created",
                GH_ParamAccess.item);
            pManager.AddIntegerParameter("Count", "Count", "How many rebars will be in a group", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Group", "Rebar Group", "Group of reinforcement bars",
                GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh group representation", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int id = 0;
            RebarShape rebarShape = null;
            Vector3d vector = new Vector3d();
            int count = 0;

            DA.GetData(0, ref id);
            DA.GetData(1, ref rebarShape);
            DA.GetData(2, ref vector);
            DA.GetData(3, ref count);

            RebarGroup rebarGroup = new RebarGroup(id, rebarShape);
            rebarGroup.VectorCountSpacing(vector, count);

            DA.SetData(0, rebarGroup);
            DA.SetDataList(1, rebarGroup.RebarGroupMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.VectorCountSpacing;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("9dd8dbe4-4e1c-47be-9e3a-57816e581207"); }
        }
    }
}