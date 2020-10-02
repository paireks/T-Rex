﻿using System;
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

        private Mesh CreateRebarMesh(Curve rebarCurve, double radius)
        {
            return Mesh.CreateFromCurvePipe(rebarCurve, radius, 10, 70, MeshPipeCapStyle.Flat, false);
        }
        private Curve CreateFilletPolylineWithBendingRoller(Curve rebarCurve, double bendingRollerDiameter)
        {
            RhinoDoc activeDoc = RhinoDoc.ActiveDoc;

            return Curve.CreateFilletCornersCurve(rebarCurve, bendingRollerDiameter / 2.0 + Props.Radius, activeDoc.ModelAbsoluteTolerance, activeDoc.ModelAngleToleranceRadians);
        }
        public void CurveToRebarShape(Curve rebarCurve)
        {
            RebarCurve = rebarCurve;
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }
        public void PolylineToRebarShape(Curve rebarCurve, double bendingRollerDiameter)
        {
            RebarCurve = CreateFilletPolylineWithBendingRoller(rebarCurve, bendingRollerDiameter);
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }

        public void LineBarShape(Rectangle3d rectangle, RebarProperties properties, bool isBottom,
            CoverDimensions coverDimensions)
        {
            double yLevel;

            if (isBottom) { yLevel = rectangle.Y.Min + coverDimensions.Bottom + properties.Radius; }
            else { yLevel = rectangle.Y.Max - coverDimensions.Top - properties.Radius; }

            Point3d startPoint = new Point3d(rectangle.X.Min + coverDimensions.Left, yLevel, 0);
            Point3d endPoint = new Point3d(rectangle.X.Max - coverDimensions.Right, yLevel, 0);

            LineCurve line = new LineCurve(startPoint, endPoint);

            Transform planeToPlane = Transform.PlaneToPlane(Plane.WorldXY, rectangle.Plane);
            Curve rebarLine = line.DuplicateCurve();
            rebarLine.Transform(planeToPlane);

            RebarCurve = rebarLine;
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }

        public void UBarShape(Rectangle3d rectangle, RebarProperties properties, double bendingRollerDiameter,
            bool isBottom, CoverDimensions coverDimensions, double hookLength)
        {
            double yBottomLevel;
            double yTopLevel;

            if (isBottom)
            {
                yBottomLevel = rectangle.Y.Min + coverDimensions.Bottom + properties.Radius;
                yTopLevel = rectangle.Y.Min + coverDimensions.Bottom + hookLength;
            }
            else
            {
                yBottomLevel = rectangle.Y.Max - coverDimensions.Top - properties.Radius;
                yTopLevel = rectangle.Y.Max - coverDimensions.Top - hookLength;
            }

            Point3d topLeft = new Point3d(rectangle.X.Min + coverDimensions.Left + properties.Radius, yTopLevel, 0);
            Point3d bottomLeft = new Point3d(rectangle.X.Min + coverDimensions.Left + properties.Radius, yBottomLevel, 0);
            Point3d bottomRight = new Point3d(rectangle.X.Max - coverDimensions.Right - properties.Radius, yBottomLevel, 0);
            Point3d topRight = new Point3d(rectangle.X.Max - coverDimensions.Right - properties.Radius, yTopLevel, 0);

            PolylineCurve polyline = new PolylineCurve(new List<Point3d> {topLeft, bottomLeft, bottomRight, topRight});
            Transform planeToPlane = Transform.PlaneToPlane(Plane.WorldXY, rectangle.Plane);
            Curve rebarPolyline = polyline.DuplicateCurve();
            rebarPolyline.Transform(planeToPlane);
            rebarPolyline = CreateFilletPolylineWithBendingRoller(rebarPolyline, bendingRollerDiameter);

            RebarCurve = rebarPolyline;
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }

        public void StirrupBarShape(Rectangle3d rectangle, RebarProperties properties, double bendingRollerDiameter,
            int hooksCorner, int hooksType, CoverDimensions coverDimensions, double hookLength)
        {
            List<Point3d> stirrupPoints = new List<Point3d>();

            double bendingRollerRadius = bendingRollerDiameter / 2.0;

            double yBottom = rectangle.Y.Min + coverDimensions.Bottom + properties.Radius;
            double yTop = rectangle.Y.Max - coverDimensions.Top - properties.Radius;
            double xLeft = rectangle.X.Min + coverDimensions.Left + properties.Radius;
            double xRight = rectangle.X.Max - coverDimensions.Right - properties.Radius;

            if (hooksType == 0)
            {
                stirrupPoints.Add(new Point3d(xLeft + hookLength - properties.Radius, yTop, 0));
                stirrupPoints.Add(new Point3d(xLeft, yTop, 0));
                stirrupPoints.Add(new Point3d(xLeft, yBottom, 0));
                stirrupPoints.Add(new Point3d(xRight, yBottom, 0));
                stirrupPoints.Add(new Point3d(xRight, yTop, properties.Diameter));
                stirrupPoints.Add(new Point3d(xLeft, yTop, properties.Diameter));
                stirrupPoints.Add(new Point3d(xLeft, yTop - hookLength + properties.Radius, properties.Diameter));
            }
            else if (hooksType == 1)
            {
                stirrupPoints.Add(new Point3d(xLeft + hookLength - properties.Radius, yTop, 0));
                stirrupPoints.Add(new Point3d(xLeft, yTop, 0));
                stirrupPoints.Add(new Point3d(xLeft, yBottom, 0));
                stirrupPoints.Add(new Point3d(xRight, yBottom, 0));
                stirrupPoints.Add(new Point3d(xRight, yTop, properties.Diameter));
                stirrupPoints.Add(new Point3d(xLeft - bendingRollerRadius * Math.Sqrt(2) - Props.Diameter / Math.Sqrt(2),
                                                   yTop,
                                                  properties.Diameter));
                stirrupPoints.Add(new Point3d(xLeft - bendingRollerRadius * Math.Sqrt(2) - Props.Diameter / Math.Sqrt(2) + (bendingRollerRadius * (Math.Sqrt(2) - 1) + hookLength + bendingRollerRadius + Props.Radius) / Math.Sqrt(2),
                                                  yTop - (bendingRollerRadius * (Math.Sqrt(2) - 1) + hookLength + bendingRollerRadius + Props.Radius) / Math.Sqrt(2),
                                                  properties.Diameter));
            }
            else
            {
                throw new ArgumentException("Hooks Type should be between 0 and 1");
            }

            PolylineCurve polyline = new PolylineCurve(stirrupPoints);
            Transform planeToPlane = Transform.PlaneToPlane(Plane.WorldXY, rectangle.Plane);
            Curve rebarPolyline = polyline.DuplicateCurve();
            rebarPolyline.Transform(planeToPlane);
            rebarPolyline = CreateFilletPolylineWithBendingRoller(rebarPolyline, bendingRollerDiameter);

            RebarCurve = rebarPolyline;
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
            X = stirrupPoints;
            Radius = Props.Radius;
            Diameter = Props.Diameter;
        }

        public override string ToString()
        {
            return "Rebar Shape";
        }

        public Mesh RebarMesh { get; set; }
        public Curve RebarCurve { get; set; }
        public RebarProperties Props { get; set; }
        public List<Point3d> X { get; set; }
        public double Radius { get; set; }
        public double Diameter { get; set; }
    }
}