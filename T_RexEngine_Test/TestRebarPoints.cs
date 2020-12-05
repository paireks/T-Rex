using System;
using System.Collections.Generic;
using T_RexEngine;
using Xunit;
using Rhino.Geometry;

namespace T_RexEngine_Test
{
    public class TestRebarPoints
    {
        #region TestCreateForLineFromRectangle
        
        Rectangle3d rectangle = new Rectangle3d(Plane.WorldXY, Point3d.Origin, new Point3d(0.500, 0.400, 0));
        CoverDimensions coverDimensions = new CoverDimensions(0.015, 0.020, 0.025, 0.030);
        RebarProperties props = new RebarProperties(0.012, new Material("Name", "Grade", 7850));

        [Fact]
        public void TestCreateForLineFromRectangle_Position0()
        {
            int position = 0;
            List<Point3d> actualPoints = RebarPoints.CreateForLineFromRectangle(rectangle, position, coverDimensions, props);
            List<Point3d> expectedPoints = new List<Point3d>{new Point3d(0.015,0.369,0), new Point3d(0.48,0.369,0)};
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }
        [Fact]
        public void TestCreateForLineFromRectangle_Position1()
        {
            int position = 1;
            List<Point3d> actualPoints = RebarPoints.CreateForLineFromRectangle(rectangle, position, coverDimensions, props);
            List<Point3d> expectedPoints = new List<Point3d>{new Point3d(0.474,0.375,0), new Point3d(0.474,0.03,0)};
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }
        [Fact]
        public void TestCreateForLineFromRectangle_Position2()
        {
            int position = 2;
            List<Point3d> actualPoints = RebarPoints.CreateForLineFromRectangle(rectangle, position, coverDimensions, props);
            List<Point3d> expectedPoints = new List<Point3d>{new Point3d(0.015,0.036,0), new Point3d(0.48,0.036,0)};
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }
        [Fact]
        public void TestCreateForLineFromRectangle_Position3()
        {
            int position = 3;
            List<Point3d> actualPoints = RebarPoints.CreateForLineFromRectangle(rectangle, position, coverDimensions, props);
            List<Point3d> expectedPoints = new List<Point3d>{new Point3d(0.021,0.03,0), new Point3d(0.021,0.375,0)};
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }
        [Fact]
        public void CheckExceptions_Position4()
        {
            var exception = Assert.Throws<ArgumentException>(() => RebarPoints.CreateForLineFromRectangle(rectangle, 4, coverDimensions, props));
            Assert.Equal("Position should be between 0 and 3", exception.Message);
        }
        
        #endregion

        
    }
}