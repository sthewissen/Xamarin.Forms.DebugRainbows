using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.DebugRainbows;
using Android.Content;
using AView = Android.Views.View;
using Android.Graphics;
using Android.Util;
using Android.App;

[assembly: ExportRenderer(typeof(DebugGridWrapper), typeof(DebugGridWrapperRendererDroid))]
namespace Xamarin.Forms.DebugRainbows
{
    public class DebugGridWrapperRendererDroid : ViewRenderer<DebugGridWrapper, AView>
    {
        public DebugGridWrapperRendererDroid(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<DebugGridWrapper> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var grid = e.NewElement as DebugGridWrapper;

                SetNativeControl(new DebugGridViewDroid(Context)
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
                    Padding = grid.Padding,
                    MakeGridRainbows = grid.MakeGridRainbows
                });
            }
        }
    }

    public class DebugGridViewDroid : AView
    {
        private int screenWidth;
        private int screenHeight;

        public double HorizontalItemSize { get; set; }
        public double VerticalItemSize { get; set; }
        public int MajorGridLineInterval { get; set; }
        public Color MajorGridLineColor { get; set; }
        public Color GridLineColor { get; set; }
        public double MajorGridLineOpacity { get; set; }
        public double GridLineOpacity { get; set; }
        public double MajorGridLineThickness { get; set; }
        public double GridLineThickness { get; set; }
        public Thickness Padding { get; set; }
        public bool MakeGridRainbows { get; set; }

        public DebugGridViewDroid(Context context) : base(context)
        {
            Init();
        }

        public static float ConvertDpToPixel(float dp, Context context)
        {
            return dp * ((float)context.Resources.DisplayMetrics.DensityDpi / (int)DisplayMetricsDensity.Default);
        }

        public void Init()
        {
            GetScreenDimensions();
        }

        private void GetScreenDimensions()
        {
            DisplayMetrics displayMetrics = new DisplayMetrics();
            ((Activity)Context).WindowManager.DefaultDisplay.GetMetrics(displayMetrics);
            screenWidth = displayMetrics.WidthPixels;
            screenHeight = displayMetrics.HeightPixels;
        }

        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            base.OnLayout(changed, left, top, right, bottom);
            GetScreenDimensions();
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            var majorPaint = new Paint
            {
                StrokeWidth = ConvertDpToPixel((float)MajorGridLineThickness, Context),
                Color = MajorGridLineColor.ToAndroid(),
                Alpha = (int)(255 * MajorGridLineOpacity)
            };

            var Paint = new Paint
            {
                StrokeWidth = ConvertDpToPixel((float)GridLineThickness, Context),
                Color = GridLineColor.ToAndroid(),
                Alpha = (int)(255 * GridLineOpacity)
            };

            if (MakeGridRainbows)
            {
                var colors = new int[] {
                        Color.FromHex("#f3855b").ToAndroid().ToArgb(),
                        Color.FromHex("#fbcf93").ToAndroid().ToArgb(),
                        Color.FromHex("#fbe960").ToAndroid().ToArgb(),
                        Color.FromHex("#a0e67a").ToAndroid().ToArgb(),
                        Color.FromHex("#33c6ee").ToAndroid().ToArgb(),
                        Color.FromHex("#c652ba").ToAndroid().ToArgb()
                };

                var a = canvas.Width * Math.Pow(Math.Sin(2 * Math.PI * ((90 + 0.75) / 2)), 2);
                var b = canvas.Height * Math.Pow(Math.Sin(2 * Math.PI * ((90 + 0.0) / 2)), 2);
                var c = canvas.Width * Math.Pow(Math.Sin(2 * Math.PI * ((90 + 0.25) / 2)), 2);
                var d = canvas.Height * Math.Pow(Math.Sin(2 * Math.PI * ((90 + 0.5) / 2)), 2);

                var locations = new float[] { 0, 0.2f, 0.4f, 0.6f, 0.8f, 1 };

                var shader = new LinearGradient(canvas.Width - (float)a, (float)b, canvas.Width - (float)c, (float)d, colors, locations, Shader.TileMode.Clamp);
                Paint.SetShader(shader);
                majorPaint.SetShader(shader);
            }

            float verticalPosition = 0;
            int i = 0;
            while (verticalPosition <= screenHeight)
            {
                canvas.DrawLine(0, verticalPosition, screenWidth, verticalPosition, i % MajorGridLineInterval == 0 ? majorPaint : Paint);
                verticalPosition += ConvertDpToPixel((float)VerticalItemSize, Context);
                i++;
            }

            float horizontalPosition = 0;
            i = 0;
            while (horizontalPosition <= screenWidth)
            {
                canvas.DrawLine(horizontalPosition, 0, horizontalPosition, screenHeight, i % MajorGridLineInterval == 0 ? majorPaint : Paint);
                horizontalPosition += ConvertDpToPixel((float)HorizontalItemSize, Context);
                i++;
            }
        }
    }
}
