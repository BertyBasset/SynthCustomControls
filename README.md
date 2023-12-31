# SynthCustomControls
WPF Controls for Virtual Analog Synth Project - primarily a flexible WPF Knob control

## Basic Usage

Add a reference to either SynthCustomControls.dll or SynthCustomControls.csproj

```
XAML:
<Window x:Class="Testbed.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:sys="clr-namespace:System;assembly=mscorlib"
        xmlns:custom="clr-namespace:SynthCustomControls;assembly=SynthCustomControls"
        Title="MainWindow" Height="400" Width="800">
    <Canvas>
        <custom:Knob Width="70" Name="knob1"  Canvas.Left="144" Canvas.Top="75">
            <custom:Knob.Value>0</custom:Knob.Value>
            <custom:Knob.ValueMin>0</custom:Knob.ValueMin>
            <custom:Knob.ValueMax>1</custom:Knob.ValueMax>
            <custom:Knob.FullSweepAngle>270</custom:Knob.FullSweepAngle>
        </custom:Knob>

        <TextBox Name="txtValue" Canvas.Left="105" Canvas.Top="113" HorizontalAlignment="Left" VerticalAlignment="Center" Width="81"/>
    </Canvas>
</Window>

Codebehind:
public MainWindow() {
    InitializeComponent();
    knob1.ValueChanged += (o, e) => {
          txtValue.Text = $"{e:F3}";
    };
}
```
`MinValue` is set to 0, `MaxValue` is set to 1, `FullSweepAngle` is set to 270°, so dragging the knob marker with the mouse gives continuous variation of `Value` (note textbox is separate control):
```
<custom:Knob.ValueMin>0</custom:Knob.ValueMin>
<custom:Knob.ValueMax>1</custom:Knob.ValueMax>
<custom:Knob.FullSweepAngle>270</custom:Knob.FullSweepAngle>
```

![Basic Use](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/f65f9baa8b5cdbb6aa57f0c10067136f298932e8/ReadmeImages/BasicUse.png)

`MinValue` and `MaxValue` can be set to any valid positive or negative double value as long as `MaxValue` > `MinValue`. `FullSweepAngle` can be set to any angle between 20° and 340°. The `Value` property will reflect the current knob marker position taking the aforementioned properties into account. e.g. `MinValue` = -1, `MaxValue` = +3, `FullSweepAngle` = 180°
```
<custom:Knob.ValueMin>-1</custom:Knob.ValueMin>
<custom:Knob.ValueMax>3</custom:Knob.ValueMax>
<custom:Knob.FullSweepAngle>180</custom:Knob.FullSweepAngle>
```
![Basic Use 2](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/edb8abd20c17686b8e2d78a3b60b454cad82a0b2/ReadmeImages/BasicUse2.png)

### ValueChanged Event
The Knob control has a single event that is fired whenever the `Value` property changes. It passes an event argument of type double representing the `Value` property. An event handler can be specified in the XAML, or in codebehind:
```
XAML:
<custom:Knob ValueChanged="knob1_ValueChanged" />

Codebehind:
knob2.ValueChanged += Knob1_ValueChanged;
```
where the event handler is
```
private void Knob1_ValueChanged(object? sender, double e) {
    txtValue.Text = $"{e:F3}";
}
```
Alternatively a lambda may be used
```
knob2.ValueChanged += (o, e) => {
   txtValue.Text = $"{e:F3}";
};
```

## Appearance
### Marker Style
There are 4 marker styles controlled by the `MarkerStyle` property: `Line1`, `Line2`, `Line3` and `Dot`.
```
<custom:Knob.MarkerStyle>Line3</custom:Knob.MarkerStyle>
```
![Marker Style](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/aa3302c2e3dabab259680f2b4b0ed5fa02281b4b/ReadmeImages/MarkerType.png)


### MarkerWidth
``` 
<custom:Knob.MarkerWidth>3</custom:Knob.MarkerWidth>
```
Note: `MarkerWidth` also affects outline of dot where `MarkerStyle` is `Dot`

![Marker Width](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/eb6dd8454bce8584c91d5cadc40c0a3d6ecf0479/ReadmeImages/MarkerWidth.png)


### MarkerColor
``` 
<custom:Knob.MarkerColor>Green</custom:Knob.MarkerColor>
```
Note: `MarkerColor` also affects outline of dot where `MarkerStyle` is `Dot`

![Marker Color](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/eb6dd8454bce8584c91d5cadc40c0a3d6ecf0479/ReadmeImages/MarkerColor.png)


### DotFillColour
For `Dot` marker, a `DotFillColour` can be specified to distinguish the dot from the brush used to paint the body of the knob. Dot outline is rendered using `MarkerWidth` and `MarkerColor` settings.
```
<custom:Knob.MarkerColor>Red</custom:Knob.MarkerColor>
<custom:Knob.DotFillColor>Yellow</custom:Knob.DotFillColor>
```
![Dot Fill Colour](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/b554e97f70df5df92c9c94976f8f9496923f38dd/ReadmeImages/MarkerDotFillColor.png)


### OutlineWidth
Determines the width of the knob outline. Note: The Marker width is controlled by a separate `MarkerWidth` property.
```
<custom:Knob.OutlineWidth>3</custom:Knob.OutlineWidth>
```
![Outline Width](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/3f159ad96d8b16df1d372d5d87574c417925b021/ReadmeImages/OutlineWidth.png)

### OutlineColor
Determines the colour of the knob outline. Note: The Marker colour is controlled by a separate `MarkerColor` property.
```
<custom:Knob.OutlineColor>Magenta</custom:Knob.OutlineColor>
```
![Outline Colour](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/3f159ad96d8b16df1d372d5d87574c417925b021/ReadmeImages/OutlineColor.png)

### Images
Rather than passing filenames and paths to the knob control, we've used WPF resources instead. Firstly, it means you don't have physical files as part of your deployment, and secondly you don't have to worry about paths if you've set up the resource correctly. You will see later that images are used for two separate purposes
1. As a Knob background when using `ImageBrush`
2. As marker icons when using image Annotations.
   
Any image you intend to use, add to the <Windows.Resource> section of your XAML, and give them a meaningful key by which to access them later:
```
<Window.Resources>
    <!-- ImageBrush Images -->
    <BitmapImage x:Key="BrushedAluminiumImage" UriSource="/KnobImages/BrushedAluminium.png" />
    <!-- Annotation Images -->
    <BitmapImage x:Key="Icon_Saw" UriSource="/KnobImages/Saw.png" />
    <BitmapImage x:Key="Icon_Sine" UriSource="/KnobImages/Sine.png" />
    <BitmapImage x:Key="Icon_Square" UriSource="/KnobImages/Square.png" />
    <BitmapImage x:Key="Icon_SuperSaw" UriSource="/KnobImages/SuperSaw.png" />
    <BitmapImage x:Key="Icon_Triangle" UriSource="/KnobImages/Triangle.png" />
</Window.Resources>
```
*Note* The images must be somewhere within your project hierarchy, and the `UriSource` should be set to reflect that. A subfolder under the WPF project such as _/KnobImages_ above is ideal. Also ensure that the images are added to your project. For each image, set its _Build Action_ to _Resource_.

### FillBrush
A `Brush` for filling in the body of the knob. `FillBrush` is of type `abstract class Brush` which means that it can be set to any `Brush` type that derives from this - these being `SolidBrush`, `LinearGradientBrush`, `RadialGradientBrush`, `ImageBrush`, `DrawingBrush`, `VisualBrush`, `TileBrush` and `BitmapCacheBrush` - we won't discuss the final 3.

#### SolidBrush
For this example, we've gone for a darker background with the `Canvas` `Background` set to `Gray`, and the Knob and Marker outlines set to `White`. This illustrates that the Knob background takes on the background of the `Canvas` automatically.
```
<custom:Knob.MarkerColor>White</custom:Knob.MarkerColor>
<custom:Knob.OutlineColor>White</custom:Knob.OutlineColor>
<custom:Knob.FillBrush>
    <SolidColorBrush Color="Navy" />
</custom:Knob.FillBrush>
```
![Solid Brush](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/8880e3aebc3eab8d01eb0555d1b01e54214d1d41/ReadmeImages/FillColorSolid.png)


#### LinearGradientBrush
With a linear gradient brush, you specify a `StartPoint` and an `EndPoint`, the  angle between which determines the gradient direction, and the distance apart specifies the start and end of the gradient transition. Two or more `GradientStop` elements are then specified to control colours, and how fast the transistion between them occur. The first example shows a vertical gradient, the remainder angled. See following for brief article on Linear Gradients https://www.c-sharpcorner.com/uploadfile/mahesh/wpf-lineargradientbrush/
```
<custom:Knob.FillBrush>
    <LinearGradientBrush StartPoint="0,0" EndPoint="0,1" >
        <GradientStop Color="Blue" Offset="0" />
        <GradientStop Color="Red" Offset="1.0" />
    </LinearGradientBrush>
</custom:Knob.FillBrush>
```
![Linear Gradient Brush](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/0c3b8506681c1872ddbac7a8430bc560ac317bdb/ReadmeImages/FillColorLinear.png)

#### RadialGradientBrush
Here, you specify a `Center` point and x and y radii- `RadiusX` and `RadiusY` respectively. These specify the start and end of colour transistions. There is also a `GradientOrigin` which is a sort of focal point for the gradient. By default it is the same as `Center` but may be set separately. Again two or more `GradientStop` elements specify colours and how fast transition between the occurs. See following for brief article on Radial Gradients https://www.c-sharpcorner.com/uploadfile/mahesh/wpf-radialgradientbrush/
```
<custom:Knob.FillBrush>
    <RadialGradientBrush GradientOrigin="0.5,0.5" Center="0.5,0.5" RadiusX="0.5" RadiusY="0.5">
        <RadialGradientBrush.GradientStops>
            <GradientStop Color="Blue" Offset="0" />
            <GradientStop Color="Red" Offset="1.0" />                   
        </RadialGradientBrush.GradientStops>
    </RadialGradientBrush>
<\custom:Knob.FillBrush>
```        
![Radial Gradient Brush](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/a01c23c8ef635b18d23bc126ccb9a39a8019f273/ReadmeImages/FillColorRadial.png)


#### ImageBrush
Here you can specify an image file resource as a `Brush`. This is potentially useful for simulating knob materials. Ensure your image has been setup as a resource with a key.
```
<Window.Resources>
    <!-- ImageBrush Images -->
    <BitmapImage x:Key="BrushedAluminiumImage" UriSource="/KnobImages/BrushedAluminium.png" />
</Window.Resources>


<custom:Knob.FillBrush>
    <ImageBrush ImageSource="{StaticResource BrushedAluminiumImage}" />
</custom:Knob.FillBrush>
```
![Image Brush](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/80aca0a34b57d4e8ed28462754f42d893472c722/ReadmeImages/FillImagel.png)

One disadvantage is that where an image background has an obvious orientation, it does not rotate as the knob rotates. It might be worth adding a `RotateBackground` property in the future?

#### DrawingBrush
A `DrawingBrush` extends the functionality of an `ImageBrush`. As well as images, it can contain text, shapes and media. Shapes and text can be drawn directly using XAML or codebehind. Again there is the disadvantage that the background does not rotate with the knob.
```
<custom:Knob.FillBrush>
    <DrawingBrush>
        <DrawingBrush.Drawing>
            <GeometryDrawing Brush="Yellow">
                <GeometryDrawing.Geometry>
                    <GeometryGroup>
                        <RectangleGeometry Rect="50,25,25,25" />
                        <RectangleGeometry Rect="25,50,25,25" />
                    </GeometryGroup>
                </GeometryDrawing.Geometry>
                <GeometryDrawing.Pen>
                    <Pen Thickness="5">
                        <Pen.Brush>
                            <LinearGradientBrush>
                                <GradientStop Offset="0.0" Color="Blue" />
                                <GradientStop Offset="1.0" Color="Black" />
                            </LinearGradientBrush>
                        </Pen.Brush>
                    </Pen>
                </GeometryDrawing.Pen>
            </GeometryDrawing>
        </DrawingBrush.Drawing>
    </DrawingBrush>
</custom:
```
![Drawing Brush](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/5ddd436993ffa25df4b7832096c2646c3f9921a0/ReadmeImages/FillDrawingBrush.png) 



## Snapping
By default, the knob moves smoothly in response to a MouseDrag through the `FullSweepAngle`. However, we can get it to snap to a fixed number of positions by setting the `NumPositions` property. When set, this will split the `FullSweepAngle` into `NumPositions` - 1 sectors. When dragged with a mouse, the knob `Value` will snap to these sector boundaries. In this example where `ValueMin` is -1, `ValueMax` is 3, and there are 4 `NumPositions`, with a `FullSweepAngle` of 270°, there are 3 sectors, each occupying 90°. Value increments are (1 -(-3) / 3 = 4 / 3 giving 1.333 increments. These will therefore follow the sequence  -1.000, 0.333, 1.667, 3.000
```
 <custom:Knob.NumPositions>4</custom:Knob.NumPositions>
 <custom:Knob.Value>0</custom:Knob.Value>
 <custom:Knob.ValueMin>-1</custom:Knob.ValueMin>
 <custom:Knob.ValueMax>3</custom:Knob.ValueMax>
 <custom:Knob.FillBrush>
     <SolidColorBrush Color="">#103070</SolidColorBrush>
 </custom:Knob.FillBrush>
```
![Snapping using NumPositions](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/48155599a8dcf39e34db4a4fe09328d2da74f6f6/ReadmeImages/Snapping.png)

## Tick Marks
Tick Marks can optionally be shown to indicate the angle the knob has rotated through. By default, 11 ticks with 10 equally sized sectors are displayed. Ticks are enabled using the `ShowTicks` property, and they can be styled using the `TickColor` and `TickWidth`. When Tick Marks are enabled, the knob will be scaled down slightly to provide room for the ticks to be displayed.
```
<custom:Knob.ShowTicks>true</custom:Knob.ShowTicks>
<custom:Knob.TickColor>silver</custom:Knob.TickColor>
<custom:Knob.TickWidth>2</custom:Knob.TickWidth>
```
![Tick Marks](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/5d5c527034d7f81b926ac19828e5b5b03ca4a30b/ReadmeImages/Ticks.png)

If snapping is enabled due to `NumPositions` property being set to a value, the number of ticks and their locations correspond to the snap angles. 
```
<custom:Knob.FullSweepAngle>180</custom:Knob.FullSweepAngle>
<custom:Knob.NumPositions>6</custom:Knob.NumPositions>
<custom:Knob.ShowTicks>true</custom:Knob.ShowTicks>
<custom:Knob.TickColor>black</custom:Knob.TickColor>
<custom:Knob.TickWidth>1</custom:Knob.TickWidth>
```
![Wick Marks with Snapping](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/832068f70f4f3752ca0a235fed60eba45243001d/ReadmeImages/TicksSnapped.png)

### Manually override Tick Positions
By default, the Tick Positions are set automatically using a StartRadius and an EndRadius relative to the Knob Width. To override, these, set the manual radii on the `ManualTickRadiusStart` and `ManualTickRadiusEnd` `double?` properties.
```
<custom:Knob.ShowTicks>true</custom:Knob.ShowTicks>
<custom:Knob.ManualTickRadiusStart>1.3</custom:Knob.ManualTickRadiusStart>
<custom:Knob.ManualTickRadiusEnd>1.5</custom:Knob.ManualTickRadiusEnd>
```

## Annotations
Each Tick Mark location can be annotated, even when `ShowTicks` is `false`. Either a text label or an icon can be displayed at each Tick Position. Text labels can be manually specified, or they can be automatic where they will take the value 0 up to the number of Tick Positions. Annotations can be selected by setting the `AnnotationMode` property, valid values being `None`, `LabelsAuto`, `Labels` and `Images`. For `LabelsAuto` and `Labels` modes, the text size of the labels will automatically increase if the knob is made bigger. 

### Automatic Labels
`AnnotationMode` is set to `LabelsAuto`. If snapping is off, then the digits 0 to 10 will be displayed along the full sweep angle. If `ShowTicks` is `true`, the positions will correspond to those of the ticks. The last 2 examples show snapping by setting `NumPositions`. In this case the number and location of labels displayed correspond with the snap positions.
```
<custom:Knob.FullSweepAngle>220</custom:Knob.FullSweepAngle>
<custom:Knob.ShowTicks>true</custom:Knob.ShowTicks>
<custom:Knob.TickColor>black</custom:Knob.TickColor>
<custom:Knob.TickWidth>1</custom:Knob.TickWidth>
<custom:Knob.AnnotationColor>black</custom:Knob.AnnotationColor>
<custom:Knob.AnnotationMode>LabelsAuto</custom:Knob.AnnotationMode>
```
![Automatic Labels](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/2cc53fa84ed078f7b7863ae4be67e65aa1e00877/ReadmeImages/AnnotationsAuto.png)

### Labels
`AnnotationMode` is set to `Labels`. In this setting labels can be specified manually using the `Annotations` property. `Annotations` is of type `List<string>` so a separate annotation can be set for each Tick Position. If there are more Tick Positions than annotations provided, the remainder of the annotations are displayed as in `LabelsAuto` mode. This mode is meant mainly the knob is in snapping mode, and usually `NumPositions` is set quite low.
```
<custom:Knob.NumPositions>5</custom:Knob.NumPositions>
<custom:Knob.FullSweepAngle>180</custom:Knob.FullSweepAngle>
<custom:Knob.AnnotationColor>black</custom:Knob.AnnotationColor>
<custom:Knob.AnnotationMode>Labels</custom:Knob.AnnotationMode>
<custom:Knob.Annotations>
   <sys:String>-2</sys:String>
   <sys:String>-1</sys:String>
   <sys:String>0</sys:String>
   <sys:String>+1</sys:String>
   <sys:String>+2</sys:String>
</custom:Knob.Annotations>
```
![Label Annotations](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/54ac4981b8765df77da2975dacf21525f93b54a7/ReadmeImages/AnnotationsLabel.png)

### Images
`AnnotationMode` is set to `Images`. In this setting, rather than displaying text labels, image icons are displayed at the Tick Positions. Again, this is meant mainly for when the knob is in snapping mode and a few number of options have been specified by the `NumPositions` property.

We are passing images in as resource again, but in this case we must pass in a string array of Resource Keys.

```
<Window.Resources>
    <!-- Annotation Images -->
    <BitmapImage x:Key="Icon_Saw" UriSource="/KnobImages/Saw.png" />
    <BitmapImage x:Key="Icon_Sine" UriSource="/KnobImages/Sine.png" />
    <BitmapImage x:Key="Icon_Square" UriSource="/KnobImages/Square.png" />
    <BitmapImage x:Key="Icon_SuperSaw" UriSource="/KnobImages/SuperSaw.png" />
    <BitmapImage x:Key="Icon_Triangle" UriSource="/KnobImages/Triangle.png" />
</Window.Resources>


<custom:Knob.ShowTicks>false</custom:Knob.ShowTicks>
<custom:Knob.AnnotationMode>Images</custom:Knob.AnnotationMode>
<custom:Knob.AnnotationImageResourceKeys>
    <sys:String>Icon_Saw</sys:String>
    <sys:String>Icon_Sine</sys:String>
    <sys:String>Icon_Square</sys:String>
    <sys:String>Icon_SuperSaw</sys:String>
    <sys:String>Icon_Triangle</sys:String>
</custom:Knob.AnnotationImageResourceKeys>
```
![Image Annotations](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/8ccdd16403c707d7fdc38e53c627b03208974cb7/ReadmeImages/AnnotationImages.png)

If fewer images have been specified than of `NumPositions`, then nothing is displayed for those Tick Positions lacking an image. `ShowTick` is probably best set to `false` when displaying images. **Note:** Image positioning could do with a bit more work. 


### Styling Image Annotations
Any white pixels (R:255, G:255, B:255) in the provided image will be converted to a pixel of `AnnotationColor`. Alpha channel is maintained for transparancy. Any non-white pixeld will be unchanged. So, to style an annotation image, design it with a transparent background layer, apply graphic in white on another layer, then merge layers and save. If you don't want it styled just use any pixel that isn't white.
```
<custom:Knob.AnnotationMode>Images</custom:Knob.AnnotationMode>
<custom:Knob.AnnotationColor>Orange</custom:Knob.AnnotationColor>
<custom:Knob.AnnotationImageResourceKeys>
    <sys:String>Icon_Saw</sys:String>
    <sys:String>Icon_Sine</sys:String>
    <sys:String>Icon_Square</sys:String>
    <sys:String>Icon_SuperSaw</sys:String>
    <sys:String>Icon_Triangle</sys:String>
</custom:Knob.AnnotationImageResourceKeys>
```

![Styling Styling Image Annotations](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/f8e45c5de7e724ea4475cf2005452d42ab377129/ReadmeImages/AnnotationColor.png)


### Manually override Annotation position and fontsize
By default, the Annotation positions are set automatically using a radius relative to the Knob Width. To override this, set the radius using the `ManualAnnotationRadius` `double?` property. The FontSize is also automatically set to scale with Knob Width. To override, set the `ManualAnnotationFontSize` `double?` property.
```
<custom:Knob.AnnotationMode>Labels</custom:Knob.AnnotationMode>
<custom:Knob.ManualAnnotationRadius>1.9</custom:Knob.ManualAnnotationRadius>
<custom:Knob.ManualAnnotationFontSize>15</custom:Knob.ManualAnnotationFontSize>
```

## Caption
A caption can be displayed below the knob using the `Caption`, `CaptionBold` and `CaptionColor` properties.
```
<custom:Knob.ShowTicks>true</custom:Knob.ShowTicks>
<custom:Knob.AnnotationMode>Labels</custom:Knob.AnnotationMode>
<custom:Knob.Annotations>
    <sys:String>-2</sys:String>
    <sys:String>-1</sys:String>
    <sys:String>0</sys:String>
    <sys:String>+1</sys:String>
    <sys:String>+2</sys:String>
/custom:Knob.Annotations>
<custom:Knob.Caption>Frequency</custom:Knob.Caption>
<custom:Knob.CaptionBold>true</custom:Knob.CaptionBold>
<custom:Knob.CaptionColor>Green</custom:Knob.CaptionColor>
```
![Caption](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/42fe8be5916bf8e2ef214be3e7f935b9c278720d/ReadmeImages/Caption.png)

### Manually override Caption Radius and fontsize
The caption is automatically drawn at a specific radius from the knob centre. This can be manually set where `ManualCaptionRadius` is relative to knob width. The caption fontsize can also be overriden by `ManualCaptionFontSize`.
```
<custom:Knob.ManualCaptionRadius>1.9</custom:Knob.ManualAnnotationRadius>
<custom:Knob.ManualCaptionFontSize>15</custom:Knob.ManualAnnotationFontSize>
```

## Nullable Properties
Several properties are nullable - namelly, `ManualAnnotationFontSize`, `ManualAnnotationRadius`,  `ManualCaptionRadius`, `ManualCaptionFontSize`, `ManualTickRadiusEnd`, `ManualTickRadiusStart`, `NumPositions`
In XAML, properties can be expressed either as attributes or as sub-elements. e.g. `NumPositions`, which is an `int?` can be set using either of these methods:
```
<custom:Knob NumPositions="5"></custom:Knob>

<custom:Knob>
    <custom:Knob.NumPositions>5</custom:Knob.NumPositions>
</custom:Knob>
```
If you change the 5 to any other valid non-null value, the setter of `NumPositions` gets called and the value will change. But what if we want to set it back to null. There are two ways. The simplest which works for both attribute and sub-element notation is to simply remove the attribute or sub-element altogether.
```
<custom:Knob></custom:Knob>
<custom:Knob>

</custom:Knob>
```
The second method only works for attribute notation - the {x:Null} placeholder can be used. This will pass a null to the property's setter. This trick does not work with the sub-element notation.
```
<custom:Knob NumPositions="{x:Null}"></custom:Knob>
```

## Using Themes
Instead of applying appearance properties to a knob control one by one in xaml or codebehind, you can so so en-masse using a Themes file. If you have multiple themese files, they can be swapped over using a single line of code. The knob styling is placed in a themes file - in this case Prophet.xaml
```
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:custom="clr-namespace:SynthCustomControls;assembly=SynthCustomControls">

    <!-- Apply any window styling -->
    <Style TargetType="Canvas">
        <Setter Property="Background" Value="navy"/>
    </Style>

    <!-- Define a SolidColorBrush for the FillBrush -->
    <RadialGradientBrush x:Key="KnobFillBrush" Center=".62,.62">
        <GradientStop Color="silver" Offset="0" />
        <GradientStop Color="Black" Offset="1.0" />
    </RadialGradientBrush>
    
    <!-- Define the custom control style -->
    <Style TargetType="custom:Knob">
        <!-- Assign a null value to ManualCaptionFontSize using x:Null -->
        <Setter Property="OutlineWidth" Value="2"/>
        <Setter Property="OutlineColor" Value="Yellow"/>
        <Setter Property="MarkerWidth" Value="3"/>
        <Setter Property="MarkerColor" Value="Yellow"/>
        <Setter Property="DotFillColor" Value="White"/>
        <Setter Property="FillBrush" Value="{StaticResource KnobFillBrush}"/>
    </Style>
</ResourceDictionary>
```

In particular notice we can style a `RadialGradientBrush` that is then referenced in the Knob control theming using `<Setter Property="FillBrush" Value="{StaticResource KnobFillBrush}"/>`

To apply a theme, the Theme is included in the `<Windows.Resources>` section of the main xaml form. The theme can be changed by changing the filename in `<ResourceDictionary Source="/Themes/Prophet.xaml"/>`

```
<Window x:Class="WpfUi.TestWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:sys="clr-namespace:System;assembly=mscorlib"
        xmlns:custom="clr-namespace:SynthCustomControls;assembly=SynthCustomControls"
        Title="Synth 5" Height="188" Width="356" >

    <Window.Resources>
        <ResourceDictionary>
            <!-- Merge the Prophet.xaml theme -->
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="/Themes/Prophet.xaml"/>
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Window.Resources>

    <Canvas Margin="0,0,-27,-39">
        <custom:Knob Canvas.Top="30" Canvas.Left="50" Name="knob1" Height="100"  ValueMin="0" ValueMax="5"></custom:Knob>
        <custom:Knob Canvas.Top="30" Canvas.Left="200" Name="knob2" Height="100" ValueMin="0" ValueMax="5"></custom:Knob>
    </Canvas>
</Window>
```
![Themes](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/0bafe8bab8646d5b9ac9f4d53436c5a7e86b0a00/ReadmeImages/Themes.png)


## Notes
The `Height` property  of the knob tracks the `Width` property, so the control outline will always be a square, and the knob outline will always be a circle.

There are a lot of thing being drawn. Therefore in code, there are two separate methods used for drawing: `DrawKnob()` which displays outline, ticks, labels, caption etc. and `DrawMarker()` which just displays the marker, which is a single straight line (or circle). When the knob is being rotated by having the mouse drag over it, it's only the marker position that needs to change, so only `DrawMarker()` is called. However, the rest of the knob will be lost when doing this. Therefore, whenever DrawKnob() is called, before returning it copies the drawing context into a cache. The DrawMarker() can then use this cache for restoring the knob background before drawing the marker. As this is essentially a memory copy operation, it is faster than performing all the mathematical operations for actually drawing the background each time. 

When a property affecting the knob display is changed, `DrawKnob()` <u>is</u> called, and the entire knob is redrawn. However, this normally happens much less frequently than rotating the knob with a mouse drag.

Tick Marks and annotation are positioned/sized automatically. Thhis can be overridden by setting the `ManualTickRadiusStart`, `ManualTickRadiusStart`, `ManualAnnotationRadius` and `ManualAnnotationFontSize` properties.

## Full Property List
| Property/XAML attribute | Data Type      |   Options    |       |       |       | Notes |
| ------ | ------| ------| ------| ------| ------| ------|
| `AnnotationImageResourceKeys` | `List<string>` |       |       |       |       | Image Resources must have been setup for all images in <Window.Resources> |
| `AnnotationMode`       | `AnnotationType`      | `None`      | `LabelsAuto`      | `Labels`      | `Images`      | Knob size decreases to accomodate annotations |
| `Annotations`       | `List<string>`      |       |       |       |       | Labels where `AnnotationMode` = `LabelsAuto` |
| `AnnotationColor`       | `Color`      |       |       |       |       | Text colour or image colour. Image must be white on an alpha channel  |
| `Caption`       | `string?`      |       |       |       |       | |
| `CaptionBold`       | `bool`      |       |       |       |       | |
| `CaptionColor`      | `Color`      |       |       |       |       | |
| `DotFillColor`       | `Color`      |       |       |       |       | |
| `FillBrush`       | `Brush`      |       |       |       |       | e.g. `SolidBrush`,  `LinearGradientBrush`, `RadialGradientBrush`, `ImageBrush` etc. |
| `FullSweepAngle`       | `double`      |       |       |       |       | Sweep angle is symmetrical around positive vertical axis |
| `ManualAnnotationFontSize` | `double?`      |       |       |       |       | Override auto font size |
| `ManualAnnotationRadius` | `double?`      |       |       |       |       | Override auto label position |
| `ManualCaptionRadius` | `double?`       |       |       |       |       | Override Caption position |
| `ManualCaptionFontsize` | `double?`       |       |       |       |       | Override auto font size |
| `ManualTickRadiusEnd` | `double?`      |       |       |       |       | Override auto tick end point |
| `ManualTickRadiusStart` | `double?`      |       |       |       |       |  Override auto tick start point |
| `MarkerColor`       | `Color`      |       |       |       |       | |
| `MarkerStyle`       | `MarkerStyleType`      | `Line1`      | `Line2`      | `Line3`      | `Dot`      | |
| `MarkerWidth`       | `int`      |       |       |       |       | |
| `NumPositions`       | `int?`      |       |       |       |       | When set, knob snaps through fixed positions. Number of sectors will = `NumPositions` - 1 |
| `OutlineColor`       | `Color`      |       |       |       |       | Affects Knob and `Dot` marker outlines |
| `OutlineWidth`       | `int`      |       |       |       |       | Affects Knob and `Dot` marker outlines |
| `ShowTicks`       | `bool`      |       |       |       |       | Knob size decreases to accomodate ticks |
| `TickWidth`       | `int`      |       |       |       |       | |
| `Value`       | `double`      |       |       |       |       | |
| `ValueMax`       | `double`      |       |       |       |       | |
| `ValueMin`       | `double`      |       |       |       |       | |
| `Width`       | `int`      |       |       |       |       | `Height` tracks `Width` |




