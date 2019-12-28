using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;
using UIKit;
using Xamarin.Forms.DebugRainbows;
using CoreAnimation;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(DebugGridWrapper), typeof(DebugGridWrapperRendereriOS))]
namespace Xamarin.Forms.DebugRainbows
{
    public class DebugGridWrapperRendereriOS : ViewRenderer<DebugGridWrapper, UIView>
    {
        UIView _contentView;

        protected override void OnElementChanged(ElementChangedEventArgs<DebugGridWrapper> e)
        {
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var grid = e.NewElement as DebugGridWrapper;

                    SetNativeControl(new DebugGridViewiOS()
                    {
                        HorizontalItemSize = grid.HorizontalItemSize,
                        VerticalItemSize = grid.VerticalItemSize,
                        MajorGridLineInterval = grid.MajorGridLineInterval,
                        MajorGridLineColor = grid.MajorGridLineColor,
                        GridLineColor = grid.GridLineColor,
                        MajorGridLineOpacity = grid.MajorGridLineOpacity,
                        GridLineOpacity = grid.GridLineOpacity,
                        MajorGridLineThickness = grid.MajorGridLineWidth,
                        GridLineThickness = grid.GridLineWidth,
                        MakeGridRainbows = grid.MakeGridRainbows,
                        Inverse = grid.Inverse
                    });
                }
            }

            base.OnElementChanged(e);
        }
    }

    public class DebugGridViewiOS : UIView
    {
        private CALayer _gridLayer;
        private CALayer _majorGridLayer;

        private CGColor[] rainbowColors = {
                        Color.FromHex("#f3855b").ToCGColor(),
                        Color.FromHex("#fbcf93").ToCGColor(),
                        Color.FromHex("#fbe960").ToCGColor(),
                        Color.FromHex("#a0e67a").ToCGColor(),
                        Color.FromHex("#33c6ee").ToCGColor(),
                        Color.FromHex("#c652ba").ToCGColor(),
                        Color.FromHex("#ef53b2").ToCGColor()
                    };

        public double HorizontalItemSize { get; set; }
        public double VerticalItemSize { get; set; }
        public int MajorGridLineInterval { get; set; }
        public Color MajorGridLineColor { get; set; }
        public Color GridLineColor { get; set; }
        public double MajorGridLineOpacity { get; set; }
        public double GridLineOpacity { get; set; }
        public double MajorGridLineThickness { get; set; }
        public double GridLineThickness { get; set; }
        public bool MakeGridRainbows { get; set; }
        public bool Inverse { get; set; }

        public DebugGridViewiOS()
        {
            BackgroundColor = UIColor.Clear;
            ContentMode = UIViewContentMode.Redraw;
        }

        private void DrawGrid(CGRect rect)
        {
            if (Inverse)
            {
                DrawInverseGridLayer(rect);
            }
            else
            {
                DrawNormalGridLayer(_gridLayer, false);
                DrawNormalGridLayer(_majorGridLayer, true);
            }
        }

        private void DrawInverseGridLayer(CGRect rect)
        {
            var horizontalTotal = 0;
            var context = UIGraphics.GetCurrentContext();

            for (int i = 1; horizontalTotal < Bounds.Size.Width; i++)
            {
                var verticalTotal = 0;
                var horizontalSpacerSize = MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? MajorGridLineThickness : GridLineThickness;

                for (int j = 1; verticalTotal < Bounds.Size.Height; j++)
                {
                    var verticalSpacerSize = MajorGridLineInterval > 0 && j % MajorGridLineInterval == 0 ? MajorGridLineThickness : GridLineThickness;
                    var rectangle = new CGRect(horizontalTotal, verticalTotal, HorizontalItemSize, VerticalItemSize);

                    if (MakeGridRainbows)
                    {
                        var color = rainbowColors[(i + j) % rainbowColors.Length];
                        context.SetFillColor(color);
                        context.SetAlpha((nfloat)GridLineOpacity);
                    }
                    else
                    {
                        context.SetFillColor(GridLineColor.ToCGColor());
                        context.SetAlpha((nfloat)GridLineOpacity);
                    }

                    context.FillRect(rectangle);

                    verticalTotal += (int)(VerticalItemSize + verticalSpacerSize);
                }

                horizontalTotal += (int)(HorizontalItemSize + horizontalSpacerSize);
            }
        }

        private void DrawNormalGridLayer(CALayer layer, bool isMajor)
        {
            if (isMajor && MajorGridLineInterval == 0)
                return;

            using (var path = CreatePath(isMajor ? MajorGridLineInterval : 0))
            {
                layer = new CAShapeLayer
                {
                    LineWidth = isMajor ? (nfloat)MajorGridLineThickness : (nfloat)GridLineThickness,
                    Path = path.CGPath,
                    StrokeColor = isMajor ? MajorGridLineColor.ToCGColor() : GridLineColor.ToCGColor(),
                    Opacity = isMajor ? (float)MajorGridLineOpacity : (float)GridLineOpacity,
                    Frame = new CGRect(0, 0, Bounds.Size.Width, Bounds.Size.Height)
                };

                if (!MakeGridRainbows)
                {
                    this.Layer.AddSublayer(layer);
                }
                else
                {
                    var gradientLayer = new CAGradientLayer
                    {
                        StartPoint = new CGPoint(0.5, 0.0),
                        EndPoint = new CGPoint(0.5, 1.0),
                        Frame = new CGRect(0, 0, Bounds.Size.Width, Bounds.Size.Height),
                        Colors = rainbowColors,
                        Mask = layer
                    };
                    this.Layer.AddSublayer(gradientLayer);
                }
            }
        }

        private UIBezierPath CreatePath(int interval = 0)
        {
            var path = new UIBezierPath();
            var gridLinesHorizontal = Bounds.Width / HorizontalItemSize;
            var gridLinesVertical = Bounds.Height / VerticalItemSize;

            for (int i = 0; i < gridLinesHorizontal; i++)
            {
                if (interval == 0 || i % interval == 0)
                {
                    var start = new CGPoint(x: (nfloat)i * HorizontalItemSize, y: 0);
                    var end = new CGPoint(x: (nfloat)i * HorizontalItemSize, y: Bounds.Height);
                    path.MoveTo(start);
                    path.AddLineTo(end);
                }
            }

            for (int i = 0; i < gridLinesVertical; i++)
            {
                if (interval == 0 || i % interval == 0)
                {
                    var start = new CGPoint(x: 0, y: (nfloat)i * VerticalItemSize);
                    var end = new CGPoint(x: Bounds.Width, y: (nfloat)i * VerticalItemSize);
                    path.MoveTo(start);
                    path.AddLineTo(end);
                }
            }

            path.ClosePath();

            return path;
        }

        private void RemoveGrid()
        {
            _gridLayer?.RemoveFromSuperLayer();
            _majorGridLayer?.RemoveFromSuperLayer();
        }

        public override void Draw(CGRect rect)
        {
            DrawGrid(rect);
        }
    }
}
