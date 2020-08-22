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
            if (Vertices.Count > 2)
            {
                RebarCurve = Curve.CreateFilletCornersCurve(RebarCurve, Props.BendingRoller / 2.0 + Props.Diameter / 2.0,
                    _activeDoc.ModelAbsoluteTolerance, _activeDoc.ModelAngleToleranceRadians);
            }
            List<Curve> segments = RebarCurveTools.ExplodeIntoSegments(RebarCurve);
            List<Point3d> divisionPointsOfRebarCurve = RebarCurveTools.DivideSegmentsToPoints(segments);
            List<Point3d> sectionPoints = RebarMeshRepresentation.CreateSectionPoints(Props.Diameter);
            List<Point3d> rebarMeshPoints = RebarMeshRepresentation.CreateRebarMeshPoints(sectionPoints, divisionPointsOfRebarCurve);

            MeshPoints = rebarMeshPoints;
        }

        public List<Point3d> MeshPoints { get; set; }
        public Curve RebarCurve { get; set; }
        public List<Point3d> Vertices { get; set; }
        public RebarProperties Props { get; set; }
    }
}
