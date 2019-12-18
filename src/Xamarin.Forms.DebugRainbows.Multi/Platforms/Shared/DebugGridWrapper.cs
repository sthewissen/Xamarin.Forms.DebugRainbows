using System;

namespace Xamarin.Forms.DebugRainbows
{
    public class DebugGridWrapper : Grid
    {
        public double HorizontalSpacing { get; set; }
        public double VerticalSpacing { get; set; }

        public int MajorGridLineInterval { get; set; }
        public Color MajorGridLineColor { get; set; }
        public Color MinorGridLineColor { get; set; }
        public double MajorGridLineOpacity { get; set; }
        public double MinorGridLineOpacity { get; set; }
        public double MajorGridLineWidth { get; set; }
        public double MinorGridLineWidth { get; set; }
    }
}
