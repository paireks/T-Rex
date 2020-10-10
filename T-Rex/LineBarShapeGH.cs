﻿using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class LineBarShapeGH : GH_Component
    {
        public LineBarShapeGH()
          : base("Line Bar Shape", "Line Bar Shape",
              "Create Line Bar Shape",
              "T-Rex", "Rebar Shape")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddRectangleParameter("Rectangle", "Rectangle", "Boundary rectangle when rebar will be placed",
                GH_ParamAccess.item);
            pManager.AddGenericParameter("Properties", "Properties", "Reinforcement properties", GH_ParamAccess.item);
            pManager.AddBooleanParameter("IsBottom", "IsBottom",
                "If true = Bar will be placed at the bottom of the rectangle, false = at the top",
                GH_ParamAccess.item, true);
            pManager.AddGenericParameter("Cover Dimensions", "Cover Dimensions", "Dimensions of a concrete cover",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Shape", "Rebar Shape", "Reinforcement bar shape", GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh that represents reinforcement", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Rectangle3d rectangle = Rectangle3d.Unset;
            RebarProperties properties = null;
            bool isBottom = true;
            CoverDimensions coverDimensions = null;

            DA.GetData(0, ref rectangle);
            DA.GetData(1, ref properties);
            DA.GetData(2, ref isBottom);
            DA.GetData(3, ref coverDimensions);

            RebarShape rebarShape = new RebarShape(properties);
            rebarShape.LineBarShape(rectangle, properties, isBottom, coverDimensions);

            DA.SetData(0, rebarShape);
            DA.SetData(1, rebarShape.RebarMesh);
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
            get { return new Guid("ee39b6ee-0fbf-406c-a20c-b6ed02e4ebd2"); }
        }
    }
}