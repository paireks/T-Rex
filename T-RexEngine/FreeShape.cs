using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino;
using Rhino.Geometry;
using Rhino.UI.Controls.ThumbnailUI;

namespace T_RexEngine
{
    public class FreeShape
    {
        private RhinoDoc _activeDoc = RhinoDoc.ActiveDoc;

        public FreeShape(List<Point3d> vertices, RebarProperties props)
        {
            Vertices = vertices;
            Props = props;
            RebarCurve = new PolylineCurve(Vertices);
            RebarCurve = Curve.CreateFilletCornersCurve(RebarCurve, Props.BendingRoller, _activeDoc.ModelAbsoluteTolerance, _activeDoc.ModelAngleToleranceRadians);
            RebarBrep = Brep.CreatePipe(RebarCurve, Props.Diameter / 2.0, false, PipeCapMode.Flat, true,
                _activeDoc.ModelAbsoluteTolerance, _activeDoc.ModelAngleToleranceRadians);

            PolyCurve polyCurve = RebarCurve as PolyCurve;
            Curve[] segments = polyCurve.Explode();
            List<Curve> segmentsAsList = new List<Curve>();
            segmentsAsList.AddRange(segments);
            Segments = segmentsAsList;

            List<Point3d> divisionPointsOfRebarCurve = new List<Point3d>();

            foreach (var segment in Segments)
            {
                if (segment.IsArc())
                {
                    Point3d[] divisionCurrentPoints = new Point3d[11];
                    segment.DivideByCount(10, false, out divisionCurrentPoints);
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

        }

        public List<Point3d> DivisionPointsOfRebarCurve { get; set; }
        public List<Curve> Segments { get; set; }
        public Curve RebarCurve { get; set; }
        public List<Point3d> Vertices { get; set; }
        public Brep[] RebarBrep { get; set; }
        public RebarProperties Props { get; set; }
        public List<Point3d> SectionCoordinates { get; set; }
    }
}
