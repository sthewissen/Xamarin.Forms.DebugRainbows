using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;
using UIKit;
using Xamarin.Forms.DebugRainbows;
using CoreAnimation;

[assembly: ExportRenderer(typeof(DebugGridWrapper), typeof(DebugGridWrapperRendereriOS))]
namespace Xamarin.Forms.DebugRainbows
{
    public class DebugGridWrapperRendereriOS : ViewRenderer<DebugGridWrapper, UIView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DebugGridWrapper> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var grid = e.NewElement as DebugGridWrapper;

                SetNativeControl(new DebugGridViewiOS
                {
                    HorizontalSpacing = grid.HorizontalSpacing,
                    VerticalSpacing = grid.VerticalSpacing,
                    MajorGridLineInterval = grid.MajorGridLineInterval,
                    MajorGridLineColor = grid.MajorGridLineColor,
                    MinorGridLineColor = grid.MinorGridLineColor,
                    MajorGridLineOpacity = grid.MajorGridLineOpacity,
                    MinorGridLineOpacity = grid.MinorGridLineOpacity,
                    MajorGridLineThickness = grid.MajorGridLineWidth,
                    MinorGridLineThickness = grid.MinorGridLineWidth,
                    Padding = grid.Padding,
                    MakeGridRainbows = grid.MakeGridRainbows
                });
            }
        }
    }

    public class DebugGridViewiOS : UIView
    {
        private CALayer _minorGridLayer;
        private CALayer _majorGridLayer;

        public double HorizontalSpacing { get; set; }
        public double VerticalSpacing { get; set; }
        public int MajorGridLineInterval { get; set; }
        public Color MajorGridLineColor { get; set; }
        public Color MinorGridLineColor { get; set; }
        public double MajorGridLineOpacity { get; set; }
        public double MinorGridLineOpacity { get; set; }
        public double MajorGridLineThickness { get; set; }
        public double MinorGridLineThickness { get; set; }
        public Thickness Padding { get; set; }
        public bool MakeGridRainbows { get; set; }

        private void DrawGrid()
        {
            DrawGridLayer(_minorGridLayer, false);
            DrawGridLayer(_majorGridLayer, true);
        }

        private void DrawGridLayer(CALayer layer, bool isMajor)
        {
            var path = CreatePath(isMajor ? MajorGridLineInterval : 0);

            layer = new CAShapeLayer
            {
                LineWidth = isMajor ? (nfloat)MajorGridLineThickness : (nfloat)MinorGridLineThickness,
                Path = path.CGPath,
                StrokeColor = isMajor ? MajorGridLineColor.ToCGColor() : MinorGridLineColor.ToCGColor(),
                Opacity = isMajor ? (float)MajorGridLineOpacity : (float)MinorGridLineOpacity,
                Frame = new CGRect(0, 0, Bounds.Size.Width, Bounds.Size.Height)
            };

            if (!MakeGridRainbows)
            {
                this.Layer.AddSublayer(layer);
            }
            else
            {
                var gradientLayer = new CAGradientLayer();
                gradientLayer.StartPoint = new CGPoint(0.5, 0.0);
                gradientLayer.EndPoint = new CGPoint(0.5, 1.0);
                gradientLayer.Frame = new CGRect(0, 0, Bounds.Size.Width, Bounds.Size.Height);

                gradientLayer.Colors = new CGColor[] {
                        Color.FromHex("#f3855b").ToCGColor(),
                        Color.FromHex("#fbcf93").ToCGColor(),
                        Color.FromHex("#fbe960").ToCGColor(),
                        Color.FromHex("#a0e67a").ToCGColor(),
                        Color.FromHex("#33c6ee").ToCGColor(),
                        Color.FromHex("#c652ba").ToCGColor(),
                        Color.FromHex("#ef53b2").ToCGColor()
                    };

                gradientLayer.Mask = layer;
                this.Layer.AddSublayer(gradientLayer);
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
            _minorGridLayer?.RemoveFromSuperLayer();
            _majorGridLayer?.RemoveFromSuperLayer();
        }

        public override void Draw(CGRect rect)
        {
            DrawGrid();
        }
    }

    public class InvertedShapeLayer : CALayer
    {
        public CGPath Path { get; set; }
        public CGColor FillColor { get; set; }
        public CGColor StrokeColor { get; set; }
        public nfloat LineWidth { get; set; } = 1.0f;

        public override void DrawInContext(CGContext ctx)
        {
            base.DrawInContext(ctx);

            ctx.SetFillColor(gray: 0.0f, alpha: 1.0f);
            ctx.FillRect(this.Bounds);
            ctx.SetBlendMode(CGBlendMode.SourceIn);

            ctx.SetStrokeColor(StrokeColor);
            ctx.SetFillColor(FillColor);
            ctx.SetLineWidth(LineWidth);
            ctx.AddPath(Path);

            ctx.DrawPath(CGPathDrawingMode.FillStroke);
        }
    }
}
