using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class RebarGroup
    {
        private int _count;

        public RebarGroup(RebarShape rebarShape)
        {
            RebarShape = rebarShape;
        }
        public void VectorSpacing(int count, Vector3d spaceVector)
        {
            Count = count;
            Transform moveMesh = Transform.Translation(spaceVector);

            Mesh rebarShapeMesh = RebarShape.RebarMesh.DuplicateMesh();
            RebarGroupMesh = new List<Mesh>();
            RebarGroupMesh.Add(rebarShapeMesh.DuplicateMesh());

            for (int i = 0; i < count - 1; i++)
            {
                rebarShapeMesh.Transform(moveMesh);
                Mesh duplicateMesh = rebarShapeMesh.DuplicateMesh();
                RebarGroupMesh.Add(duplicateMesh);
            }
        }
        public void CurveSpacing(int count, Curve spaceCurve)
        {
            Count = count;
            double[] divideParameters = spaceCurve.DivideByCount(Count - 1, true);
            Plane[] perpendicularPlanes = spaceCurve.GetPerpendicularFrames(divideParameters);

            RebarGroupMesh = new List<Mesh>();

            foreach (var plane in perpendicularPlanes)
            {
                Transform planeToPlane = Transform.PlaneToPlane(RebarShape.RebarPlane, plane);
                Mesh rebarShapeMesh = RebarShape.RebarMesh.DuplicateMesh();
                rebarShapeMesh.Transform(planeToPlane);
                RebarGroupMesh.Add(rebarShapeMesh);
            }
        }
        public override string ToString()
        {
            return "Rebar Group";
        }

        public RebarShape RebarShape { get; set; }
        public int Count
        {
            get { return _count; }
            set
            {
                if (value > 1)
                {
                    _count = value;
                }
                else
                {
                    throw new ArgumentException("Count parameter should be larger than 1");
                }
            }
        }
        public List<Mesh> RebarGroupMesh { get; set; }
    }
}
