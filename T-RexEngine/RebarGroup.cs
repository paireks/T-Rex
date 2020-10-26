using System;
using System.Collections.Generic;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class RebarGroup
    {
        public RebarGroup(RebarShape rebarShape)
        {
            RebarShape = rebarShape;
        }
        public void VectorSpacing(int count, Vector3d spaceVector)
        {
            if (count <= 1)
            {
                throw new ArgumentException("Count parameter should be larger than 1");
            }
            Count = count;
            Transform moveMesh = Transform.Translation(spaceVector);

            Mesh rebarShapeMesh = RebarShape.RebarMesh.DuplicateMesh();
            RebarGroupMesh = new List<Mesh> {rebarShapeMesh.DuplicateMesh()};

            for (int i = 0; i < count - 1; i++)
            {
                rebarShapeMesh.Transform(moveMesh);
                Mesh duplicateMesh = rebarShapeMesh.DuplicateMesh();
                RebarGroupMesh.Add(duplicateMesh);
            }
        }
        public void CurveSpacing(int count, Curve spaceCurve)
        {
            if (count <= 1)
            {
                throw new ArgumentException("Count parameter should be larger than 1");
            }
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
        public void VectorLengthSpacing(Vector3d startEndVector, double spacingLength, int spacingType, double tolerance)
        {
            if (startEndVector.Length < spacingLength)
            {
                throw new ArgumentException("Spacing Length should be smaller than Vector length");
            }

            double lengthFromStartToEnd = startEndVector.Length;
            double restOfDistance = lengthFromStartToEnd % spacingLength;
            double halfRestOfDistance = restOfDistance / 2.0;

            startEndVector.Unitize();
            Vector3d constantDistanceVector = startEndVector * spacingLength;
            Vector3d restDistanceVector = startEndVector * restOfDistance;
            Vector3d halfOfRestDistanceVector = startEndVector * halfRestOfDistance;
            Transform moveConstantValue = Transform.Translation(constantDistanceVector);
            Transform moveRestValue = Transform.Translation(restDistanceVector);
            Transform moveHalfOfRestValue = Transform.Translation(halfOfRestDistanceVector);

            RebarGroupMesh = new List<Mesh>();
            Count = 0;

            Mesh rebarShapeMesh = RebarShape.RebarMesh.DuplicateMesh();
            RebarGroupMesh.Add(rebarShapeMesh);
            Count += 1;
            
            Mesh duplicateMeshForTranslation = rebarShapeMesh.DuplicateMesh();
            Mesh duplicateMesh;
            double distanceToCover = lengthFromStartToEnd;

            switch (spacingType)
            {
                case 0:
                {
                    while (distanceToCover > restOfDistance)
                    {
                        duplicateMeshForTranslation.Transform(moveConstantValue);
                        duplicateMesh = duplicateMeshForTranslation.DuplicateMesh();
                        RebarGroupMesh.Add(duplicateMesh);
                        distanceToCover -= spacingLength;
                        Count += 1;
                    }

                    if (restOfDistance > tolerance)
                    {
                        duplicateMeshForTranslation.Transform(moveRestValue);
                        duplicateMesh = duplicateMeshForTranslation.DuplicateMesh();
                        RebarGroupMesh.Add(duplicateMesh);
                        distanceToCover -= restOfDistance;
                        Count += 1;
                    }

                    break;
                }
                case 1:
                {
                    if (restOfDistance > tolerance)
                    {
                        duplicateMeshForTranslation.Transform(moveRestValue);
                        duplicateMesh = duplicateMeshForTranslation.DuplicateMesh();
                        RebarGroupMesh.Add(duplicateMesh);
                        distanceToCover -= restOfDistance;
                        Count += 1;
                    }

                    while (distanceToCover > restOfDistance)
                    {
                        duplicateMeshForTranslation.Transform(moveConstantValue);
                        duplicateMesh = duplicateMeshForTranslation.DuplicateMesh();
                        RebarGroupMesh.Add(duplicateMesh);
                        distanceToCover -= spacingLength;
                        Count += 1;
                    }

                    break;
                }
                case 2:
                {
                    if (restOfDistance > tolerance)
                    {
                        duplicateMeshForTranslation.Transform(moveHalfOfRestValue);
                        duplicateMesh = duplicateMeshForTranslation.DuplicateMesh();
                        RebarGroupMesh.Add(duplicateMesh);
                        distanceToCover -= halfRestOfDistance;
                        Count += 1;
                    }
                    
                    while (distanceToCover > halfRestOfDistance)
                    {
                        duplicateMeshForTranslation.Transform(moveConstantValue);
                        duplicateMesh = duplicateMeshForTranslation.DuplicateMesh();
                        RebarGroupMesh.Add(duplicateMesh);
                        distanceToCover -= spacingLength;
                        Count += 1;
                    }

                    if (restOfDistance > tolerance)
                    {
                        duplicateMeshForTranslation.Transform(moveHalfOfRestValue);
                        duplicateMesh = duplicateMeshForTranslation.DuplicateMesh();
                        RebarGroupMesh.Add(duplicateMesh);
                        distanceToCover -= halfRestOfDistance;
                        Count += 1;
                    }

                    break;
                }
                case 3:

                    Count = Convert.ToInt32(Math.Floor( lengthFromStartToEnd / spacingLength ));

                    if (restOfDistance > tolerance)
                    {
                        Count += 1;
                    }

                    double actualSmallerSpacingLength = lengthFromStartToEnd / Convert.ToDouble(Count);
                    
                    Vector3d actualSmallerSpacingVector = startEndVector * actualSmallerSpacingLength;
                    Transform moveSmallerConstantValue = Transform.Translation(actualSmallerSpacingVector);

                    for (int i = 0; i < Count; i++)
                    {
                        duplicateMeshForTranslation.Transform(moveSmallerConstantValue);
                        duplicateMesh = duplicateMeshForTranslation.DuplicateMesh();
                        RebarGroupMesh.Add(duplicateMesh);
                    }

                    Count += 1;
                    
                    break;
                
                default:
                    throw new ArgumentException("Spacing type should be between 0 and 3");
            }
        }
        public override string ToString()
        {
            return "Rebar Group";
        }

        public RebarShape RebarShape { get; }
        public int Count { get; private set; }
        public List<Mesh> RebarGroupMesh { get; private set; }
        public double Volume => Count * RebarShape.RebarCurve.GetLength() * Math.PI * Math.Pow(RebarShape.Props.Radius, 2.0);
        public double Weight => Count * Volume * RebarShape.Props.Material.Density;
    }
}
