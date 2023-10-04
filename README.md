# SynthCustomControls
WPF Controls for Virtual Analog Synth Project - primarily a flexible WPF Knob control

## Basic Usage

Add a refrence to either SynthCustomControls.dll or SynthCustomControls.csproj

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
MinValue is set to 0, MaxValue is set to 1, FullSweepAngle is set to 270°, so dragging the knob marker with the mouse gives continuous variation of Value:

![Basic Use](https://raw.githubusercontent.com/BertyBasset/SynthCustomControls/f65f9baa8b5cdbb6aa57f0c10067136f298932e8/ReadmeImages/BasicUse.png)

## Appearance
### Marker Style
There are 4 marker styles controlled by the MarkerStyle property e.g.
```
<custom:Knob.MarkerStyle>Line3</custom:Knob.MarkerStyle>
```



#### Dot Fill Colour
For a dot marker, a Fill Colour can be specified

### Fill Brush

### Outline Colour

### Outline Width

### Marker Width


## Snapping


## Tick Marks


## Annotations
Annotation Mode
### Automatic Labels

### Labels

### Images


## Caption

### CaptionBold
