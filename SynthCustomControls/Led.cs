using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static SynthCustomControls.Knob;

namespace SynthCustomControls;

public class Led : Control {
    public event EventHandler<bool>? LedChanged;

    public enum ShapeType { 
        Circle,
        Ellipse,
        Square,
        Rectangle
    }

    public static readonly DependencyProperty ShapeProperty =
    DependencyProperty.Register(
        "Shape",
        typeof(ShapeType),
        typeof(Led),
        new PropertyMetadata(ShapeType.Circle));

    public ShapeType? Shape {
        get { return (ShapeType)GetValue(ShapeProperty); }
        set {
            SetValue(ShapeProperty, value);
            InvalidateVisual();
        }
    }



    bool _LedOn;
    public bool LedOn {
        get { return _LedOn; }
        set {
            _LedOn = value;
            InvalidateVisual();
        }
    }

    bool _ToggleOnClick;
    public bool ToggleOnClick {
        get { return _ToggleOnClick; }
        set {
            _ToggleOnClick = value;
            InvalidateVisual();
        }
    }


    public static readonly DependencyProperty OutlineColorProperty =
      DependencyProperty.Register(
          "OutlineColor",
          typeof(Color),
          typeof(Led),
          new PropertyMetadata(Colors.Black));

    public Color OutlineColor {
        get { return (Color)GetValue(OutlineColorProperty); }
        set {
            SetValue(OutlineColorProperty, value);
            InvalidateVisual();
        }
    }

    public static readonly DependencyProperty OutlineWidthProperty =
      DependencyProperty.Register(
          "OutlineWidth",
          typeof(int),
          typeof(Led),
          new PropertyMetadata(1));

    public int OutlineWidth {
        get { return (int)GetValue(OutlineWidthProperty); }
        set {
            SetValue(OutlineWidthProperty, value);
            InvalidateVisual();
        }
    }

    public static readonly DependencyProperty FillBrushOnProperty =
    DependencyProperty.Register(
         "FillBrushOn",
         typeof(Brush),
         typeof(Led),
         new PropertyMetadata(null));

    public Brush? FillBrushOn {
        get { return (Brush?)GetValue(FillBrushOnProperty); }
        set {
            SetValue(FillBrushOnProperty, value);
            InvalidateVisual();
        }
    }

    public static readonly DependencyProperty FillBrushOffProperty =
    DependencyProperty.Register(
         "FillBrushOff",
         typeof(Brush),
         typeof(Led),
         new PropertyMetadata(null));

    public Brush? FillBrushOff {
        get { return (Brush?)GetValue(FillBrushOffProperty); }
        set {
            SetValue(FillBrushOffProperty, value);
            InvalidateVisual();
        }
    }


    public Led() {
        InvalidateVisual();

        this.MouseUp += (o, e) => {if (_ToggleOnClick) LedOn = !LedOn; LedChanged?.Invoke(this, LedOn); };
        
    }

    protected override void OnRender(DrawingContext dc) {
        base.OnRender(dc);

        if (Shape == ShapeType.Circle || Shape == ShapeType.Square)
            Height = Width;

        Point origin = new (ActualWidth/2.0, ActualHeight/2.0);


        var pen = new Pen(new SolidColorBrush() { Color = OutlineColor }, OutlineWidth);
        if(Shape == ShapeType.Circle || Shape == ShapeType.Ellipse) {
            if (_LedOn)
                dc.DrawEllipse(FillBrushOn, pen, origin, Width / 2, Height / 2); 
            else
                dc.DrawEllipse(FillBrushOff, pen, origin, Width / 2, Height / 2);
        } else {
            if (_LedOn)
                dc.DrawRectangle(FillBrushOn, pen, new Rect(0, 0, ActualWidth, ActualHeight));
            else
                dc.DrawRectangle(FillBrushOff, pen, new Rect(0, 0, ActualWidth, ActualHeight));
        }
    }
  
}

