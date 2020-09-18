using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class RebarShape
    {
        public RebarShape(RebarProperties props)
        {
            Props = props;
        }
        public void CurveToRebarShape(Curve rebarCurve)
        {
            RebarCurve = rebarCurve;
            RebarMesh = Mesh.CreateFromCurvePipe(RebarCurve, Props.Diameter / 2.0, 10, 70, MeshPipeCapStyle.Flat, false);
        }
        public void PolylineToRebarShape(Curve rebarCurve, int bendingRollerDiameter)
        {
            RhinoDoc activeDoc = RhinoDoc.ActiveDoc;

            RebarCurve = Curve.CreateFilletCornersCurve(rebarCurve, bendingRollerDiameter / 2.0 + Props.Diameter / 2.0, activeDoc.ModelAbsoluteTolerance, activeDoc.ModelAngleToleranceRadians);
            RebarMesh = Mesh.CreateFromCurvePipe(RebarCurve, Props.Diameter / 2.0, 10, 70, MeshPipeCapStyle.Flat, false);
        }

        public override string ToString()
        {
            return "Rebar Shape";
        }

        public Mesh RebarMesh { get; set; }
        public Curve RebarCurve { get; set; }
        public RebarProperties Props { get; set; }
    }
}
