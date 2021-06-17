using System.Collections.Generic;
using T_RexEngine;
using T_RexEngine.ElementLibrary;
using System;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino;
using Rhino.Geometry;
using T_RexEngine.Enums;

namespace T_Rex
{
    public class ProfileToElementsGH : GH_Component
    {
        private bool _useDegrees = false;
        public ProfileToElementsGH()
          : base("Profile To Elements", "Profile To Elements",
              "Create Elements from Profile",
              "T-Rex", "Concrete")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "Name", "Name of the elements", GH_ParamAccess.item);
            pManager.AddGenericParameter("Profile", "Profile", "Profile to create element from", GH_ParamAccess.item);
            pManager.AddAngleParameter("Rotation Angle", "Rotation Angle", "Set rotation angle for the profile",
                GH_ParamAccess.item);
            pManager.AddGenericParameter("Material", "Material", "Concrete element material", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Type", "Type", "Element type as integer", GH_ParamAccess.item);
            pManager.AddLineParameter("Insert Lines", "Insert Lines", "Lines to specify the element length and position",
                GH_ParamAccess.list);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element Group", "Element Group", "Concrete elements", GH_ParamAccess.item);
            pManager.AddBrepParameter("Breps", "Breps", "Breps that represent concrete elements", GH_ParamAccess.list);
        }
        
        protected override void BeforeSolveInstance()
        {
            base.BeforeSolveInstance();
            _useDegrees = false;
            if (Params.Input[2] is Param_Number angleParameter)
                _useDegrees = angleParameter.UseDegrees;
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string name = String.Empty;
            Profile profile = null;
            List<Line> lines = new List<Line>();
            double angle = 0.0;
            Material material = null;
            int type = 0;

            DA.GetData(0, ref name);
            DA.GetData(1, ref profile);
            if (!DA.GetData(2, ref angle)) return;
            if (_useDegrees)
                angle = RhinoMath.ToRadians(angle);
            DA.GetData(3, ref material);
            DA.GetData(4, ref type);
            DA.GetDataList(5, lines);

            ProfileToElements profileToElements = new ProfileToElements(name, profile, lines, angle, material, type);

            DA.SetData(0, profileToElements);
            DA.SetDataList(1, profileToElements.Breps);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.ProfileElements;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("a768f287-2cb4-42b1-b93c-ed03c4514f9e"); }
        }
    }
}