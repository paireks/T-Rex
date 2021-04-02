using System.Collections.Generic;
using T_RexEngine;
using System;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace T_Rex
{
    public class CreateIfcGH : GH_Component
    {
        public CreateIfcGH()
          : base("Create IFC", "Create IFC",
              "Create IFC",
              "T-Rex", "IFC")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Groups", "Groups", "T-Rex groups to add to the IFC file",
                GH_ParamAccess.list);
            pManager.AddTextParameter("Path", "Path", "Path where the IFC file will be saved, should end up with .ifc",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<ElementGroup> elementGroups = new List<ElementGroup>();
            string path = string.Empty;

            DA.GetDataList(0, elementGroups);
            DA.GetData(1, ref path);
            
            Ifc Ifc = new Ifc(elementGroups, path);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.IFC;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("829950d4-9a32-415d-bd94-6d122eece5f1"); }
        }
    }
}