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
        public void VectorLengthSpacing(Vector3d startEndVector, double spacingLength, int spacingType)
        {
            if (startEndVector.Length < spacingLength)
            {
                throw new ArgumentException("Spacing Length should be smaller than Vector length");
            }

            double divisionOfVectorLengthAndSpacing = startEndVector.Length / spacingLength;
            Count = Convert.ToInt32(Math.Floor(divisionOfVectorLengthAndSpacing));
            double restOfDistance = (divisionOfVectorLengthAndSpacing - Count) * spacingLength;

            startEndVector.Unitize();
            Vector3d constantDistanceVector = startEndVector * spacingLength;
            Vector3d restDistanceVector = startEndVector * restOfDistance;
            Transform moveConstantValue = Transform.Translation(constantDistanceVector);
            Transform moveRestValue = Transform.Translation(restDistanceVector);

            RebarGroupMesh = new List<Mesh>();

            Mesh rebarShapeMesh = RebarShape.RebarMesh.DuplicateMesh();
            RebarGroupMesh.Add(rebarShapeMesh);
            Mesh duplicateMeshForTranslation = rebarShapeMesh.DuplicateMesh();
            Mesh duplicateMesh;

            if (spacingType == 0)
            {
                for (int i = 0; i < Count - 2; i++)
                {
                    duplicateMeshForTranslation.Transform(moveConstantValue);
                    duplicateMesh = duplicateMeshForTranslation.DuplicateMesh();
                    RebarGroupMesh.Add(duplicateMesh);
                }

                duplicateMeshForTranslation.Transform(moveRestValue);
                duplicateMesh = duplicateMeshForTranslation.DuplicateMesh();
                RebarGroupMesh.Add(duplicateMesh);
            }
            else if (spacingType == 1)
            {
                duplicateMeshForTranslation.Transform(moveRestValue);
                duplicateMesh = duplicateMeshForTranslation.DuplicateMesh();
                RebarGroupMesh.Add(duplicateMesh);

                for (int i = 0; i < Count - 2; i++)
                {
                    duplicateMeshForTranslation.Transform(moveConstantValue);
                    duplicateMesh = duplicateMeshForTranslation.DuplicateMesh();
                    RebarGroupMesh.Add(duplicateMesh);
                }
            }
            else if (spacingType == 2)
            {

            }
            else if (spacingType == 3)
            {

            }
            else
            {
                throw new ArgumentException("Spacing type should be between 0 and 3");
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

        public double Volume
        {
            get
            {
                return Count * RebarShape.RebarCurve.GetLength() * Math.PI * Math.Pow(RebarShape.Props.Radius, 2.0);
            }
        }

        public double Weight
        {
            get
            {
                return Count * Volume * RebarShape.Props.Material.Density;
            }
        }
    }
}
