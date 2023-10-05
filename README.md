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

`MinValue` and `MaxValue` can be set to any valid positive or negative double value as long as `MaxValue` > `MinValue`. `FullSweepAngle` can be set to any angle between 20° and 340°. The `Value` property will reflect the current knob marker value taking the previous properties into account. e.g. `MinValue` = -1, `MaxValue` = +3, `FullSweepAngle` = 180°
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
Alternatively a lamda may be used
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
For `Dot` marker, a Fill Colour can be specified to distinguish the dot from the brush used to paint the body of the knob.
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


### FillBrush
A Brush for filling in the body of the knob. `FillBrush` is of type `abstract class Brush` which means that it can be set to any `Brush` type that derives from this - these being `SolidBrush`, `LinearGradientBrush`, `RadialGradientBrush`, `ImageBrush`, `DrawingBrush`, `VisualBrush`. `TileBrush` and `BitmapCacheBrush` - we won't discuss the final 3.

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
Here you can specify an image file, or other image resource as a `Brush`. This is potentially useful for simulating knob materials.
```
<custom:Knob.FillBrush>
    <ImageBrush ImageSource="images/ManOnBike.png"></ImageBrush>
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
By default, the knob moves smoothly in response to a MouseDrag through the `FullSweepAngle`. However, we can get it to snap to a fixed number of positions by setting the `NumPositions` property. When set, this will split the `FullSweepAngle` into `NumPositions` - 1 sectors. When dragged with a mouse, the knob `Value` will snap to these sector boundaries. In this example where `ValueMin` is -1, `ValueMax` is 3, and there are 4 `NumPositions`, with a `FullSweepAngle` of 270°, there are 3 sectors, each occupying 90°. Value increments are (1 -(-3) / 3 = 4 / 3 giving 1.333 increments. These will therefore follow the sequence  -1, 0.333, 1.667, 3.
```
 <custom:Knob.NumPositions>4</custom:Knob.NumPositions>
 <custom:Knob.Value>0</custom:Knob.Value>
 <custom:Knob.ValueMin>-1</custom:Knob.ValueMin>
 <custom:Knob.ValueMax>3</custom:Knob.ValueMax>
 <custom:Knob.FillBrush>
     <SolidColorBrush Color="">#103070</SolidColorBrush>
 </custom:Knob.FillBrush>
```

## Tick Marks


## Annotations
Annotation Mode
### Automatic Labels

### Labels

### Images


## Caption

### CaptionBold
