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
**MinValue** is set to 0, **MaxValue** is set to 1, **FullSweepAngle** is set to 270°, so dragging the knob marker with the mouse gives continuous variation of **Value** (note textbox is separate control):

![Basic Use](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/f65f9baa8b5cdbb6aa57f0c10067136f298932e8/ReadmeImages/BasicUse.png)

**MinValue and **MaxValue** can be set to any valid positive or negative double value as long as **MaxValue** > **MinValue**. **FullSweepAngle** can be set to any angle between 20° and 340°. The **Value** property will reflect the current knob marker value taking the previous properties into account. e.g. **MinValue** = -1, **MaxValue** = +3, **FullSweepAngle** = 180°
<TO DO IMAGE>

### ValueChanged Event
The Knob control has a single event that is fired whenever the **Value** property changes. It passes an event argument of type double representing the **Value** property. An event handler can be specified in the XAML, or in codebehind:
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
There are 4 marker styles controlled by the **MarkerStyle** property e.g.
```
<custom:Knob.MarkerStyle>Line3</custom:Knob.MarkerStyle>
```
![Marker Style](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/aa3302c2e3dabab259680f2b4b0ed5fa02281b4b/ReadmeImages/MarkerType.png)


### MarkerWidth
``` 
<custom:Knob.MarkerWidth>3</custom:Knob.MarkerWidth>
```
Note: `**MarkerWidth**` also affects outline of dot where `**MarkerStyle**` is Dot

![Marker Width](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/eb6dd8454bce8584c91d5cadc40c0a3d6ecf0479/ReadmeImages/MarkerWidth.png)


### MarkerColor
``` 
<custom:Knob.MarkerColor>Green</custom:Knob.MarkerColor>
```
Note: **MarkerColor** also affects outline of dot where **MarkerStyle** is Dot

![Marker Color](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/eb6dd8454bce8584c91d5cadc40c0a3d6ecf0479/ReadmeImages/MarkerColor.png)


#### DotFillColour
For Dot marker, a Fill Colour can be specified to distinguish the dot from the brush used to paint the body of the knob.
```
<custom:Knob.MarkerColor>Red</custom:Knob.MarkerColor>
<custom:Knob.DotFillColor>Yellow</custom:Knob.DotFillColor>
```
![Dot Fill Colour](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/b554e97f70df5df92c9c94976f8f9496923f38dd/ReadmeImages/MarkerDotFillColor.png)


### OutlineWidth
Determines the width of the knob outline. Note: The Marker width is controlled by a separate **MarkerWidth** property.
```
<custom:Knob.OutlineWidth>3</custom:Knob.OutlineWidth>
```
![Outline Width](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/3f159ad96d8b16df1d372d5d87574c417925b021/ReadmeImages/OutlineWidth.png)

### OutlineColor
Determines the colour of the knob outline. Note: The Marker colour is controlled by a separate **MarkerColor** property.
```
<custom:Knob.OutlineColor>Magenta</custom:Knob.OutlineColor>
```
![Outline Colour](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/3f159ad96d8b16df1d372d5d87574c417925b021/ReadmeImages/OutlineColor.png)


### FillBrush
A solid brush, a linear gradient, or a radial gradient brush can be used to paint the body of the knob.
<samples>     a range of solid and gradient brushes
<images>





## Snapping
By default, the knob moves smoothly in response to a MouseDrag through the **FullSweepAmgle**.

## Tick Marks


## Annotations
Annotation Mode
### Automatic Labels

### Labels

### Images


## Caption

### CaptionBold
