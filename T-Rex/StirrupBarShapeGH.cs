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
          : base("Stirrup Bar Shape", "Stirrup Bar Shape",
              "Create Stirrup Bar Shape",
              "T-Rex", "Rebar Shape")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddRectangleParameter("Rectangle", "Rectangle", "Boundary rectangle when rebar will be placed",
                GH_ParamAccess.item);
            pManager.AddGenericParameter("Properties", "Properties", "Reinforcement properties", GH_ParamAccess.item);
            pManager.AddNumberParameter("Bending Roller Diameter", "Bending Roller Diameter",
                "Bending roller diameter", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Hooks Corner", "Hooks Corner",
                "0 = top-left corner, 1 = top-right corner, 2 = bottom-right corner, 3 = bottom-left corner",
                GH_ParamAccess.item, 0);
            pManager.AddIntegerParameter("Hooks Type", "Hooks Type",
                "0 = 90-angle, 1 = 135-angle",
                GH_ParamAccess.item, 0);
            pManager.AddGenericParameter("Cover Dimensions", "Cover Dimensions", "Dimensions of a concrete cover",
                GH_ParamAccess.item);
            pManager.AddNumberParameter("Hook Length", "Hook Length", "Length of a hook", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Shape", "Rebar Shape", "Reinforcement bar shape", GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Desc", GH_ParamAccess.item);
            pManager.AddPointParameter("X", "X", "Desc", GH_ParamAccess.list);
            pManager.AddNumberParameter("Diameter", "Diameter", "Desc", GH_ParamAccess.item);
            pManager.AddNumberParameter("Radius", "Radius", "Desc", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Rectangle3d rectangle = Rectangle3d.Unset;
            RebarProperties properties = null;
            double bendingRollerDiameter = 0.0;
            int hooksCorner = 0;
            int hooksType = 0;
            CoverDimensions coverDimensions = null;
            double hookLength = 0.0;

            DA.GetData(0, ref rectangle);
            DA.GetData(1, ref properties);
            DA.GetData(2, ref bendingRollerDiameter);
            DA.GetData(3, ref hooksCorner);
            DA.GetData(4, ref hooksType);
            DA.GetData(5, ref coverDimensions);
            DA.GetData(6, ref hookLength);

            RebarShape rebarShape = new RebarShape(properties);
            rebarShape.StirrupBarShape(rectangle, properties, bendingRollerDiameter, hooksCorner, hooksType, coverDimensions, hookLength);

            DA.SetData(0, rebarShape);
            DA.SetData(1, rebarShape.RebarMesh);
            DA.SetDataList(2, rebarShape.X);
            DA.SetData(3, rebarShape.Diameter);
            DA.SetData(4, rebarShape.Radius);
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
            get { return new Guid("4ca87b9a-3455-4285-9c66-a96310c1a089"); }
        }
    }
}