# SynthCustomControls
WPF Controls for Virtual Analog Synth Project - primarily a flexible WPF Knob control

## Basic Usage

Add a reference to either SynthCustomControls.dll or SynthCustomControls.csproj

```
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
MinValue is set to 0, MaxValue is set to 1, FullSweepAngle is set to 270°, so dragging the knob marker with the mouse gives continuous variation of Value (note textbox is separate control):

![Basic Use](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/f65f9baa8b5cdbb6aa57f0c10067136f298932e8/ReadmeImages/BasicUse.png)

## Appearance
### Marker Style
There are 4 marker styles controlled by the MarkerStyle property e.g.
```
<custom:Knob.MarkerStyle>Line3</custom:Knob.MarkerStyle>
```
![Marker Style](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/aa3302c2e3dabab259680f2b4b0ed5fa02281b4b/ReadmeImages/MarkerType.png)

### Marker Width
``` 
<custom:Knob.MarkerWidth>3</custom:Knob.MarkerWidth>
```
![Marker Width](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/eb6dd8454bce8584c91d5cadc40c0a3d6ecf0479/ReadmeImages/MarkerWidth.png)

### Marker Color
``` 
<custom:Knob.MarkerColor>Green</custom:Knob.MarkerWidth>
```
![Marker Color](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/eb6dd8454bce8584c91d5cadc40c0a3d6ecf0479/ReadmeImages/MarkerColor.png)

#### Dot Fill Colour
For a dot marker, a Fill Colour can be specified to distinguish the dot from the brush used to paint the body of the knob.
<sample>
<image>


### Fill Brush
A solid brush, a linear gradient, or a radial gradient brush can be used to paint the body of the knob.
<samples>     a range of solid and gradient brushes
<images>



### Outline Colour

### Outline Width





## Snapping


## Tick Marks


## Annotations
Annotation Mode
### Automatic Labels

### Labels

### Images


## Caption

### CaptionBold
