using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class MaterialGH : GH_Component
    {
        public MaterialGH()
          : base("Material", "Material",
              "Creates material of the element",
              "T-Rex", "Properties")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "Name", "Name of the material", GH_ParamAccess.item, "Reinforcement steel");
            pManager.AddTextParameter("Grade", "Grade", "Grade of the material", GH_ParamAccess.item, "B500SP");
            pManager.AddIntegerParameter("Density", "Density", "Density in kg/m3 as integer", GH_ParamAccess.item);
            pManager.AddNumberParameter("Price Of Kilo", "Price Of Kilo", "Price of the kilogram of this material",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Material", "Material", "Created material", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string name = string.Empty;
            string grade = string.Empty;
            int density = 0;
            double priceOfKilo = 0;

            DA.GetData(0, ref name);
            DA.GetData(1, ref grade);
            DA.GetData(2, ref density);
            DA.GetData(3, ref priceOfKilo);

            Material material = new Material(name, grade, density, priceOfKilo);

            DA.SetData(0, material);
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
            get { return new Guid("866eec60-6ee9-4f87-8172-57c2f7134f65"); }
        }
    }
}