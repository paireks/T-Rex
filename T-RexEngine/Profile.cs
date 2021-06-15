using System;
using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class Profile
    {
        private List<Point3d> _profilePoints;
        private double _tolerance;
        private Brep[] _breps;
        
        public Profile(string name, List<Point3d> points, double tolerance)
        {
            Name = name;
            Tolerance = tolerance;
            ProfilePoints = points;

            Polyline polyline = ProfilePointsToPolyline(ProfilePoints);
            List<Curve> curves = PolylineToListOfCurves(polyline);

            BoundarySurfaces = Brep.CreatePlanarBreps(curves, tolerance);
            ProfileCurve = polyline.ToNurbsCurve();
        }

        public Profile(string name, double height, double width, double tolerance)
        {
            Name = name;
            Tolerance = tolerance;

            Point3d topRightPoint = new Point3d(width/2.0, height/2.0, 0);
            Point3d bottomRightPoint = new Point3d(width/2.0, -height/2.0, 0);
            Point3d bottomLeftPoint = new Point3d(-width/2.0, -height/2.0, 0);
            Point3d topLeftPoint = new Point3d(-width/2.0, height/2.0, 0);
            
            ProfilePoints = new List<Point3d>{topRightPoint, bottomRightPoint, bottomLeftPoint, topLeftPoint};
            
            Polyline polyline = ProfilePointsToPolyline(ProfilePoints);
            List<Curve> curves = PolylineToListOfCurves(polyline);

            BoundarySurfaces = Brep.CreatePlanarBreps(curves, tolerance);
            ProfileCurve = polyline.ToNurbsCurve();
        }

        public Profile(string name, int type, double height, double flangeHeight, double webWidth, double flangeWidth, double tolerance)
        {
            Name = name;
            Tolerance = tolerance;

            switch (type)
            {
                case 0:
                    ProfilePoints = CreatePointsForTShape(height, flangeHeight, webWidth, flangeWidth);
                    break;
                case 1:
                    ProfilePoints = CreatePointsForLShape(height, flangeHeight, webWidth, flangeWidth);
                    break;
                default:
                    throw new ArgumentException("Profile type not recognized");
            }
            
            Polyline polyline = ProfilePointsToPolyline(ProfilePoints);
            List<Curve> curves = PolylineToListOfCurves(polyline);

            BoundarySurfaces = Brep.CreatePlanarBreps(curves, tolerance);
            ProfileCurve = polyline.ToNurbsCurve();
        }

        public override string ToString()
        {
            return $"Profile{Environment.NewLine}" + $"Name: {Name}";
        }

        private static List<Point3d> CreatePointsForTShape(double height, double flangeHeight, double webWidth, double flangeWidth)
        {
            Point3d topRightPoint = new Point3d(flangeWidth/2.0, height/2.0, 0);
            Point3d flangeBottomRightPoint = new Point3d(flangeWidth/2.0, height/2.0 - flangeHeight,0);
            Point3d webTopRightPoint = new Point3d(webWidth/2.0, height/2.0 - flangeHeight, 0);
            Point3d webBottomRightPoint = new Point3d(webWidth/2.0, - height/2.0, 0);
            
            Point3d webBottomLeftPoint = new Point3d(- webWidth/2.0, - height/2.0, 0);
            Point3d webTopLeftPoint = new Point3d(- webWidth/2.0, height/2.0 - flangeHeight, 0);
            Point3d flangeBottomLeftPoint = new Point3d(- flangeWidth/2.0, height/2.0 - flangeHeight,0);
            Point3d topLeftPoint = new Point3d(- flangeWidth/2.0, height/2.0, 0);
            
            List<Point3d> points = new List<Point3d>{topRightPoint, flangeBottomRightPoint,
                                                     webTopRightPoint, webBottomRightPoint,
                                                     webBottomLeftPoint, webTopLeftPoint,
                                                     flangeBottomLeftPoint, topLeftPoint};

            return points;
        }

        private static List<Point3d> CreatePointsForLShape(double height, double flangeHeight, double webWidth,
            double flangeWidth)
        {
            Point3d topRightPoint = new Point3d(-flangeWidth/2.0 + webWidth, height/2.0, 0);
            Point3d flangeTopPoint = new Point3d(-flangeWidth/2.0 + webWidth, -height/2.0 + flangeHeight, 0);
            Point3d flangeTopRightPoint = new Point3d(flangeWidth/2.0, -height/2.0 + flangeHeight, 0);
            Point3d flangeBottomRightPoint = new Point3d(flangeWidth/2.0, -height/2.0, 0);
            Point3d flangeBottomLeftPoint = new Point3d(-flangeWidth/2.0, -height/2.0, 0);
            Point3d topLeftPoint = new Point3d(-flangeWidth/2.0, height/2.0, 0);
            
            List<Point3d> points = new List<Point3d>{topRightPoint, flangeTopPoint,
                flangeTopRightPoint, flangeBottomRightPoint, flangeBottomLeftPoint, topLeftPoint};

            return points;
        }

        private Polyline ProfilePointsToPolyline(List<Point3d> profilePoints)
        {
            List<Point3d> pointsForPolyline = profilePoints;
            pointsForPolyline.Add(profilePoints[0]);
            
            Polyline polyline = new Polyline(pointsForPolyline);
            
            if (!polyline.IsClosed)
            {
                throw new ArgumentException("Polyline should be closed");
            }

            return polyline;
        }

        private List<Curve> PolylineToListOfCurves(Polyline polyline)
        {
            Line[] lines = polyline.GetSegments();
            List<Curve> curves = lines.Select(line => line.ToNurbsCurve()).Cast<Curve>().ToList();
            return curves;
        }

        public double Tolerance
        {
            get => _tolerance;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Tolerance should be > 0");
                }
                _tolerance = value;
            }

        }
        public string Name { get; }

        public List<Point3d> ProfilePoints
        {
            get => _profilePoints;
            private set 
            {
                if (value.Count <= 2)
                {
                    throw new ArgumentException("There should be more than 2 points as input");
                }
                
                foreach (var point3d in value)
                {
                    if (point3d.Z > Tolerance)
                    {
                        throw new ArgumentException("Points of a profile should be defined on XY Plane. Each point should have Z = 0");
                    }
                }
                _profilePoints = value;
            }
        }

        public Curve ProfileCurve { get; }

        public Brep[] BoundarySurfaces
        {
            get => _breps;
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Brep result is null, check input points.");
                }
                
                if (value.Length != 1)
                {
                    throw new ArgumentException("There is more than 1 brep as a result of profile creation. Check if points are correct and if the order of points is correct.");
                }

                _breps = value;
            }
        }
    }
}