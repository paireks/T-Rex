using System.Collections.Generic;
using T_RexEngine;
using T_RexEngine.ElementLibrary;
using System;
using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine.Enums;

namespace T_Rex
{
    public class CustomElementsGH : GH_Component
    {
        public CustomElementsGH()
          : base("Custom Elements", "Custom Elements",
              "Create Elements from Mesh",
              "T-Rex", "Concrete")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh representation of model", GH_ParamAccess.item);
            pManager.AddGenericParameter("Material", "Material", "Concrete element material", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Type", "Type", "Element type as integer. 0 = Pad Footing, 1 = Strip Foundation", GH_ParamAccess.item);
            pManager.AddPlaneParameter("Insert Planes", "Insert Planes", "Destination planes of an element",
                GH_ParamAccess.list);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element Group", "Element Group", "Concrete elements", GH_ParamAccess.item);
            pManager.AddMeshParameter("Meshes", "Meshes", "Meshes that  concrete elements", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh mesh = null;
            Material material = null;
            int type = 0;
            List<Plane> insertPlanes = new List<Plane>();

            DA.GetData(0, ref mesh);
            DA.GetData(1, ref material);
            DA.GetData(2, ref type);
            DA.GetDataList(3, insertPlanes);

            CustomElements customElements = new CustomElements(mesh, material, type, insertPlanes);

            DA.SetData(0, customElements);
            DA.SetDataList(1, customElements.ResultMesh);
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
}