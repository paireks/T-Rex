using System;
using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class Profile
    {
        public Profile(string name, List<Point3d> points, double tolerance)
        {
            List<Point3d> pointsForPolyline = points;
            pointsForPolyline.Add(points[0]);

            ProfilePoints = pointsForPolyline;

            Polyline polyline = new Polyline(ProfilePoints);
            
            if (!polyline.IsClosed)
            {
                throw new ArgumentException("Polyline should be closed");
            }
            if (!polyline.ToNurbsCurve().IsPlanar())
            {
                throw new ArgumentException("Polyline should be planar");
            }

            Line[] lines = polyline.GetSegments();
            List<Curve> curves = lines.Select(line => line.ToNurbsCurve()).Cast<Curve>().ToList();

            BoundarySurfaces = Brep.CreatePlanarBreps(curves, tolerance);
            ProfileCurve = polyline.ToNurbsCurve();
            Name = name;
            Tolerance = tolerance;
        }
        public double Tolerance { get; }
        public string Name { get; }
        public List<Point3d> ProfilePoints { get; }
        public Curve ProfileCurve { get; }
        public Brep[] BoundarySurfaces { get; }
    }
}