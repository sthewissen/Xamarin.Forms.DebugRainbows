using System;

namespace Xamarin.Forms.DebugRainbows
{
    public class DebugGridWrapper : ContentView
    {
        public double HorizontalItemSize { get; set; }
        public double VerticalItemSize { get; set; }

        public int MajorGridLineInterval { get; set; }
        public Color MajorGridLineColor { get; set; }
        public Color GridLineColor { get; set; }
        public double MajorGridLineOpacity { get; set; }
        public double GridLineOpacity { get; set; }
        public double MajorGridLineWidth { get; set; }
        public double GridLineWidth { get; set; }
        public bool Inverse { get; set; }
        public bool MakeGridRainbows { get; set; }
        public DebugGridOrigin GridOrigin { get; set; }

        public DebugGridWrapper()
        {
            InputTransparent = true;
        }
    }
}
