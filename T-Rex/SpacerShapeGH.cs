using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class SpacerShapeGH : GH_Component
    {
        public SpacerShapeGH()
          : base("Spacer Shape", "Spacer Shape",
              "Create Spacer Shape",
              "T-Rex", "Rebar Shape")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("Plane", "Plane", "Plane where the shape will be inserted",
                GH_ParamAccess.item);
            pManager.AddNumberParameter("Height", "Height", "Height of a spacer", GH_ParamAccess.item);
            pManager.AddNumberParameter("Length", "Length", "Length of a spacer", GH_ParamAccess.item);
            pManager.AddNumberParameter("Width", "Width", "Width of a spacer", GH_ParamAccess.item);
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
            double length = 0.0;
            double width = 0.0;
            RebarProperties properties = null;
            double bendingRollerDiameter = 0.0;

            DA.GetData(0, ref insertPlane);
            DA.GetData(1, ref height);
            DA.GetData(2, ref length);
            DA.GetData(3, ref width);
            DA.GetData(4, ref properties);
            DA.GetData(5, ref bendingRollerDiameter);

            RebarShape rebarShape = new RebarShape(properties);
            rebarShape.SpacerShape(insertPlane, height, length, width, bendingRollerDiameter);

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
                return Properties.Resources.SpacerShape;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("16e6c58a-ceec-492b-8ddc-b2c79ab2f22c"); }
        }
    }
}