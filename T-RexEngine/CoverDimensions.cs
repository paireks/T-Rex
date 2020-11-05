using System;

namespace T_RexEngine
{
    public class CoverDimensions
    {
        public CoverDimensions(double left, double right, double top, double bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public override string ToString()
        {
            return String.Format("Cover Dimensions{0}" +
                                 "Left: {1}{0}" +
                                 "Right: {2}{0}" +
                                 "Top: {3}{0}" +
                                 "Bottom: {4}",
                Environment.NewLine, Left, Right, Top, Bottom);
        }

        public double Left { get; set; }
        public double Right { get; set; }
        public double Top { get; set; }
        public double Bottom { get; set; }
    }
}
