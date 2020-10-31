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
            RebarPlane = Plane.WorldXY;
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

        public void LineBarShape(Rectangle3d rectangle, int position, CoverDimensions coverDimensions)
        {
            Point3d startPoint;
            Point3d endPoint;

            if (position == 0)
            {
                startPoint = new Point3d(rectangle.X.Min + coverDimensions.Left, rectangle.Y.Max - coverDimensions.Top - Props.Radius, 0);
                endPoint = new Point3d(rectangle.X.Max - coverDimensions.Right, rectangle.Y.Max - coverDimensions.Top - Props.Radius, 0);
            }
            else if (position == 1)
            {
                startPoint = new Point3d(rectangle.X.Max - coverDimensions.Right - Props.Radius, rectangle.Y.Max - coverDimensions.Top,0);
                endPoint = new Point3d(rectangle.X.Max - coverDimensions.Right - Props.Radius, rectangle.Y.Min + coverDimensions.Bottom,0);
            }
            else if (position == 2)
            {
                startPoint = new Point3d(rectangle.X.Min + coverDimensions.Left, rectangle.Y.Min + coverDimensions.Bottom + Props.Radius, 0);
                endPoint = new Point3d(rectangle.X.Max - coverDimensions.Right, rectangle.Y.Min + coverDimensions.Bottom + Props.Radius, 0);
            }
            else if (position == 3)
            {
                startPoint = new Point3d(rectangle.X.Min + coverDimensions.Left + Props.Radius, rectangle.Y.Min + coverDimensions.Bottom,0);
                endPoint = new Point3d(rectangle.X.Min + coverDimensions.Left + Props.Radius, rectangle.Y.Max - coverDimensions.Top,0);
            }
            else
            {
                throw new ArgumentException("Position should be between 0 and 3");
            }

            LineCurve line = new LineCurve(startPoint, endPoint);

            Transform planeToPlane = Transform.PlaneToPlane(Plane.WorldXY, rectangle.Plane);
            Curve rebarLine = line.DuplicateCurve();
            rebarLine.Transform(planeToPlane);

            RebarCurve = rebarLine;
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
            RebarPlane = rectangle.Plane;
        }

        public void UBarShape(Rectangle3d rectangle, double bendingRollerDiameter,
            bool isBottom, CoverDimensions coverDimensions, double hookLength)
        {
            double yBottomLevel;
            double yTopLevel;

            if (isBottom)
            {
                yBottomLevel = rectangle.Y.Min + coverDimensions.Bottom + Props.Radius;
                yTopLevel = rectangle.Y.Min + coverDimensions.Bottom + hookLength;
            }
            else
            {
                yBottomLevel = rectangle.Y.Max - coverDimensions.Top - Props.Radius;
                yTopLevel = rectangle.Y.Max - coverDimensions.Top - hookLength;
            }

            Point3d topLeft = new Point3d(rectangle.X.Min + coverDimensions.Left + Props.Radius, yTopLevel, 0);
            Point3d bottomLeft = new Point3d(rectangle.X.Min + coverDimensions.Left + Props.Radius, yBottomLevel, 0);
            Point3d bottomRight = new Point3d(rectangle.X.Max - coverDimensions.Right - Props.Radius, yBottomLevel, 0);
            Point3d topRight = new Point3d(rectangle.X.Max - coverDimensions.Right - Props.Radius, yTopLevel, 0);

            PolylineCurve polyline = new PolylineCurve(new List<Point3d> {topLeft, bottomLeft, bottomRight, topRight});
            Transform planeToPlane = Transform.PlaneToPlane(Plane.WorldXY, rectangle.Plane);
            Curve rebarPolyline = polyline.DuplicateCurve();
            rebarPolyline.Transform(planeToPlane);
            rebarPolyline = CreateFilletPolylineWithBendingRoller(rebarPolyline, bendingRollerDiameter);

            RebarCurve = rebarPolyline;
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
            RebarPlane = rectangle.Plane;
        }

        public void SpacerShape(Point3d insertPoint, double height, double length, double width, double bendingRollerDiameter)
        {
            List<Point3d> spacerPoints = new List<Point3d>
            {
                new Point3d(insertPoint.X - length / 2.0, insertPoint.Y - width / 2.0 + Props.Radius, insertPoint.Z + Props.Radius),
                new Point3d(insertPoint.X, insertPoint.Y - width / 2.0 + Props.Radius, insertPoint.Z + Props.Radius),
                new Point3d(insertPoint.X, insertPoint.Y - width / 2.0 + Props.Radius, insertPoint.Z + height - Props.Radius),
                new Point3d(insertPoint.X, insertPoint.Y + width / 2.0 - Props.Radius , insertPoint.Z + height - Props.Radius),
                new Point3d(insertPoint.X, insertPoint.Y + width / 2.0 - Props.Radius, insertPoint.Z + Props.Radius),
                new Point3d(insertPoint.X + length / 2.0, insertPoint.Y + width / 2.0 - Props.Radius, insertPoint.Z + Props.Radius)
            };


            PolylineCurve polyline = new PolylineCurve(spacerPoints);
            Curve rebarPolyline = CreateFilletPolylineWithBendingRoller(polyline, bendingRollerDiameter);
            
            RebarCurve = rebarPolyline;
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
            RebarPlane = new Plane(insertPoint, Vector3d.XAxis, Vector3d.YAxis);
        }

        public void StirrupShape(Rectangle3d rectangle, double bendingRollerDiameter,
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
            RebarPlane = rectangle.Plane;
        }

        public override string ToString()
        {
            return "Rebar Shape";
        }

        public Mesh RebarMesh { get; set; }
        public Curve RebarCurve { get; set; }
        public Plane RebarPlane { get; set; }
        public RebarProperties Props { get; set; }
    }
}
