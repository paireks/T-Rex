using System.Collections.Generic;
using Rhino;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class FreeShape
    {
        readonly RhinoDoc _activeDoc = RhinoDoc.ActiveDoc;

        public FreeShape(List<Point3d> vertices, RebarProperties props)
        {
            Vertices = vertices;
            Props = props;
            RebarCurve = new PolylineCurve(Vertices);
            RebarCurve = Curve.CreateFilletCornersCurve(RebarCurve, Props.BendingRoller / 2.0 + Props.Diameter / 2.0,
                _activeDoc.ModelAbsoluteTolerance, _activeDoc.ModelAngleToleranceRadians);

            if (RebarCurve is PolyCurve polyCurve)
            {
                Curve[] segments = polyCurve.Explode();
                List<Curve> segmentsAsList = new List<Curve>();
                segmentsAsList.AddRange(segments);
                Segments = segmentsAsList;
            }

            List<Point3d> divisionPointsOfRebarCurve = new List<Point3d>();

            foreach (var segment in Segments)
            {
                if (segment.IsArc())
                {
                    segment.DivideByCount(10, false, out var divisionCurrentPoints);
                    divisionPointsOfRebarCurve.AddRange(divisionCurrentPoints);
                }
                else
                {
                    divisionPointsOfRebarCurve.Add(segment.PointAtStart);
                }
            }
            divisionPointsOfRebarCurve.Add(Segments[Segments.Count-1].PointAtEnd);

            DivisionPointsOfRebarCurve = divisionPointsOfRebarCurve;



            RebarMeshRepresentation mesh = new RebarMeshRepresentation(Props.Diameter);
            List<Point3d> sectionPoints = new List<Point3d>();

            for (int i = 0; i < mesh.SectionCoordinates[0].Length; i++)
            {
                sectionPoints.Add(new Point3d(mesh.SectionCoordinates[0][i], mesh.SectionCoordinates[1][i], 0));
            }


            SectionCoordinates = sectionPoints;

            List<Point3d> meshPoints = new List<Point3d>();

            for (int i = 0; i < DivisionPointsOfRebarCurve.Count - 1; i++)
            {
                Vector3d workVector = new Vector3d
                (
                    DivisionPointsOfRebarCurve[i + 1].X - DivisionPointsOfRebarCurve[i].X,
                    DivisionPointsOfRebarCurve[i + 1].Y - DivisionPointsOfRebarCurve[i].Y,
                    DivisionPointsOfRebarCurve[i + 1].Z - DivisionPointsOfRebarCurve[i].Z
                );
                Plane workPlane = new Plane(DivisionPointsOfRebarCurve[i], workVector);

                Transform changeBasis = Transform.ChangeBasis(workPlane, Plane.WorldXY);

                foreach (var point in SectionCoordinates)
                {
                    point.Transform(changeBasis);
                    meshPoints.Add(point);
                }
            }

            MeshPoints = meshPoints;
        }

        public List<Point3d> DivisionPointsOfRebarCurve { get; set; }
        public List<Point3d> MeshPoints { get; set; }
        public List<Curve> Segments { get; set; }
        public Curve RebarCurve { get; set; }
        public List<Point3d> Vertices { get; set; }
        public RebarProperties Props { get; set; }
        public List<Point3d> SectionCoordinates { get; set; }
    }
}
