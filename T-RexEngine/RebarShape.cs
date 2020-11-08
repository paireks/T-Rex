using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class RebarShape
    {
        private Curve _rebarCurve;
        public RebarShape(RebarProperties props)
        {
            Props = props;
        }
        private Mesh CreateRebarMesh(Curve rebarCurve, double radius)
        {
            return Mesh.CreateFromCurvePipe(rebarCurve, radius, 12, 70, MeshPipeCapStyle.Flat, false);
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
            if (!rebarCurve.TryGetPolyline(out Polyline rebarPolyline))
            {
                throw new ArgumentException("Input curve is not a polyline");
            }
            if (rebarPolyline.Count < 3)
            {
                throw new ArgumentException("Polyline has to contain at least 3 points");
            }
            
            try
            {
                RebarCurve = CreateFilletPolylineWithBendingRoller(rebarCurve, bendingRollerDiameter);
            }
            catch
            {
                throw new ArgumentException("Could not round the corners - check if the input polyline is not a straight line");
            }
            
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
            int position, CoverDimensions coverDimensions, double hookLength)
        {
            List<Point3d> shapePoints =
                RebarPoints.CreateForUBarFromRectangle(rectangle, position, coverDimensions, hookLength, Props);
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
            List<Point3d> shapePoints = RebarPoints.CreateForLBarShape(height, width, Props);
            
            PolylineCurve polyline = new PolylineCurve(shapePoints);
            Curve filletedPolyline = CreateFilletPolylineWithBendingRoller(polyline, bendingRollerDiameter);

            RebarCurve = CreateRebarCurveInAnotherPlane(filletedPolyline, Plane.WorldXY, insertPlane);
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }

        public void RectangleToStirrupShape(Rectangle3d rectangle, double bendingRollerDiameter,
             int hooksType, CoverDimensions coverDimensions, double hookLength)
        {
            List<Point3d> shapePoints = RebarPoints.CreateStirrupFromRectangleShape(rectangle,
            hooksType, bendingRollerDiameter, coverDimensions, hookLength, Props);

            PolylineCurve polyline = new PolylineCurve(shapePoints);
            Curve filletedPolyline = CreateFilletPolylineWithBendingRoller(polyline, bendingRollerDiameter);

            RebarCurve = CreateRebarCurveInAnotherPlane(filletedPolyline, Plane.WorldXY, rectangle.Plane);
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }
        public void StirrupShape(Plane plane, double height, double width, double bendingRollerDiameter, int hooksType, double hookLength)
        {
            Rectangle3d rectangle = new Rectangle3d(plane, width, height);
            CoverDimensions coverDimensions = new CoverDimensions(0.0, 0.0, 0.0, 0.0);
            
            RectangleToStirrupShape(rectangle, bendingRollerDiameter, hooksType, coverDimensions, hookLength);
        }

        public override string ToString()
        {
            return "Rebar Shape";
        }

        public Mesh RebarMesh { get; private set; }

        public Curve RebarCurve
        {
            get { return _rebarCurve; }
            set
            {
                if (value.GetLength() > 0)
                {
                    _rebarCurve = value;
                }
                else
                {
                    throw new ArgumentException("Length of the Rebar Curve can't be 0");
                }
            }
        }
        public RebarProperties Props { get; }
    }
}
