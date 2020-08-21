﻿using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class RebarPropertiesGH : GH_Component
    {
        public RebarPropertiesGH()
          : base("Rebar Properties", "Rebar Properties",
              "Properties for a reinforcement bar",
              "T-Rex", "Properties")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Diameter", "Diameter", "Diameter of the bar", GH_ParamAccess.item);
            pManager.AddTextParameter("Material", "Material", "Material of the rebar", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Bending Roller", "Bending Roller", "Diameter of the bending roller",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Properties", "Properties", "Properties of the bar", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int diameter = 0;
            string material = String.Empty;
            int bendingRoller = 0;

            DA.GetData(0, ref diameter);
            DA.GetData(1, ref material);
            DA.GetData(2, ref bendingRoller);

            RebarProperties prop = new RebarProperties(diameter, material, bendingRoller);

            DA.SetData(0, prop);
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
            get { return new Guid("2be43588-53c8-4cdf-b63f-e378736d9540"); }
        }
    }
}