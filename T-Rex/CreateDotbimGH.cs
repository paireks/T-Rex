﻿using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using T_RexEngine;

namespace T_Rex
{
    public class CreateDotbimGH : GH_Component
    {
        public CreateDotbimGH()
          : base("Create Dotbim", "Create Dotbim",
              "Creates Dotbim file. Make sure that groups are modelled in meters, because dotbim requires geometry modelled in meters." +
              "You can try to adjust Geometry Scale Factor if necessary.",
              "T-Rex", "Dotbim")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Groups", "Groups", "T-Rex groups to add to the dotbim file",
                GH_ParamAccess.list);
            pManager.AddTextParameter("Project Name", "Project Name", "Name of the project",
                GH_ParamAccess.item);
            pManager.AddTextParameter("Building Name", "Building Name", "Name of the building",
                GH_ParamAccess.item);
            pManager.AddNumberParameter("Geometry Scale Factor", "Geometry Scale Factor",
                "Scaling factor in all directions for geometry only. It won't scale any properties exported.",
                GH_ParamAccess.item);
            pManager.AddTextParameter("Path", "Path", "Path where the dotbim file will be saved, should end up with .bim",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<ElementGroup> elementGroups = new List<ElementGroup>();
            string projectName = string.Empty;
            string buildingName = string.Empty;
            double scaleFactor = double.NaN;
            string path = string.Empty;

            DA.GetDataList(0, elementGroups);
            DA.GetData(1, ref projectName);
            DA.GetData(2, ref buildingName);
            DA.GetData(3, ref scaleFactor);
            DA.GetData(4, ref path);
            
            Dotbim dotbim = new Dotbim(elementGroups, projectName, buildingName, scaleFactor, path);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Dotbim;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("bf7514f2-0455-4a27-8f1c-2b416dc96503"); }
        }
    }
}