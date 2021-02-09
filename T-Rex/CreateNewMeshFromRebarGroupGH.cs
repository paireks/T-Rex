using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class CreateNewMeshFromRebarGroupGH : GH_Component
    {
        public CreateNewMeshFromRebarGroupGH()
          : base("Create New Mesh From Rebar Group", "Create New Mesh From Rebar Group",
              "Creates new meshes from Rebar Group with custom settings",
              "T-Rex", "Tools")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Group", "Rebar Group", "Group of reinforcement bars",
                GH_ParamAccess.item);
            pManager.AddIntegerParameter("Segments", "Segments", "Segments as integer", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Accuracy", "Accuracy", "Accuracy as integer", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "Mesh", "Created new custom mesh", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            RebarGroup rebarGroup = null;
            int segments = 0;
            int accuracy = 0;

            DA.GetData(0, ref rebarGroup);
            DA.GetData(1, ref segments);
            DA.GetData(2, ref accuracy);

            List<Mesh> newMeshes = Tools.CreateNewCustomMesh(rebarGroup, segments, accuracy);

            DA.SetDataList(0, newMeshes);
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