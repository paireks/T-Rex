using System.Collections.Generic;
using Rhino;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class CurveToRebar
    {
        public CurveToRebar(Curve rebarCurve, RebarProperties props)
        {
            RebarCurve = rebarCurve;
            Props = props;

            RebarMesh = Mesh.CreateFromCurvePipe(RebarCurve, Props.Diameter / 2.0, 10, 70, MeshPipeCapStyle.Flat, false);

        }
        public Mesh RebarMesh { get; set; }
        public Curve RebarCurve { get; set; }
        public RebarProperties Props { get; set; }
    }
}
