/*using System.Collections.Generic;
using T_RexEngine;
using T_RexEngine.ElementLibrary;
using System;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace T_Rex
{
    public class MeshElementGH : GH_Component
    {
        public MeshElementGH()
          : base("Mesh Element", "Mesh Element",
              "Create Mesh Element",
              "T-Rex", "Concrete")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh representation of model", GH_ParamAccess.item);
            pManager.AddGenericParameter("Material", "Material", "Concrete element material", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "Element", "Concrete element", GH_ParamAccess.list);
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh that represents concrete element", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh mesh = null;
            Material material = null;

            DA.GetData(0, ref mesh);
            DA.GetData(1, ref material);

            MeshElement meshElement = new MeshElement(mesh, material);

            DA.SetData(0, meshElement);
            DA.SetData(1, meshElement.Mesh);
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
            get { return new Guid("eeda1d0b-9d48-459a-a6ff-5a8cabd37b55"); }
        }
    }
}*/