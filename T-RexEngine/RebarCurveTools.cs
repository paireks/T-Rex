using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class RebarCurveTools
    {
        public static List<Curve> ExplodeIntoSegments(Curve curveToDivideIntoSegments)
        {
            List<Curve> segmentsAsList = new List<Curve>();

            if (curveToDivideIntoSegments is PolyCurve polyCurve)
            {
                Curve[] segments = polyCurve.Explode();
                segmentsAsList.AddRange(segments);
            }
            else
            {
                segmentsAsList.Add(curveToDivideIntoSegments);
            }

            return segmentsAsList;
        }

        public static List<Point3d> DivideSegmentsToPoints(List<Curve> segmentsToDivide)
        {
            List<Point3d> pointsOfDivisions = new List<Point3d>();

            foreach (var segment in segmentsToDivide)
            {
                if (segment.IsArc())
                {
                    segment.DivideByCount(10, false, out var points);
                    pointsOfDivisions.AddRange(points);
                }
                else
                {
                    pointsOfDivisions.Add(segment.PointAtStart);
                }
            }

            pointsOfDivisions.Add(segmentsToDivide[segmentsToDivide.Count-1].PointAtEnd);

            return pointsOfDivisions;
        }
    }
}
