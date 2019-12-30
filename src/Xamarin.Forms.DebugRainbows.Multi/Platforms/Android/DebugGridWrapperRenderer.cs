﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.DebugRainbows;
using Android.Content;
using AView = Android.Views.View;
using Android.Graphics;
using Android.Util;
using Android.App;
using System.Linq;

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
                    MakeGridRainbows = grid.MakeGridRainbows,
                    Inverse = grid.Inverse,
                    GridOrigin = grid.GridOrigin
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
        public bool MakeGridRainbows { get; set; }
        public bool Inverse { get; set; }
        public DebugGridOrigin GridOrigin { get; set; }

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

            var majorPaint = new Paint();
            var minorPaint = new Paint();

            var colors = new[] {
                Color.FromHex("#f3855b").ToAndroid(),
                Color.FromHex("#fbcf93").ToAndroid(),
                Color.FromHex("#fbe960").ToAndroid(),
                Color.FromHex("#a0e67a").ToAndroid(),
                Color.FromHex("#33c6ee").ToAndroid(),
                Color.FromHex("#c652ba").ToAndroid()
            };

            if (Inverse)
            {
                majorPaint.StrokeWidth = 0;
                majorPaint.Color = GridLineColor.ToAndroid();
                majorPaint.Alpha = (int)(255 * GridLineOpacity);

                var horizontalTotal = 0;
                for (int i = 1; horizontalTotal < screenWidth; i++)
                {
                    var verticalTotal = 0;
                    var horizontalSpacerSize = MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? ConvertDpToPixel((float)MajorGridLineThickness, Context) : ConvertDpToPixel((float)GridLineThickness, Context);

                    for (int j = 1; verticalTotal < screenHeight; j++)
                    {
                        var verticalSpacerSize = MajorGridLineInterval > 0 && j % MajorGridLineInterval == 0 ? ConvertDpToPixel((float)MajorGridLineThickness, Context) : ConvertDpToPixel((float)GridLineThickness, Context);

                        var rectangle = new Rect(
                            (int)horizontalTotal,
                            (int)verticalTotal,
                            (int)(horizontalTotal + ConvertDpToPixel((float)HorizontalItemSize, Context)),
                            (int)(verticalTotal + ConvertDpToPixel((float)VerticalItemSize, Context))
                        );

                        if (MakeGridRainbows)
                        {
                            var color = colors[(i + j) % colors.Length];
                            majorPaint.Color = color;
                        }

                        canvas.DrawRect(rectangle, majorPaint);

                        verticalTotal += (int)(ConvertDpToPixel((float)VerticalItemSize, Context) + verticalSpacerSize);
                    }

                    horizontalTotal += (int)(ConvertDpToPixel((float)HorizontalItemSize, Context) + horizontalSpacerSize);
                }
            }
            else
            {
                if (MakeGridRainbows)
                {
                    var a = canvas.Width * Math.Pow(Math.Sin(2 * Math.PI * ((90 + 0.75) / 2)), 2);
                    var b = canvas.Height * Math.Pow(Math.Sin(2 * Math.PI * ((90 + 0.0) / 2)), 2);
                    var c = canvas.Width * Math.Pow(Math.Sin(2 * Math.PI * ((90 + 0.25) / 2)), 2);
                    var d = canvas.Height * Math.Pow(Math.Sin(2 * Math.PI * ((90 + 0.5) / 2)), 2);

                    var locations = new float[] { 0, 0.2f, 0.4f, 0.6f, 0.8f, 1 };
                    var shader = new LinearGradient(canvas.Width - (float)a, (float)b, canvas.Width - (float)c, (float)d, colors.Select(x => (int)x.ToArgb()).ToArray(), locations, Shader.TileMode.Clamp);

                    minorPaint.SetShader(shader);
                    majorPaint.SetShader(shader);
                }

                majorPaint.StrokeWidth = ConvertDpToPixel((float)MajorGridLineThickness, Context);
                majorPaint.Color = MajorGridLineColor.ToAndroid();
                majorPaint.Alpha = (int)(255 * MajorGridLineOpacity);

                minorPaint.StrokeWidth = ConvertDpToPixel((float)GridLineThickness, Context);
                minorPaint.Color = GridLineColor.ToAndroid();
                minorPaint.Alpha = (int)(255 * GridLineOpacity);

                float verticalPosition = 0;
                int i = 0;
                while (verticalPosition <= screenHeight)
                {
                    canvas.DrawLine(0, verticalPosition, screenWidth, verticalPosition, MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? majorPaint : minorPaint);
                    verticalPosition += ConvertDpToPixel((float)VerticalItemSize, Context);
                    i++;
                }

                float horizontalPosition = 0;
                i = 0;
                while (horizontalPosition <= screenWidth)
                {
                    canvas.DrawLine(horizontalPosition, 0, horizontalPosition, screenHeight, MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? majorPaint : minorPaint);
                    horizontalPosition += ConvertDpToPixel((float)HorizontalItemSize, Context);
                    i++;
                }
            }
        }
    }
}
