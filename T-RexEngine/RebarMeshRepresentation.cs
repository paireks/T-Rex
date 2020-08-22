using System.Collections.Generic;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class RebarMeshRepresentation
    {
        public static List<Point3d> CreateSectionPoints(int diameter)
        {
            List<Point3d> sectionPoints = new List<Point3d>
            {
                new Point3d(0.433*diameter, 0.250*diameter, 0),
                new Point3d(0.250*diameter, 0.433*diameter, 0),
                new Point3d(0.000, 0.500*diameter, 0),
                new Point3d(-0.250*diameter, 0.433*diameter, 0),
                new Point3d(-0.433*diameter, 0.250*diameter, 0),
                new Point3d(-0.500*diameter, 0.000, 0),
                new Point3d(-0.433*diameter, -0.250*diameter, 0),
                new Point3d(-0.250*diameter, -0.433*diameter, 0),
                new Point3d(0.000, -0.500*diameter, 0),
                new Point3d(0.250*diameter, -0.433*diameter, 0),
                new Point3d(0.433*diameter, -0.250*diameter, 0),
                new Point3d(0.500*diameter, 0.000, 0)
            };

            return sectionPoints;
        }

        public static List<Point3d> CreateRebarMeshPoints(List<Point3d> sectionPoints,
            List<Point3d> curveDivisionPoints)
        {
            List<Point3d> rebarMeshPoints = new List<Point3d>();
            Vector3d workVector = new Vector3d();
            Plane workPlane;

            rebarMeshPoints.Add(curveDivisionPoints[0]);

            for (int i = 0; i < curveDivisionPoints.Count - 1; i++)
            {
                workVector = new Vector3d
                (
                    curveDivisionPoints[i + 1].X - curveDivisionPoints[i].X,
                    curveDivisionPoints[i + 1].Y - curveDivisionPoints[i].Y,
                    curveDivisionPoints[i + 1].Z - curveDivisionPoints[i].Z
                );
                workPlane = new Plane(curveDivisionPoints[i], workVector);
                rebarMeshPoints.AddRange(MoveXyPointsToAnotherPlane(sectionPoints, workPlane));
            }

            workPlane = new Plane(curveDivisionPoints[curveDivisionPoints.Count - 1], workVector);
            rebarMeshPoints.AddRange(MoveXyPointsToAnotherPlane(sectionPoints, workPlane));

            rebarMeshPoints.Add(curveDivisionPoints[curveDivisionPoints.Count - 1]);

            return rebarMeshPoints;
        }

        public static List<Point3d> MoveXyPointsToAnotherPlane(List<Point3d> pointsToMove, Plane destinationPlane)
        {
            List<Point3d> movedPoints = new List<Point3d>(); 

            Transform changeBasis = Transform.ChangeBasis(destinationPlane, Plane.WorldXY);

            foreach (var point in pointsToMove)
            {
                point.Transform(changeBasis);
                movedPoints.Add(point);
            }

            return movedPoints;
        }
    }
}
