using System;
using System.Collections.Generic;
using System.ComponentModel;
using Rhino.Geometry;
using T_RexEngine;
using Xunit;

namespace T_RexEngine_Test
{
    [Collection("Rhino Collection")]
    public class Test_RebarCurveTools
    {
        [Fact]
        public void Test_ExplodeIntoSegments_3PointCurve_2Segments()
        {
            Curve rebarCurve = new PolylineCurve(new List<Point3d>
            {
                new Point3d(0, 0, 0), new Point3d(10,0,0), new Point3d(0,10,0)
            });
            rebarCurve = Curve.CreateFilletCornersCurve(rebarCurve, 10, 0.001, 0.1);

            List<Curve> segmentsToTest = RebarCurveTools.ExplodeIntoSegments(rebarCurve);

            Assert.Equal(2, segmentsToTest.Count);
        }

        [Fact]
        public void Test_ExplodeIntoSegments_2PointLine_1Segment()
        {
            Curve rebarCurve = new PolylineCurve(new List<Point3d>
            {
                new Point3d(0, 0, 0), new Point3d(10,0,0)
            });
            rebarCurve = Curve.CreateFilletCornersCurve(rebarCurve, 10, 0.001, 0.1);

            List<Curve> segmentsToTest = RebarCurveTools.ExplodeIntoSegments(rebarCurve);

            Assert.Single(segmentsToTest);
        }

        [Fact]
        public void Test_DivideSegmentsToPoints_1Line_2Points()
        {
            Curve lineCurve = new LineCurve(new Point3d(0,0,0), new Point3d(10,0,0));
            List<Curve> segments = new List<Curve>{lineCurve};
            List<Point3d> points = RebarCurveTools.DivideSegmentsToPoints(segments);

            Assert.Equal(2, points.Count);
        }

        [Fact]
        public void Test_DivideSegmentsToPoints_1Arc_10Points()
        {
            Curve arcCurve = new ArcCurve(new Arc(
                new Point3d(0,0,0),
                new Point3d(0, 10,0),
                new Point3d(10, 0, 0)));
            List<Curve> segments = new List<Curve> { arcCurve };
            List<Point3d> points = RebarCurveTools.DivideSegmentsToPoints(segments);

            Assert.Equal(10, points.Count);
        }
    }
}
