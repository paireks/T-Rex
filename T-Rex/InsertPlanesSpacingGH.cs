using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class InsertPlanesSpacingGH : GH_Component
    {
        public InsertPlanesSpacingGH()
          : base("Insert Planes Spacing", "Insert Planes Spacing",
              "Creates Rebar Group with insert planes. Rebar Shape should be located on World XY Plane, and then insert planes determines where rebars actually are located.",
              "T-Rex", "Rebar Spacing")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Id", "Id", "Id as an integer for Rebar Group", GH_ParamAccess.item);
            pManager.AddGenericParameter("Rebar Shape", "Rebar Shape", "Rebar Shape to create Rebar Group. Should be located on World XY Plane.",
                GH_ParamAccess.item);
            pManager.AddPlaneParameter("Insert Planes", "Insert Planes", "Destination planes for rebars",
                GH_ParamAccess.list);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Group", "Rebar Group", "Group of reinforcement bars",
                GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh group representation", GH_ParamAccess.list);
            pManager.AddCurveParameter("Curve", "Curve", "Curves that represents reinforcement", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int id = 0;
            RebarShape rebarShape = null;
            List<Plane> insertPlanes = new List<Plane>();

            DA.GetData(0, ref id);
            DA.GetData(1, ref rebarShape);
            DA.GetDataList(2, insertPlanes);

            RebarGroup rebarGroup = new RebarGroup(id, new RebarSpacing(rebarShape, insertPlanes));

            DA.SetData(0, rebarGroup);
            DA.SetDataList(1, rebarGroup.RebarGroupMesh);
            DA.SetDataList(2, rebarGroup.RebarGroupCurves);
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
            get { return new Guid("63acf7a3-8e63-4f81-89ac-6a390d01a8ca"); }
        }
    }
}