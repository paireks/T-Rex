using System;
using System.Collections.Generic;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class RebarSpacing
    {
        public RebarSpacing(RebarShape rebarShape, Plane rebarPlane, int count, Curve spaceCurve, double angle)
        {
            OriginRebarShape = rebarShape;
            UseCurveSpacing(rebarPlane, count, spaceCurve, angle);
        }

        public RebarSpacing(RebarShape rebarShape, Vector3d startEndVector, int count)
        {
            OriginRebarShape = rebarShape;
            UseVectorCountSpacing(startEndVector, count);
        }

        public RebarSpacing(RebarShape rebarShape, Vector3d startEndVector, double spacingLength, int spacingType, double tolerance)
        {
            OriginRebarShape = rebarShape;
            UseVectorLengthSpacing(startEndVector, spacingLength, spacingType, tolerance);
        }
        
        private void UseCurveSpacing(Plane rebarPlane, int count, Curve spaceCurve, double angle)
        {
            if (spaceCurve.GetLength() <= 0)
            {
                throw new ArgumentException("Length of the Curve can't be 0");
            }
            if (count <= 1)
            {
                throw new ArgumentException("Count parameter should be larger than 1");
            }
            Count = count;

            double[] divideParameters;
            
            if (spaceCurve.IsClosed)
            {
                divideParameters = spaceCurve.DivideByCount(Count, true);
            }
            else
            {
                divideParameters = spaceCurve.DivideByCount(Count - 1, true);
            }
            Plane[] perpendicularPlanes = spaceCurve.GetPerpendicularFrames(divideParameters);

            RebarGroupMesh = new List<Mesh>();

            foreach (var plane in perpendicularPlanes)
            {
                plane.Rotate(angle, plane.ZAxis);
                Transform planeToPlane = Transform.PlaneToPlane(rebarPlane, plane);
                Mesh rebarShapeMesh = OriginRebarShape.RebarMesh.DuplicateMesh();
                rebarShapeMesh.Transform(planeToPlane);
                RebarGroupMesh.Add(rebarShapeMesh);
            }
        }
        
        public void UseVectorCountSpacing(Vector3d startEndVector, int count)
        {
            if (count <= 1)
            {
                throw new ArgumentException("Count parameter should be larger than 1");
            }
            Count = count;
            double lengthFromStartToEnd = startEndVector.Length;
            double spacingLength = lengthFromStartToEnd / (Convert.ToDouble(Count) - 1);
            startEndVector.Unitize();
            Vector3d constantDistanceVector = startEndVector * spacingLength;
            Transform moveConstantValue = Transform.Translation(constantDistanceVector);
            Mesh rebarShapeMesh = OriginRebarShape.RebarMesh.DuplicateMesh();

            RebarGroupMesh = new List<Mesh> {rebarShapeMesh};

            Mesh duplicateMeshForTranslation = rebarShapeMesh.DuplicateMesh();
            Mesh duplicateMesh;
            for (int i = 0; i < Count - 1; i++)
            {
                duplicateMeshForTranslation.Transform(moveConstantValue);
                duplicateMesh = duplicateMeshForTranslation.DuplicateMesh();
                RebarGroupMesh.Add(duplicateMesh);
            }
        }
        
        public void UseVectorLengthSpacing(Vector3d startEndVector, double spacingLength, int spacingType, double tolerance)
        {
            if (tolerance <= 0)
            {
                throw new ArgumentException("Tolerance should be a small number, but can't be 0 or negative. For meters, centimeters and millimeters the value 0.0001 should be sufficient for most of the cases. If you want to understand it better - analyze the source code.");
            }
            
            if (startEndVector.Length < spacingLength)
            {
                throw new ArgumentException("Spacing Length should be smaller than Vector length");
            }

            double lengthFromStartToEnd = startEndVector.Length;
            double restOfDistance = lengthFromStartToEnd % spacingLength;
            double halfRestOfDistance = restOfDistance / 2.0;
            
            RebarGroupMesh = new List<Mesh>();
            Count = 0;
            
            Mesh rebarShapeMesh = OriginRebarShape.RebarMesh.DuplicateMesh();
            if (spacingType == 1)
            {
                rebarShapeMesh.Transform(new Transform(Transform.Translation(startEndVector)));
            }
            RebarGroupMesh.Add(rebarShapeMesh);
            Count += 1;

            if (spacingType == 1)
            {
                startEndVector.Reverse();
            }

            startEndVector.Unitize();
            Vector3d constantDistanceVector = startEndVector * spacingLength;
            Vector3d restDistanceVector = startEndVector * restOfDistance;
            Vector3d halfOfRestDistanceVector = startEndVector * halfRestOfDistance;
            Transform moveConstantValue = Transform.Translation(constantDistanceVector);
            Transform moveRestValue = Transform.Translation(restDistanceVector);
            Transform moveHalfOfRestValue = Transform.Translation(halfOfRestDistanceVector);

            Mesh duplicateMeshForTranslation = rebarShapeMesh.DuplicateMesh();
            Mesh duplicateMesh;
            double distanceToCover = lengthFromStartToEnd;

            switch (spacingType)
            {
                case 0:
                case 1:
                {
                    while (distanceToCover > restOfDistance + tolerance)
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
                case 2:
                {
                    if (restOfDistance + tolerance > spacingLength)
                    {
                        goto case 0;
                    }

                    if (restOfDistance > tolerance)
                    {
                        duplicateMeshForTranslation.Transform(moveHalfOfRestValue);
                        duplicateMesh = duplicateMeshForTranslation.DuplicateMesh();
                        RebarGroupMesh.Add(duplicateMesh);
                        distanceToCover -= halfRestOfDistance;
                        Count += 1;
                    }
                    
                    while (distanceToCover > halfRestOfDistance + tolerance)
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

                    if (restOfDistance > tolerance && restOfDistance + tolerance < spacingLength)
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
        
        public int Count { get; private set; }
        public List<Mesh> RebarGroupMesh { get; private set; }
        public RebarShape OriginRebarShape { get; }
        public double Volume => Count * OriginRebarShape.RebarCurve.GetLength() * Math.PI * Math.Pow(OriginRebarShape.Props.Radius, 2.0);
        public double Weight => Volume * OriginRebarShape.Props.Material.Density;
    }
}