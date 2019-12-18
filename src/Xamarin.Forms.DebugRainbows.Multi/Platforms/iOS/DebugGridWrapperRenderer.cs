using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;
using UIKit;
using Xamarin.Forms.DebugRainbows;
using CoreAnimation;

[assembly: ExportRenderer(typeof(DebugGridWrapper), typeof(DebugGridWrapperRenderer))]
namespace Xamarin.Forms.DebugRainbows
{
    public class DebugGridWrapperRenderer : ViewRenderer<DebugGridWrapper, UIView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DebugGridWrapper> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var grid = e.NewElement as DebugGridWrapper;

                SetNativeControl(new DebugGridView
                {
                    HorizontalSpacing = grid.HorizontalSpacing,
                    VerticalSpacing = grid.VerticalSpacing,
                    MajorGridLineInterval = grid.MajorGridLineInterval,
                    MajorGridLineColor = grid.MajorGridLineColor,
                    MinorGridLineColor = grid.MinorGridLineColor,
                    MajorGridLineOpacity = grid.MajorGridLineOpacity,
                    MinorGridLineOpacity = grid.MinorGridLineOpacity,
                    MajorGridLineThickness = grid.MajorGridLineWidth,
                    MinorGridLineThickness = grid.MinorGridLineWidth
                });
            }
        }
    }

    public class DebugGridView : UIView
    {
        private CAShapeLayer _minorGridLayer;
        private CAShapeLayer _majorGridLayer;

        public double HorizontalSpacing { get; set; }
        public double VerticalSpacing { get; set; }
        public int MajorGridLineInterval { get; set; }
        public Color MajorGridLineColor { get; set; }
        public Color MinorGridLineColor { get; set; }
        public double MajorGridLineOpacity { get; set; }
        public double MinorGridLineOpacity { get; set; }
        public double MajorGridLineThickness { get; set; }
        public double MinorGridLineThickness { get; set; }

        private void DrawGrid()
        {
            if (_minorGridLayer == null)
            {
                var minorPath = CreatePath();

                _minorGridLayer = new CAShapeLayer
                {
                    LineWidth = (nfloat)MinorGridLineThickness,
                    Path = minorPath.CGPath,
                    StrokeColor = MinorGridLineColor.ToCGColor(),
                    Opacity = (float)MinorGridLineOpacity
                };

                this.Layer.AddSublayer(_minorGridLayer);
            }

            if (_majorGridLayer == null)
            {
                var majorPath = CreatePath(MajorGridLineInterval);

                _majorGridLayer = new CAShapeLayer
                {
                    LineWidth = (nfloat)MajorGridLineThickness,
                    Path = majorPath.CGPath,
                    StrokeColor = MajorGridLineColor.ToCGColor(),
                    Opacity = (float)MajorGridLineOpacity
                };

                this.Layer.AddSublayer(_majorGridLayer);
            }
        }

        private UIBezierPath CreatePath(int interval = 0)
        {
            var path = new UIBezierPath();
            var gridLinesHorizontal = Bounds.Width / HorizontalSpacing;
            var gridLinesVertical = Bounds.Height / VerticalSpacing;

            for (int i = 0; i < gridLinesHorizontal; i++)
            {
                if (interval == 0 || i % interval == 0)
                {
                    var start = new CGPoint(x: (nfloat)i * HorizontalSpacing, y: 0);
                    var end = new CGPoint(x: (nfloat)i * HorizontalSpacing, y: Bounds.Height);
                    path.MoveTo(start);
                    path.AddLineTo(end);
                }
            }

            for (int i = 0; i < gridLinesVertical; i++)
            {
                if (interval == 0 || i % interval == 0)
                {
                    var start = new CGPoint(x: 0, y: (nfloat)i * VerticalSpacing);
                    var end = new CGPoint(x: Bounds.Width, y: (nfloat)i * VerticalSpacing);
                    path.MoveTo(start);
                    path.AddLineTo(end);
                }
            }

            path.ClosePath();

            return path;
        }

        private void RemoveGrid()
        {
            if (_minorGridLayer != null)
            {
                _minorGridLayer.RemoveFromSuperLayer();
            }

            if (_majorGridLayer != null)
            {
                _majorGridLayer.RemoveFromSuperLayer();
            }
        }

        public override void Draw(CGRect rect)
        {
            DrawGrid();
        }
    }
}
