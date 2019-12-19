using System;

namespace Xamarin.Forms.DebugRainbows
{
    public static class DebugRainbow
    {
        private static readonly Random _randomGen = new Random();

        #region obsolete

        [Obsolete("This property has been made obsolete. Please use 'ShowColors' instead.")]
        public static readonly BindableProperty IsDebugProperty = BindableProperty.CreateAttached("IsDebug", typeof(bool), typeof(VisualElement), default(bool));

        public static void SetIsDebug(BindableObject b, bool value)
        {
            b.SetValue(IsDebugProperty, value);
        }

        public static bool GetIsDebug(BindableObject b)
        {
            return (bool)b.GetValue(IsDebugProperty);
        }

        #endregion

        public static readonly BindableProperty ShowColorsProperty = BindableProperty.CreateAttached("ShowColors", typeof(bool), typeof(VisualElement), default(bool), propertyChanged: (b, o, n) => OnShowDebugModeChanged(b, (bool)o, (bool)n));
        public static readonly BindableProperty HorizontalSpacingProperty = BindableProperty.CreateAttached("HorizontalSpacing", typeof(double), typeof(Page), 10.0);
        public static readonly BindableProperty VerticalSpacingProperty = BindableProperty.CreateAttached("VerticalSpacing", typeof(double), typeof(Page), 10.0);

        public static readonly BindableProperty MajorGridLineIntervalProperty = BindableProperty.CreateAttached("MajorGridLineInterval", typeof(int), typeof(Page), 4);
        public static readonly BindableProperty MajorGridLineColorProperty = BindableProperty.CreateAttached("MajorGridLineColor", typeof(Color), typeof(Page), Color.Red);
        public static readonly BindableProperty MinorGridLineColorProperty = BindableProperty.CreateAttached("MinorGridLineColor", typeof(Color), typeof(Page), Color.Red);
        public static readonly BindableProperty MajorGridLineOpacityProperty = BindableProperty.CreateAttached("MajorGridLineOpacity", typeof(double), typeof(Page), 1.0);
        public static readonly BindableProperty MinorGridLineOpacityProperty = BindableProperty.CreateAttached("MinorGridLineOpacity", typeof(double), typeof(Page), 1.0);
        public static readonly BindableProperty MajorGridLineWidthProperty = BindableProperty.CreateAttached("MajorGridLineWidth", typeof(double), typeof(Page), 3.0);
        public static readonly BindableProperty MinorGridLineWidthProperty = BindableProperty.CreateAttached("MinorGridLineWidth", typeof(double), typeof(Page), 1.0);

        public static readonly BindableProperty GridPaddingProperty = BindableProperty.CreateAttached("GridPadding", typeof(Thickness), typeof(Page), default(Thickness));

        public static readonly BindableProperty ShowGridProperty = BindableProperty.CreateAttached("ShowGrid", typeof(bool), typeof(Page), default(bool), propertyChanged: (b, o, n) => OnShowDebugModeChanged(b, (bool)o, (bool)n));
        public static readonly BindableProperty MakeGridRainbowsProperty = BindableProperty.CreateAttached("MakeGridRainbows", typeof(bool), typeof(Page), default(bool));

        public static void SetShowColors(BindableObject b, bool value)
        {
            b.SetValue(ShowColorsProperty, value);
        }

        public static bool GetShowColors(BindableObject b)
        {
            return (bool)b.GetValue(ShowColorsProperty);
        }

        public static void SetGridPadding(BindableObject b, Thickness value)
        {
            b.SetValue(GridPaddingProperty, value);
        }

        public static Thickness GetGridPadding(BindableObject b)
        {
            return (Thickness)b.GetValue(GridPaddingProperty);
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

        public static void SetMakeGridRainbows(BindableObject b, bool value)
        {
            b.SetValue(MakeGridRainbowsProperty, value);
        }

        public static bool GetMakeGridRainbows(BindableObject b)
        {
            return (bool)b.GetValue(MakeGridRainbowsProperty);
        }

        public static void SetShowGrid(BindableObject b, bool value)
        {
            b.SetValue(ShowGridProperty, value);
        }

        public static bool GetShowGrid(BindableObject b)
        {
            return (bool)b.GetValue(ShowGridProperty);
        }

        static void OnShowDebugModeChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
#if DEBUG
            var showColors = GetShowColors(bindable);
            var showGrid = GetShowGrid(bindable);

            // Property changed implementation goes here
            if (bindable.GetType().IsSubclassOf(typeof(Page)))
            {
                var page = (bindable as Page);

                if (showColors || showGrid)
                    page.Appearing += Page_Appearing;
                else
                    page.Appearing -= Page_Appearing;
            }
            else if (bindable.GetType().IsSubclassOf(typeof(View)))
            {
                if (showColors)
                    (bindable as View).SizeChanged += View_SizeChanged;
                else
                    (bindable as View).SizeChanged -= View_SizeChanged;
            }
#endif
        }

        static void View_SizeChanged(object sender, EventArgs e)
        {
#if DEBUG
            if (sender.GetType().IsSubclassOf(typeof(View)))
            {
                IterateChildren((sender as View));
            }
#endif
        }

        static void Page_Appearing(object sender, EventArgs e)
        {
#if DEBUG
            if (sender.GetType().IsSubclassOf(typeof(ContentPage)))
            {
                var showColors = GetShowColors(sender as Page);
                var showGrid = GetShowGrid(sender as Page);

                if (showColors)
                    IterateChildren((sender as ContentPage).Content);

                if (showGrid)
                    BuildGrid(sender as ContentPage);
            }
            else if (sender is IViewContainer<Page>)
            {
                var tabbedPage = sender as IViewContainer<Page>;

                foreach (var item in tabbedPage.Children)
                {
                    if (item is ContentPage)
                    {
                        var showColors = GetShowColors(sender as Page);
                        var showGrid = GetShowGrid(sender as Page);

                        if (showColors)
                            IterateChildren(((ContentPage)item).Content);

                        if (showGrid)
                            BuildGrid((ContentPage)item);
                    }
                }
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
                Margin = GetGridPadding(page),
                MakeGridRainbows = GetMakeGridRainbows(page),
            };

            Grid newContent = new Grid();
            newContent.Children.Add(pageContent);
            newContent.Children.Add(gridContent);

            page.Content = newContent;
        }

        private static void IterateChildren(Element content)
        {
            if (content != null)
            {
                if (content.GetType().IsSubclassOf(typeof(Layout)))
                {
                    ((Layout)content).BackgroundColor = GetRandomColor();

                    foreach (var item in ((Layout)content).Children)
                    {
                        IterateChildren(item);
                    }
                }
                else if (content.GetType().IsSubclassOf(typeof(View)))
                {
                    ((View)content).BackgroundColor = GetRandomColor();
                }
            }
        }

        private static Color GetRandomColor()
        {
            var color = Color.FromRgb((byte)_randomGen.Next(0, 255), (byte)_randomGen.Next(0, 255), (byte)_randomGen.Next(0, 255));
            return color;
        }
    }
}
