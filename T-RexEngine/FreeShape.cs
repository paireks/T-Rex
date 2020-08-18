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

        public FreeShape(List<Point3d> vertices)
        {
            Vertices = vertices;
            RebarCurve = new PolylineCurve(Vertices);
            RebarCurve = Curve.CreateFilletCornersCurve(RebarCurve, 10, _activeDoc.ModelAbsoluteTolerance, _activeDoc.ModelAngleToleranceRadians);
            RebarBrep = Brep.CreatePipe(RebarCurve, 20, false, PipeCapMode.Flat, true,
                _activeDoc.ModelAbsoluteTolerance, _activeDoc.ModelAngleToleranceRadians);
        }

        public Curve RebarCurve { get; set; }
        public List<Point3d> Vertices { get; set; }
        public Brep[] RebarBrep { get; set; }
    }
}
