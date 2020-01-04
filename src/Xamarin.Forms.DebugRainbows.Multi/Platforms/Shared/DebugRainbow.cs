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
        public static readonly BindableProperty HorizontalItemSizeProperty = BindableProperty.CreateAttached("HorizontalItemSize", typeof(double), typeof(Page), 10.0);
        public static readonly BindableProperty VerticalItemSizeProperty = BindableProperty.CreateAttached("VerticalItemSize", typeof(double), typeof(Page), 10.0);

        public static readonly BindableProperty MajorGridLineIntervalProperty = BindableProperty.CreateAttached("MajorGridLineInterval", typeof(int), typeof(Page), 0);
        public static readonly BindableProperty MajorGridLineColorProperty = BindableProperty.CreateAttached("MajorGridLineColor", typeof(Color), typeof(Page), Color.Red);
        public static readonly BindableProperty MajorGridLineOpacityProperty = BindableProperty.CreateAttached("MajorGridLineOpacity", typeof(double), typeof(Page), 1.0);
        public static readonly BindableProperty MajorGridLineWidthProperty = BindableProperty.CreateAttached("MajorGridLineWidth", typeof(double), typeof(Page), 3.0);

        public static readonly BindableProperty GridLineColorProperty = BindableProperty.CreateAttached("GridLineColor", typeof(Color), typeof(Page), Color.Red);
        public static readonly BindableProperty GridLineOpacityProperty = BindableProperty.CreateAttached("GridLineOpacity", typeof(double), typeof(Page), 1.0);
        public static readonly BindableProperty GridLineWidthProperty = BindableProperty.CreateAttached("GridLineWidth", typeof(double), typeof(Page), 1.0);
        public static readonly BindableProperty GridPaddingProperty = BindableProperty.CreateAttached("GridPadding", typeof(Thickness), typeof(Page), default(Thickness));

        public static readonly BindableProperty ShowGridProperty = BindableProperty.CreateAttached("ShowGrid", typeof(bool), typeof(Page), default(bool), propertyChanged: (b, o, n) => OnShowDebugModeChanged(b, (bool)o, (bool)n));
        public static readonly BindableProperty MakeGridRainbowsProperty = BindableProperty.CreateAttached("MakeGridRainbows", typeof(bool), typeof(Page), default(bool));
        public static readonly BindableProperty InverseProperty = BindableProperty.CreateAttached("Inverse", typeof(bool), typeof(Page), default(bool));

        public static readonly BindableProperty GridOriginProperty = BindableProperty.CreateAttached("GridOrigin", typeof(DebugGridOrigin), typeof(Page), defaultValue: DebugGridOrigin.Center);

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

        public static void SetGridLineColor(BindableObject b, Color value)
        {
            b.SetValue(GridLineColorProperty, value);
        }

        public static Color GetGridLineColor(BindableObject b)
        {
            return (Color)b.GetValue(GridLineColorProperty);
        }

        public static void SetGridLineWidth(BindableObject b, Color value)
        {
            b.SetValue(GridLineWidthProperty, value);
        }

        public static double GetGridLineWidth(BindableObject b)
        {
            return (double)b.GetValue(GridLineWidthProperty);
        }

        public static void SetMajorGridLineWidth(BindableObject b, double value)
        {
            b.SetValue(MajorGridLineWidthProperty, value);
        }

        public static double GetMajorGridLineWidth(BindableObject b)
        {
            return (double)b.GetValue(MajorGridLineWidthProperty);
        }

        public static void SetGridLineOpacity(BindableObject b, Color value)
        {
            b.SetValue(GridLineOpacityProperty, value);
        }

        public static double GetGridLineOpacity(BindableObject b)
        {
            return (double)b.GetValue(GridLineOpacityProperty);
        }

        public static void SetMajorGridLineOpacity(BindableObject b, double value)
        {
            b.SetValue(MajorGridLineOpacityProperty, value);
        }

        public static double GetMajorGridLineOpacity(BindableObject b)
        {
            return (double)b.GetValue(MajorGridLineOpacityProperty);
        }

        public static void SetHorizontalItemSize(BindableObject b, double value)
        {
            b.SetValue(HorizontalItemSizeProperty, value);
        }

        public static double GetHorizontalItemSize(BindableObject b)
        {
            return (double)b.GetValue(HorizontalItemSizeProperty);
        }

        public static void SetVerticalItemSize(BindableObject b, double value)
        {
            b.SetValue(VerticalItemSizeProperty, value);
        }

        public static double GetVerticalItemSize(BindableObject b)
        {
            return (double)b.GetValue(VerticalItemSizeProperty);
        }

        public static void SetMakeGridRainbows(BindableObject b, bool value)
        {
            b.SetValue(MakeGridRainbowsProperty, value);
        }

        public static bool GetMakeGridRainbows(BindableObject b)
        {
            return (bool)b.GetValue(MakeGridRainbowsProperty);
        }

        public static void SetInverse(BindableObject b, bool value)
        {
            b.SetValue(InverseProperty, value);
        }

        public static bool GetInverse(BindableObject b)
        {
            return (bool)b.GetValue(InverseProperty);
        }

        public static void SetShowGrid(BindableObject b, bool value)
        {
            b.SetValue(ShowGridProperty, value);
        }

        public static bool GetShowGrid(BindableObject b)
        {
            return (bool)b.GetValue(ShowGridProperty);
        }

        public static void SetGridOrigin(BindableObject b, DebugGridOrigin value)
        {
            b.SetValue(GridOriginProperty, value);
        }

        public static DebugGridOrigin GetGridOrigin(BindableObject b)
        {
            return (DebugGridOrigin)b.GetValue(GridOriginProperty);
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

                // Size Changed gets called in time to size the actual initial grid.
                // However, it doesn't get called when using Hot Reload, so we also hook up Appearing.
                // Inside of the handler we check whether or not the Grid overlay has already been added.
                if (showGrid)
                    page.SizeChanged += Page_SizeChanged;
                else
                    page.SizeChanged -= Page_SizeChanged;
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

        private static void Page_SizeChanged(object sender, EventArgs e)
        {
#if DEBUG
            if (sender.GetType().IsSubclassOf(typeof(ContentPage)))
            {
                BuildGrid(sender as ContentPage);
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

                        if (showColors)
                            IterateChildren(((ContentPage)item).Content);
                    }
                }
            }
#endif
        }

        private static void BuildGrid(ContentPage page)
        {
            // Check for the class ID to only add the grid once.
            if (page.Content.ClassId != nameof(DebugRainbow))
            {
                View pageContent = page.Content;
                page.Content = null;

                var gridContent = new DebugGridWrapper
                {
                    HorizontalItemSize = GetHorizontalItemSize(page),
                    VerticalItemSize = GetVerticalItemSize(page),
                    MajorGridLineColor = GetMajorGridLineColor(page),
                    GridLineColor = GetGridLineColor(page),
                    MajorGridLineOpacity = GetMajorGridLineOpacity(page),
                    GridLineOpacity = GetGridLineOpacity(page),
                    MajorGridLineInterval = GetMajorGridLineInterval(page),
                    MajorGridLineWidth = GetMajorGridLineWidth(page),
                    GridLineWidth = GetGridLineWidth(page),
                    Margin = GetGridPadding(page),
                    MakeGridRainbows = GetMakeGridRainbows(page),
                    Inverse = GetInverse(page),
                    HeightRequest = pageContent.Height,
                    WidthRequest = pageContent.Width,
                    GridOrigin = GetGridOrigin(page)
                };

                Grid newContent = new Grid()
                {
                    ClassId = nameof(DebugRainbow),
                    HeightRequest = pageContent.Height,
                    WidthRequest = pageContent.Width
                };

                newContent.Children.Add(pageContent);
                newContent.Children.Add(gridContent);

                page.Content = newContent;
            }
        }

        private static void IterateChildren(Element content)
        {
            if (content != null)
            {
                if (content.GetType().IsSubclassOf(typeof(Layout)))
                {
                    if (content.GetType() != typeof(DebugGridWrapper))
                    {
                        ((Layout)content).BackgroundColor = GetRandomColor();
                    }

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
