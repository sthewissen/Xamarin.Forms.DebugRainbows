using System;
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
                    HorizontalItemSize = (float)grid.HorizontalItemSize,
                    VerticalItemSize = (float)grid.VerticalItemSize,
                    MajorGridLineInterval = grid.MajorGridLineInterval,
                    MajorGridLineColor = grid.MajorGridLineColor,
                    GridLineColor = grid.GridLineColor,
                    MajorGridLineOpacity = (float)grid.MajorGridLineOpacity,
                    GridLineOpacity = (float)grid.GridLineOpacity,
                    MajorGridLineThickness = (float)grid.MajorGridLineWidth,
                    GridLineThickness = (float)grid.GridLineWidth,
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

        public float HorizontalItemSize { get; set; }
        public float VerticalItemSize { get; set; }
        public int MajorGridLineInterval { get; set; }
        public Color MajorGridLineColor { get; set; }
        public Color GridLineColor { get; set; }
        public float MajorGridLineOpacity { get; set; }
        public float GridLineOpacity { get; set; }
        public float MajorGridLineThickness { get; set; }
        public float GridLineThickness { get; set; }
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

            // Make these into true pixels from DP.
            HorizontalItemSize = ConvertDpToPixel(HorizontalItemSize, Context);
            VerticalItemSize = ConvertDpToPixel(VerticalItemSize, Context);
            MajorGridLineThickness = ConvertDpToPixel(MajorGridLineThickness, Context);
            GridLineThickness = ConvertDpToPixel(GridLineThickness, Context);

            if (Inverse)
            {
                DrawInverse(canvas, majorPaint, colors);
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

                DrawNormal(canvas, majorPaint, minorPaint);
            }
        }

        private void DrawNormal(Canvas canvas, Paint majorPaint, Paint minorPaint)
        {
            majorPaint.StrokeWidth = MajorGridLineThickness;
            majorPaint.Color = MajorGridLineColor.ToAndroid();
            majorPaint.Alpha = (int)(255 * MajorGridLineOpacity);

            minorPaint.StrokeWidth = GridLineThickness;
            minorPaint.Color = GridLineColor.ToAndroid();
            minorPaint.Alpha = (int)(255 * GridLineOpacity);

            if (GridOrigin == DebugGridOrigin.TopLeft)
            {
                float verticalPosition = 0;
                int i = 0;
                while (verticalPosition <= screenHeight)
                {
                    canvas.DrawLine(0, verticalPosition, screenWidth, verticalPosition, MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? majorPaint : minorPaint);
                    verticalPosition += VerticalItemSize;
                    i++;
                }

                float horizontalPosition = 0;
                i = 0;
                while (horizontalPosition <= screenWidth)
                {
                    canvas.DrawLine(horizontalPosition, 0, horizontalPosition, screenHeight, MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? majorPaint : minorPaint);
                    horizontalPosition += HorizontalItemSize;
                    i++;
                }
            }
            else if (GridOrigin == DebugGridOrigin.Center)
            {
                var gridLinesHorizontalCenter = screenWidth / 2;
                var gridLinesVerticalCenter = screenHeight / 2;
                var amountOfVerticalLines = screenWidth / HorizontalItemSize;
                var amountOfHorizontalLines = screenHeight / VerticalItemSize;

                // Draw the horizontal lines.
                for (int i = 0; i < (amountOfHorizontalLines / 2); i++)
                {
                    canvas.DrawLine(
                        startX: 0,
                        startY: gridLinesVerticalCenter + (i * VerticalItemSize),
                        stopX: screenWidth,
                        stopY: gridLinesVerticalCenter + (i * VerticalItemSize),
                        paint: MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? majorPaint : minorPaint
                    );

                    canvas.DrawLine(
                        startX: 0,
                        startY: gridLinesVerticalCenter - (i * VerticalItemSize),
                        stopX: screenWidth,
                        stopY: gridLinesVerticalCenter - (i * VerticalItemSize),
                        paint: MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? majorPaint : minorPaint
                    );
                }

                // Draw vertical lines.
                for (int i = 0; i < (amountOfVerticalLines / 2); i++)
                {
                    canvas.DrawLine(
                        startX: gridLinesHorizontalCenter + (i * HorizontalItemSize),
                        startY: 0,
                        stopX: gridLinesHorizontalCenter + (i * HorizontalItemSize),
                        stopY: screenHeight,
                        paint: MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? majorPaint : minorPaint
                    );

                    canvas.DrawLine(
                        startX: gridLinesHorizontalCenter - (i * HorizontalItemSize),
                        startY: 0,
                        stopX: gridLinesHorizontalCenter - (i * HorizontalItemSize),
                        stopY: screenHeight,
                        paint: MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? majorPaint : minorPaint
                    );
                }
            }
        }

        private void DrawInverse(Canvas canvas, Paint majorPaint, global::Android.Graphics.Color[] colors)
        {
            majorPaint.StrokeWidth = 0;
            majorPaint.Color = GridLineColor.ToAndroid();
            majorPaint.Alpha = (int)(255 * GridLineOpacity);

            if (GridOrigin == DebugGridOrigin.TopLeft)
            {
                var horizontalTotal = 0;
                for (int i = 1; horizontalTotal < screenWidth; i++)
                {
                    var verticalTotal = 0;
                    var horizontalSpacerSize = MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? MajorGridLineThickness : GridLineThickness;

                    for (int j = 1; verticalTotal < screenHeight; j++)
                    {
                        var verticalSpacerSize = MajorGridLineInterval > 0 && j % MajorGridLineInterval == 0 ? MajorGridLineThickness : GridLineThickness;

                        var rectangle = new Rect(
                            (int)horizontalTotal,
                            (int)verticalTotal,
                            (int)(horizontalTotal + HorizontalItemSize),
                            (int)(verticalTotal + VerticalItemSize)
                        );

                        if (MakeGridRainbows)
                        {
                            var color = colors[(i + j) % colors.Length];
                            majorPaint.Color = color;
                        }

                        canvas.DrawRect(rectangle, majorPaint);

                        verticalTotal += (int)(VerticalItemSize + verticalSpacerSize);
                    }

                    horizontalTotal += (int)(HorizontalItemSize + horizontalSpacerSize);
                }
            }
            else if (GridOrigin == DebugGridOrigin.Center)
            {
                var horizontalRightTotal = (screenWidth / 2) + (int)((MajorGridLineInterval > 0 ? MajorGridLineThickness : GridLineThickness) / 2);
                var horizontalLeftTotal = (screenWidth / 2) - (int)(HorizontalItemSize + ((MajorGridLineInterval > 0 ? MajorGridLineThickness : GridLineThickness) / 2));

                for (int i = 1; horizontalRightTotal < screenWidth; i++)
                {
                    var horizontalSpacerSize = MajorGridLineInterval > 0 && i % MajorGridLineInterval == 0 ? MajorGridLineThickness : GridLineThickness;
                    var verticalBottomTotal = (screenHeight / 2) + (int)((MajorGridLineInterval > 0 ? MajorGridLineThickness : GridLineThickness) / 2);
                    var verticalTopTotal = (screenHeight / 2) - (int)(VerticalItemSize + ((MajorGridLineInterval > 0 ? MajorGridLineThickness : GridLineThickness) / 2));

                    for (int j = 1; verticalBottomTotal < screenHeight; j++)
                    {
                        if (MakeGridRainbows)
                        {
                            var color = colors[(i + j) % colors.Length];
                            majorPaint.Color = color;
                        }

                        var verticalSpacerSize = MajorGridLineInterval > 0 && j % MajorGridLineInterval == 0 ? MajorGridLineThickness : GridLineThickness;

                        var rectangle = new Rect(horizontalRightTotal, verticalBottomTotal, (int)(horizontalRightTotal + HorizontalItemSize), (int)(verticalBottomTotal + VerticalItemSize));
                        canvas.DrawRect(rectangle, majorPaint);

                        var rectangle2 = new Rect(horizontalLeftTotal, verticalTopTotal, (int)(horizontalLeftTotal + HorizontalItemSize), (int)(verticalTopTotal + VerticalItemSize));
                        canvas.DrawRect(rectangle2, majorPaint);

                        var rectangle3 = new Rect(horizontalRightTotal, verticalTopTotal, (int)(horizontalRightTotal + HorizontalItemSize), (int)(verticalTopTotal + VerticalItemSize));
                        canvas.DrawRect(rectangle3, majorPaint);

                        var rectangle4 = new Rect(horizontalLeftTotal, verticalBottomTotal, (int)(horizontalLeftTotal + HorizontalItemSize), (int)(verticalBottomTotal + VerticalItemSize));
                        canvas.DrawRect(rectangle4, majorPaint);

                        verticalTopTotal -= (int)(VerticalItemSize + verticalSpacerSize);
                        verticalBottomTotal += (int)(VerticalItemSize + verticalSpacerSize);
                    }

                    horizontalRightTotal += (int)(HorizontalItemSize + horizontalSpacerSize);
                    horizontalLeftTotal -= (int)(HorizontalItemSize + horizontalSpacerSize);
                }
            }
        }
    }
}
