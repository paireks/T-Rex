﻿using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Geometry;

namespace T_RexEngine
{
    public static class RebarPoints
    {
        public static List<Point3d> CreateForLineFromRectangle(Rectangle3d rectangle, int position, CoverDimensions coverDimensions, RebarProperties props)
        {
            Point3d startPoint;
            Point3d endPoint;
            
            if (position == 0)
            {
                startPoint = new Point3d(rectangle.X.Min + coverDimensions.Left, rectangle.Y.Max - coverDimensions.Top - props.Radius, 0);
                endPoint = new Point3d(rectangle.X.Max - coverDimensions.Right, rectangle.Y.Max - coverDimensions.Top - props.Radius, 0);
            }
            else if (position == 1)
            {
                startPoint = new Point3d(rectangle.X.Max - coverDimensions.Right - props.Radius, rectangle.Y.Max - coverDimensions.Top,0);
                endPoint = new Point3d(rectangle.X.Max - coverDimensions.Right - props.Radius, rectangle.Y.Min + coverDimensions.Bottom,0);
            }
            else if (position == 2)
            {
                startPoint = new Point3d(rectangle.X.Min + coverDimensions.Left, rectangle.Y.Min + coverDimensions.Bottom + props.Radius, 0);
                endPoint = new Point3d(rectangle.X.Max - coverDimensions.Right, rectangle.Y.Min + coverDimensions.Bottom + props.Radius, 0);
            }
            else if (position == 3)
            {
                startPoint = new Point3d(rectangle.X.Min + coverDimensions.Left + props.Radius, rectangle.Y.Min + coverDimensions.Bottom,0);
                endPoint = new Point3d(rectangle.X.Min + coverDimensions.Left + props.Radius, rectangle.Y.Max - coverDimensions.Top,0);
            }
            else
            {
                throw new ArgumentException("Position should be between 0 and 3");
            }
            
            return new List<Point3d>{startPoint, endPoint};
        }

        public static List<Point3d> CreateForUBarFromRectangle(Rectangle3d rectangle, double bendingRollerDiameter,
            bool isBottom, CoverDimensions coverDimensions, double hookLength, RebarProperties props)
        {
            double yBottomLevel;
            double yTopLevel;

            if (isBottom)
            {
                yBottomLevel = rectangle.Y.Min + coverDimensions.Bottom + props.Radius;
                yTopLevel = rectangle.Y.Min + coverDimensions.Bottom + hookLength;
            }
            else
            {
                yBottomLevel = rectangle.Y.Max - coverDimensions.Top - props.Radius;
                yTopLevel = rectangle.Y.Max - coverDimensions.Top - hookLength;
            }

            Point3d topLeft = new Point3d(rectangle.X.Min + coverDimensions.Left + props.Radius, yTopLevel, 0);
            Point3d bottomLeft = new Point3d(rectangle.X.Min + coverDimensions.Left + props.Radius, yBottomLevel, 0);
            Point3d bottomRight = new Point3d(rectangle.X.Max - coverDimensions.Right - props.Radius, yBottomLevel, 0);
            Point3d topRight = new Point3d(rectangle.X.Max - coverDimensions.Right - props.Radius, yTopLevel, 0);
            
            return new List<Point3d> {topLeft, bottomLeft, bottomRight, topRight};
        }

        public static List<Point3d> CreateForSpacerShape(double height, double length, double width,
            RebarProperties props)
        {
            List<Point3d> spacerPoints = new List<Point3d>
            {
                new Point3d(- length / 2.0, - width / 2.0 + props.Radius, props.Radius),
                new Point3d(0.0,- width / 2.0 + props.Radius,props.Radius),
                new Point3d(0.0,- width / 2.0 + props.Radius,height - props.Radius),
                new Point3d(0.0,width / 2.0 - props.Radius ,height - props.Radius),
                new Point3d(0.0,width / 2.0 - props.Radius,props.Radius),
                new Point3d(length / 2.0,width / 2.0 - props.Radius,props.Radius)
            };

            return spacerPoints;
        }
    }
}