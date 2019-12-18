using System;

namespace Xamarin.Forms.DebugRainbows
{
    public static class DebugGrid
    {
        public static readonly BindableProperty HorizontalSpacingProperty = BindableProperty.CreateAttached("HorizontalSpacing", typeof(double), typeof(Page), 10.0);
        public static readonly BindableProperty VerticalSpacingProperty = BindableProperty.CreateAttached("VerticalSpacing", typeof(double), typeof(Page), 10.0);

        public static readonly BindableProperty MajorGridLineIntervalProperty = BindableProperty.CreateAttached("MajorGridLineInterval", typeof(int), typeof(Page), 4);
        public static readonly BindableProperty MajorGridLineColorProperty = BindableProperty.CreateAttached("MajorGridLineColor", typeof(Color), typeof(Page), Color.Red);
        public static readonly BindableProperty MinorGridLineColorProperty = BindableProperty.CreateAttached("MinorGridLineColor", typeof(Color), typeof(Page), Color.Red);
        public static readonly BindableProperty MajorGridLineOpacityProperty = BindableProperty.CreateAttached("MajorGridLineOpacity", typeof(double), typeof(Page), 1.0);
        public static readonly BindableProperty MinorGridLineOpacityProperty = BindableProperty.CreateAttached("MinorGridLineOpacity", typeof(double), typeof(Page), 1.0);
        public static readonly BindableProperty MajorGridLineWidthProperty = BindableProperty.CreateAttached("MajorGridLineWidth", typeof(double), typeof(Page), 3.0);
        public static readonly BindableProperty MinorGridLineWidthProperty = BindableProperty.CreateAttached("MinorGridLineWidth", typeof(double), typeof(Page), 1.0);

        public static readonly BindableProperty PaddingProperty = BindableProperty.CreateAttached("Padding", typeof(Thickness), typeof(Page), default(Thickness));

        public static readonly BindableProperty IsDebugProperty = BindableProperty.CreateAttached("IsDebug", typeof(bool), typeof(Page), default(bool), propertyChanged: (b, o, n) => OnIsDebugChanged(b, (bool)o, (bool)n));

        public static void SetPadding(BindableObject b, Thickness value)
        {
            b.SetValue(PaddingProperty, value);
        }

        public static Thickness GetPadding(BindableObject b)
        {
            return (Thickness)b.GetValue(PaddingProperty);
        }

        public static void SetMajorGridLineInterval(BindableObject b, int value)
        {
            b.SetValue(MajorGridLineIntervalProperty, value);
        }

        public static int GetMajorGridLineInterval(BindableObject b)
        {
            return (int)b.GetValue(MajorGridLineIntervalProperty);
        }

        public static void SetMajorGridLineColor(BindableObject b, Color value)
        {
            b.SetValue(MajorGridLineColorProperty, value);
        }

        public static Color GetMajorGridLineColor(BindableObject b)
        {
            return (Color)b.GetValue(MajorGridLineColorProperty);
        }

        public static void SetMinorGridLineColor(BindableObject b, Color value)
        {
            b.SetValue(MinorGridLineColorProperty, value);
        }

        public static Color GetMinorGridLineColor(BindableObject b)
        {
            return (Color)b.GetValue(MinorGridLineColorProperty);
        }

        public static void SetMinorGridLineWidth(BindableObject b, Color value)
        {
            b.SetValue(MinorGridLineWidthProperty, value);
        }

        public static double GetMinorGridLineWidth(BindableObject b)
        {
            return (double)b.GetValue(MinorGridLineWidthProperty);
        }

        public static void SetMajorGridLineWidth(BindableObject b, double value)
        {
            b.SetValue(MajorGridLineWidthProperty, value);
        }

        public static double GetMajorGridLineWidth(BindableObject b)
        {
            return (double)b.GetValue(MajorGridLineWidthProperty);
        }

        public static void SetMinorGridLineOpacity(BindableObject b, Color value)
        {
            b.SetValue(MinorGridLineOpacityProperty, value);
        }

        public static double GetMinorGridLineOpacity(BindableObject b)
        {
            return (double)b.GetValue(MinorGridLineOpacityProperty);
        }

        public static void SetMajorGridLineOpacity(BindableObject b, double value)
        {
            b.SetValue(MajorGridLineOpacityProperty, value);
        }

        public static double GetMajorGridLineOpacity(BindableObject b)
        {
            return (double)b.GetValue(MajorGridLineOpacityProperty);
        }

        public static void SetHorizontalSpacing(BindableObject b, double value)
        {
            b.SetValue(HorizontalSpacingProperty, value);
        }

        public static double GetHorizontalSpacing(BindableObject b)
        {
            return (double)b.GetValue(HorizontalSpacingProperty);
        }

        public static void SetVerticalSpacing(BindableObject b, double value)
        {
            b.SetValue(VerticalSpacingProperty, value);
        }

        public static double GetVerticalSpacing(BindableObject b)
        {
            return (double)b.GetValue(VerticalSpacingProperty);
        }

        public static void SetIsDebug(BindableObject b, bool value)
        {
            b.SetValue(IsDebugProperty, value);
        }

        public static bool GetIsDebug(BindableObject b)
        {
            return (bool)b.GetValue(IsDebugProperty);
        }

        static void OnIsDebugChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
#if DEBUG
            if (bindable.GetType().IsSubclassOf(typeof(Page)))
            {
                if (newValue)
                    (bindable as ContentPage).SizeChanged += Page_SizeChanged;
                else
                    (bindable as ContentPage).SizeChanged -= Page_SizeChanged;
            }
#endif
        }

        static void Page_SizeChanged(object sender, EventArgs e)
        {
#if DEBUG
            if (sender.GetType().IsSubclassOf(typeof(ContentPage)))
            {
                BuildGrid(sender as ContentPage);
            }
#endif
        }

        private static void BuildGrid(ContentPage page)
        {
            View pageContent = page.Content;
            page.Content = null;

            var gridContent = new DebugGridWrapper
            {
                InputTransparent = true,
                HorizontalSpacing = GetHorizontalSpacing(page),
                VerticalSpacing = GetVerticalSpacing(page),
                MajorGridLineColor = GetMajorGridLineColor(page),
                MinorGridLineColor = GetMinorGridLineColor(page),
                MajorGridLineOpacity = GetMajorGridLineOpacity(page),
                MinorGridLineOpacity = GetMinorGridLineOpacity(page),
                MajorGridLineInterval = GetMajorGridLineInterval(page),
                MajorGridLineWidth = GetMajorGridLineWidth(page),
                MinorGridLineWidth = GetMinorGridLineWidth(page),
                Padding = GetPadding(page)
            };

            Grid newContent = new Grid();
            newContent.Children.Add(pageContent);
            newContent.Children.Add(gridContent);

            page.Content = newContent;
        }
    }
}