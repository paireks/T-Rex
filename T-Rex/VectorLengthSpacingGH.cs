using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class VectorLengthSpacingGH : GH_Component
    {
        public VectorLengthSpacingGH()
          : base("Vector Length Spacing", "Vector Length Spacing",
              "Creates Rebar Group with vector and length that determines spacing. Start begin where the rebar shape is.",
              "T-Rex", "Rebar Group")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Id", "Id", "Id as an integer for Rebar Group", GH_ParamAccess.item);
            pManager.AddGenericParameter("Rebar Shape", "Rebar Shape", "Rebar Shape to create Rebar Group",
                GH_ParamAccess.item);
            pManager.AddVectorParameter("Vector", "Vector", "Vector that defines direction and distance where all rebars will be created",
                GH_ParamAccess.item);
            pManager.AddNumberParameter("Spacing Length", "Spacing Length", "Length that defines spacing between each bar", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Type", "Type", "Type of spacing, " +
                                                         "0: Constant spacing with different last one, " +
                                                         "1: Constant spacing with different first one, " +
                                                         "2: Constant spacing with first and last different, " +
                                                         "3: Smaller (or the same) spacing length than given, but constant for all bars from start to end", GH_ParamAccess.item);
            pManager.AddNumberParameter("Tolerance", "Tolerance",
                "Tolerance to remove duplicate meshes at the end or start of vectors", GH_ParamAccess.item);
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
            double spacingDistance = double.NaN;
            int spacingType = 0;
            double tolerance = double.NaN;

            DA.GetData(0, ref id);
            DA.GetData(1, ref rebarShape);
            DA.GetData(2, ref vector);
            DA.GetData(3, ref spacingDistance);
            DA.GetData(4, ref spacingType);
            DA.GetData(5, ref tolerance);

            RebarGroup rebarGroup = new RebarGroup(id, rebarShape);
            rebarGroup.VectorLengthSpacing(vector, spacingDistance, spacingType, tolerance);

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
            get { return new Guid("9dd8dbe4-4e1c-47be-9e3a-57816e581206"); }
        }
    }
}