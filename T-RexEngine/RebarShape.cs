using System;
using System.Collections.Generic;
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
        private Curve CreateRebarCurveInAnotherPlane(Curve curve, Plane originPlane, Plane destinationPlane)
        {
            Curve rebarCurve = curve.DuplicateCurve();
            Transform planeToPlane = Transform.PlaneToPlane(originPlane, destinationPlane);
            rebarCurve.Transform(planeToPlane);
            
            return rebarCurve;
        }

        public void RectangleToLineBarShape(Rectangle3d rectangle, int position, CoverDimensions coverDimensions)
        {
            List<Point3d> shapePoints =
                RebarPoints.CreateForLineFromRectangle(rectangle, position, coverDimensions, Props);
            LineCurve line = new LineCurve(shapePoints[0], shapePoints[1]);
            
            RebarCurve = CreateRebarCurveInAnotherPlane(line, Plane.WorldXY, rectangle.Plane);
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }

        public void RectangleToUBarShape(Rectangle3d rectangle, double bendingRollerDiameter,
            bool isBottom, CoverDimensions coverDimensions, double hookLength)
        {
            List<Point3d> shapePoints =
                RebarPoints.CreateForUBarFromRectangle(rectangle, bendingRollerDiameter, isBottom, coverDimensions, hookLength, Props);
            PolylineCurve polyline = new PolylineCurve(shapePoints);
            Curve filletedPolyline = CreateFilletPolylineWithBendingRoller(polyline, bendingRollerDiameter);
            
            RebarCurve = CreateRebarCurveInAnotherPlane(filletedPolyline, Plane.WorldXY, rectangle.Plane);
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }

        public void SpacerShape(Plane insertPlane, double height, double length, double width, double bendingRollerDiameter)
        {
            List<Point3d> shapePoints =
                RebarPoints.CreateForSpacerShape(height, length, width, Props);
            PolylineCurve polyline = new PolylineCurve(shapePoints);
            Curve filletedPolyline = CreateFilletPolylineWithBendingRoller(polyline, bendingRollerDiameter);

            RebarCurve = CreateRebarCurveInAnotherPlane(filletedPolyline, Plane.WorldXY, insertPlane);
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }
        
        public void LBarShape(Plane insertPlane, double height, double width, double bendingRollerDiameter)
        {
            List<Point3d> lShapePoints = new List<Point3d>
            {
                new Point3d(0.0,Props.Radius, 0.0),
                new Point3d(width - Props.Radius, Props.Radius, 0.0),
                new Point3d(width - Props.Radius, height + Props.Radius, 0.0)
            };
            
            PolylineCurve polyline = new PolylineCurve(lShapePoints);
            Transform planeToPlane = Transform.PlaneToPlane(Plane.WorldXY, insertPlane);
            Curve rebarPolyline = CreateFilletPolylineWithBendingRoller(polyline, bendingRollerDiameter);
            rebarPolyline.Transform(planeToPlane);
            
            RebarCurve = rebarPolyline;
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }

        public void RectangleToStirrupShape(Rectangle3d rectangle, double bendingRollerDiameter,
             int hooksType, CoverDimensions coverDimensions, double hookLength)
        {
            List<Point3d> stirrupPoints = new List<Point3d>();

            double bendingRollerRadius = bendingRollerDiameter / 2.0;

            double yBottom = rectangle.Y.Min + coverDimensions.Bottom + Props.Radius;
            double yTop = rectangle.Y.Max - coverDimensions.Top - Props.Radius;
            double xLeft = rectangle.X.Min + coverDimensions.Left + Props.Radius;
            double xRight = rectangle.X.Max - coverDimensions.Right - Props.Radius;

            if (hooksType == 0)
            {
                stirrupPoints.Add(new Point3d(xLeft + hookLength - Props.Radius, yTop, - Props.Radius));
                stirrupPoints.Add(new Point3d(xLeft, yTop, - Props.Radius));
                stirrupPoints.Add(new Point3d(xLeft, yBottom, - Props.Radius));
                stirrupPoints.Add(new Point3d(xRight, yBottom, - Props.Radius));
                stirrupPoints.Add(new Point3d(xRight, yTop, Props.Radius));
                stirrupPoints.Add(new Point3d(xLeft, yTop, Props.Radius));
                stirrupPoints.Add(new Point3d(xLeft, yTop - hookLength + Props.Radius, Props.Radius));
            }
            else if (hooksType == 1)
            {
                double polylinePointOffsetForHook = (bendingRollerRadius + Props.Radius) * Math.Sqrt(2);
                double hookEndPointOffset =
                    ((Math.Sqrt(2) - 1) * (bendingRollerRadius + Props.Radius) - Props.Radius + hookLength +
                     (bendingRollerRadius + Props.Radius)) / Math.Sqrt(2);

                stirrupPoints.Add(new Point3d(xLeft + hookEndPointOffset, yTop + polylinePointOffsetForHook - hookEndPointOffset, -Props.Radius));
                stirrupPoints.Add(new Point3d(xLeft, yTop + polylinePointOffsetForHook, -Props.Radius));
                stirrupPoints.Add(new Point3d(xLeft, yBottom, -Props.Radius));
                stirrupPoints.Add(new Point3d(xRight, yBottom, -Props.Radius));
                stirrupPoints.Add(new Point3d(xRight, yTop, Props.Radius));
                stirrupPoints.Add(new Point3d(xLeft - polylinePointOffsetForHook, yTop, Props.Radius));
                stirrupPoints.Add(new Point3d(xLeft - polylinePointOffsetForHook + hookEndPointOffset,
                                                  yTop - hookEndPointOffset,
                                                  Props.Radius));
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
