using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class StirrupBarShapeGH : GH_Component
    {
        public StirrupBarShapeGH()
          : base("Stirrup Shape", "Stirrup Shape",
              "Create Stirrup Shape",
              "T-Rex", "Rebar Shape")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("Plane", "Plane", "Plane where the shape will be inserted",
                GH_ParamAccess.item);
            pManager.AddNumberParameter("Height", "Height", "Height of a spacer", GH_ParamAccess.item);
            pManager.AddNumberParameter("Width", "Width", "Width of a spacer", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Hooks Type", "Hooks Type",
                "0 = 90-angle, 1 = 135-angle",
                GH_ParamAccess.item, 0);
            pManager.AddNumberParameter("Hook Length", "Hook Length", "Length of a hook", GH_ParamAccess.item);
            pManager.AddGenericParameter("Properties", "Properties", "Reinforcement properties", GH_ParamAccess.item);
            pManager.AddNumberParameter("Bending Roller Diameter", "Bending Roller Diameter",
                "Bending roller diameter", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Shape", "Rebar Shape", "Reinforcement bar shape", GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh that represents reinforcement", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Plane insertPlane = Plane.Unset;
            double height = 0.0;
            double width = 0.0;
            int hooksType = 0;
            double hookLength = 0.0;
            RebarProperties properties = null;
            double bendingRollerDiameter = 0.0;

            DA.GetData(0, ref insertPlane);
            DA.GetData(1, ref height);
            DA.GetData(2, ref width);
            DA.GetData(3, ref hooksType);
            DA.GetData(4, ref hookLength);
            DA.GetData(5, ref properties);
            DA.GetData(6, ref bendingRollerDiameter);

            RebarShape rebarShape = new RebarShape(properties);
            rebarShape.BuildStirrupShape(insertPlane, height, width, bendingRollerDiameter, hooksType, hookLength);

            DA.SetData(0, rebarShape);
            DA.SetData(1, rebarShape.RebarMesh);
        }
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.StirrupBarShape;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("f88fad10-bc14-45a6-b67c-77f1e71761f8"); }
        }
    }
}