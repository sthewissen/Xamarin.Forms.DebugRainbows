<img src="https://github.com/sthewissen/Xamarin.Forms.DebugRainbows/blob/master/images/icon.png" width="150px" />

# Xamarin.Forms.DebugRainbows
The package you didn't even know you needed!

[![Build status](https://sthewissen.visualstudio.com/DebugRainbows/_apis/build/status/DebugRainbows-Deployment-CI)]() [![NuGet](https://img.shields.io/nuget/vpre/Xamarin.Forms.DebugRainbows.svg)](https://www.nuget.org/packages/Xamarin.Forms.DebugRainbows)

## Why DebugRainbows?

Have you ever had a piece of XAML code that didn't produce the layout you expected? Did you change background colors on certain elements to get an idea of where they are positioned? Admit it, you have and pretty much all of us have at some point. Either way, this is the package for you! It adds some nice colorful debug modes to your `ContentPage`s or specific visual elements that lets you immediately see where all of your elements are located!

<img src="https://raw.githubusercontent.com/sthewissen/Xamarin.Forms.DebugRainbows/master/images/sample.png" />

## API Reference

| Property | What it does |
| ------ | ------ |
| `GridLineColor` | Defines a color for the grid lines or blocks (depending on `Inverse`). |
| `GridLineOpacity` | The opacity of the grid lines in the overlay. | 
| `GridLineWidth` | The width of the grid lines or between each block (depending on `Inverse`). | 
| `GridPadding` | Pads the entire overlay. Takes a `Thickness` object. | 
| `HorizontalSpacing` | Width between grid lines or the width of the blocks (depending on `Inverse`). |
| `Inverse` | Either draws grid lines (`false`) or block view (`true`). | 
| `MajorGridLineColor` | When using major grid lines you can color them differently. | 
| `MajorGridLineInterval` | Defines the interval of when a major grid line should be drawn. | 
| `MajorGridLineOpacity` | The opacity of the major grid lines in the overlay. | 
| `MajorGridLineWidth` | The width of the major grid lines or space between block (depending on `Inverse`).  | 
| `MakeGridRainbows` | Throws some instant joy into your overlays. | 
| `ShowColors` | Automatically gives every visual element a random background color. |  
| `ShowGrid` | Draws a customizable grid overlay used to help you align elements. |  
| `VerticalItemSize` | Height between grid lines or the height of the blocks (depending on `Inverse`). | 

## How to use it?

The project is up on NuGet at the following URL:

https://www.nuget.org/packages/Xamarin.Forms.DebugRainbows

Install this package into your shared project and your platform specific projects. After that you're good to go! Simply add the namespace declaration and set the `ShowColors` or `ShowGrid` attached property to `true`!

### XAML UI

**Apply to an individual `Xamarin.Forms.ContentPage`**

```xml
<ContentPage rainbows:DebugRainbow.ShowColors="true"
   xmlns="http://xamarin.com/schemas/2014/forms" 
   xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" 
   xmlns:rainbows="clr-namespace:Xamarin.Forms.DebugRainbows;assembly=Xamarin.Forms.DebugRainbows" 
   x:Class="MyNamespace.MainPage">
             
  ...
             
</ContentPage>
```

**Apply to every `Xamarin.Forms.ContentPage`**

In `App.xaml`, we can add a `Style` to our `ResourceDictionary`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Application xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             mc:Ignorable="d"
             x:Class="MyNamespace.App"
             xmlns:rainbows="clr-namespace:Xamarin.Forms.DebugRainbows;assembly=Xamarin.Forms.DebugRainbows" >
    <Application.Resources>
        <ResourceDictionary>
            <Style TargetType="ContentPage" ApplyToDerivedTypes="True">
               <Setter Property="rainbows:DebugRainbow.ShowColors" Value="True" />
            </Style>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

### Coded UI

**Apply to an individual `Xamarin.Forms.ContentPage`**

```csharp
public MyContentPage : ContentPage
{
    Xamarin.Forms.DebugRainbows.DebugRainbow.SetShowColors(this, true);
}
```

**Apply to every `Xamarin.Forms.ContentPage`**

```csharp
public class App : Xamarin.Forms.Application
{
    public App()
    {
    
#if DEBUG
        EnableDebugRainbows(true);
#endif
        
        //...
    });
    
    void EnableDebugRainbows(bool shouldUseDebugRainbows)
    {
        Resources.Add(new Style(typeof(ContentPage))
        {
            ApplyToDerivedTypes = true,
            Setters = {
                new Setter
                {
                    Property = Xamarin.Forms.DebugRainbows.DebugRainbow.ShowColorsProperty,
                    Value = shouldUseDebugRainbows
                }
            }
        });
    }
 }
 ```
            


