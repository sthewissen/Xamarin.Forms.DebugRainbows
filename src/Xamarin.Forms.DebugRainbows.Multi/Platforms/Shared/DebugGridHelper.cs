using System;

namespace Xamarin.Forms.DebugRainbows
{
    public static class DebugGridHelper
    {
        static readonly double DebugGridOpacity = 0.2;
        static readonly Color DebugGridColor = Color.Red;
        static readonly int DebugGridItemSize = 25;

        public static readonly BindableProperty IsDebugProperty =
            BindableProperty.CreateAttached("IsDebug", typeof(bool), typeof(VisualElement), default(bool), propertyChanged: (b, o, n) => OnIsDebugChanged(b, (bool)o, (bool)n));

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

            Grid gridContent = new Grid
            {
                InputTransparent = true,
                Opacity = DebugGridOpacity
            };

            double max = Math.Max(page.Width, page.Height);

            for (int x = 24; x < max; x += 37)
            {
                for (int y = 24; y < max; y += 37)
                {
                    var rect = new BoxView
                    {
                        WidthRequest = DebugGridItemSize,
                        HeightRequest = DebugGridItemSize,
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        Margin = new Thickness(x, y, 0, 0),
                        Color = DebugGridColor
                    };

                    gridContent.Children.Add(rect);
                }
            }

            Grid newContent = new Grid();
            newContent.Children.Add(pageContent);
            newContent.Children.Add(gridContent);

            page.Content = newContent;
        }
    }
}