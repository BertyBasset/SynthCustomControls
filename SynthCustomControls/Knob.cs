using SynthCustomControls.Utils;
using System.Globalization;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SynthCustomControls;


public class Knob : Control {
    #region Private Variables & Constants
    public event EventHandler<double>? ValueChanged;

    double _knobWidth;
    bool _redrawKnob = false;

    readonly double MIN_FULL_SWEEP_ANGLE = 20;
    readonly double MAX_FULL_SWEEP_ANGLE = 340;
    readonly int MAX_NUM_TICK_POSITIONS = 10;

    double _currentAngle = 0;
    #endregion

    #region Appearance Properties
    public enum MarkerStyleType {
        Line1,          // centre to edge
        Line2,          // to edge
        Line3,          // away from edge
        Dot
    }


    public static readonly DependencyProperty MarkerStyleProperty =
        DependencyProperty.Register(
            "MarkerStyle",
            typeof(MarkerStyleType),
            typeof(Knob),
            new PropertyMetadata(MarkerStyleType.Line1));

    public MarkerStyleType? MarkerStyle {
        get { return (MarkerStyleType)GetValue(MarkerStyleProperty); }
        set {
            SetValue(MarkerStyleProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty DotFillColorProperty =
        DependencyProperty.Register(
             "DotFillColor",
             typeof(Color),
             typeof(Knob),
             new PropertyMetadata(Colors.White));

    public Color DotFillColor {
        get { return (Color)GetValue(DotFillColorProperty); }
        set {
            SetValue(DotFillColorProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty FillBrushProperty =
        DependencyProperty.Register(
             "FillBrush",
             typeof(Brush),
             typeof(Knob),
             new PropertyMetadata(null));

    public Brush? FillBrush {
        get { return (Brush?)GetValue(FillBrushProperty); }
        set {
            SetValue(FillBrushProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty OutlineColorProperty =
      DependencyProperty.Register(
          "OutlineColor",
          typeof(Color),
          typeof(Knob),
          new PropertyMetadata(Colors.Black));

    public Color OutlineColor {
        get { return (Color)GetValue(OutlineColorProperty); }
        set {
            SetValue(OutlineColorProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty OutlineWidthProperty =
        DependencyProperty.Register(
            "OutlineWidth",
            typeof(int),
            typeof(Knob),
            new PropertyMetadata(1));

    public int OutlineWidth {
        get { return (int)GetValue(OutlineWidthProperty); }
        set {
            SetValue(OutlineWidthProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty MarkerWidthProperty =
        DependencyProperty.Register(
            "MarkerWidth",
            typeof(int),
            typeof(Knob),
            new PropertyMetadata(1));

    public int MarkerWidth {
        get { return (int)GetValue(MarkerWidthProperty); }
        set {
            SetValue(MarkerWidthProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty MarkerColorProperty =
        DependencyProperty.Register(
            "MarkerColor",
            typeof(Color),
            typeof(Knob),
            new PropertyMetadata(Colors.Black));

    public Color MarkerColor {
        get { return (Color)GetValue(MarkerColorProperty); }
        set {
            SetValue(MarkerColorProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register(
            "Caption",
            typeof(string),
            typeof(Knob),
            new PropertyMetadata(null));

    public string? Caption {
        get { return (string?)GetValue(CaptionProperty); }
        set {
            SetValue(CaptionProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty CaptionColorProperty =
        DependencyProperty.Register(
            "CaptionColor",
            typeof(Color),
            typeof(Knob),
            new PropertyMetadata(Colors.Black));

    public Color CaptionColor {
        get { return (Color)GetValue(CaptionColorProperty); }
        set {
            SetValue(CaptionColorProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty CaptionBoldProperty =
        DependencyProperty.Register(
            "CaptionBold",
            typeof(bool),
            typeof(Knob),
            new PropertyMetadata(false));

    public bool CaptionBold {
        get { return (bool)GetValue(CaptionBoldProperty); }
        set {
            SetValue(CaptionBoldProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty ManualCaptionRadiusProperty =
    DependencyProperty.Register(
        "ManualCaptionRadius",
        typeof(double?),
        typeof(Knob),
        new PropertyMetadata(null));

    public double? ManualCaptionRadius {
        get { return (double?)GetValue(ManualCaptionRadiusProperty); }
        set {
            SetValue(ManualCaptionRadiusProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty ManualCaptionFontSizeProperty =
        DependencyProperty.Register(
            "ManualCaptionFontSize",
            typeof(double?),
            typeof(Knob),
            new PropertyMetadata(null));

    public double? ManualCaptionFontSize {
        get { return (double?)GetValue(ManualCaptionFontSizeProperty); }
        set {
            SetValue(ManualCaptionFontSizeProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty ShowTicksProperty =
        DependencyProperty.Register(
            "ShowTicks",
            typeof(bool),
            typeof(Knob),
            new PropertyMetadata(false));

    public bool ShowTicks {
        get { return (bool)GetValue(ShowTicksProperty); }
        set {
            SetValue(ShowTicksProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty TickColorProperty =
        DependencyProperty.Register(
            "TickColor",
            typeof(Color),
            typeof(Knob),
            new PropertyMetadata(Colors.Black));

    public Color TickColor {
        get { return (Color)GetValue(TickColorProperty); }
        set {
            SetValue(TickColorProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty TickWidthProperty =
        DependencyProperty.Register(
            "TickWidth",
            typeof(int),
            typeof(Knob),
            new PropertyMetadata(1));

    public int TickWidth {
        get { return (int)GetValue(TickWidthProperty); }
        set {
            SetValue(TickWidthProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty ManualTickRadiusStartProperty =
        DependencyProperty.Register(
            "ManualTickRadiusStart",
            typeof(double?),
            typeof(Knob),
            new PropertyMetadata(null));

    ///  This is relative to Knob width, not control width
    public double? ManualTickRadiusStart {
        get { return (double?)GetValue(ManualTickRadiusStartProperty); }
        set {
            SetValue(ManualTickRadiusStartProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty ManualTickRadiusEndProperty =
        DependencyProperty.Register(
            "ManualTickRadiusEnd",
            typeof(double?),
            typeof(Knob),
            new PropertyMetadata(null));

    ///  This is relative to Knob width, not control width
    public double? ManualTickRadiusEnd {
        get { return (double?)GetValue(ManualTickRadiusEndProperty); }
        set {
            SetValue(ManualTickRadiusEndProperty, value);
            DoFullRedraw();
        }
    }
    

    // Annotations
    public enum AnnotationModeType {
        None,
        LabelsAuto,
        Labels,
        Images
    }


    public static readonly DependencyProperty AnnotationModeProperty =
        DependencyProperty.Register(
            "AnnotationMode",
            typeof(AnnotationModeType),
            typeof(Knob),
            new PropertyMetadata(AnnotationModeType.None));

    public AnnotationModeType AnnotationMode {
        get { return (AnnotationModeType)GetValue(AnnotationModeProperty); }
        set {
            SetValue(AnnotationModeProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty AnnotationTextColorProperty =
        DependencyProperty.Register(
            "AnnotationTextColor",
            typeof(Color),
            typeof(Knob),
            new PropertyMetadata(Colors.Black));

    public Color AnnotationTextColor {
        get { return (Color)GetValue(AnnotationTextColorProperty); }
        set {
            SetValue(AnnotationTextColorProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty AnnotationImageResourceKeysProperty =
        DependencyProperty.Register(
            "AnnotationImageResourceKeys",
            typeof(List<string>),
            typeof(Knob),
            new PropertyMetadata(new List<string>()));

    public List<string> AnnotationImageResourceKeys {
        get { return (List<string>)GetValue(AnnotationImageResourceKeysProperty); }
        set {
            SetValue(AnnotationImageResourceKeysProperty, value);
            DoFullRedraw();
        }
    }


    // Either show a speicified Tick Label, otherwise show a number 0-n
    private string GetAnnotationLabel(int index) {
        // Depening on Annotation mode, this will either return
        //     an auto number
        //     a string from Annotations property
        //     an image filename from Annotations property 

        switch (AnnotationMode) {
            case AnnotationModeType.None:
                return "";
            case AnnotationModeType.LabelsAuto:
                return index.ToString();
            case AnnotationModeType.Labels:
                // If we don't have enough Manual Labels, use index
                if (index < (AnnotationLabels?.Count ?? 0) && AnnotationLabels != null)
                    return string.IsNullOrEmpty(AnnotationLabels[index]) ? index.ToString() : AnnotationLabels[index];
                else
                    return index.ToString();
            case AnnotationModeType.Images:
                if (index < (AnnotationLabels?.Count ?? 0) && AnnotationLabels != null)
                    return string.IsNullOrEmpty(AnnotationLabels[index]) ? "" : AnnotationLabels[index];
                else
                    return "";
            default:
                return "";
        }
     }


    public static readonly DependencyProperty AnnotationLabelsProperty =
        DependencyProperty.Register(
            "AnnotationLabels",
            typeof(List<string>),
            typeof(Knob),
            new PropertyMetadata(new List<string>()));

    public List<string> AnnotationLabels {
        get { return (List<string>)GetValue(AnnotationLabelsProperty); }
        set {
            SetValue(AnnotationLabelsProperty, value);
            DoFullRedraw();
        }
    }


    public static readonly DependencyProperty ManualAnnotationRadiusProperty =
        DependencyProperty.Register(
            "ManualAnnotationRadius",
            typeof(double?),
            typeof(Knob),
            new PropertyMetadata(null));

    public double? ManualAnnotationRadius {
        get { return (double?)GetValue(ManualAnnotationRadiusProperty); }
        set {
            SetValue(ManualAnnotationRadiusProperty, value);
            DoFullRedraw();
        }
    }

    
    public static readonly DependencyProperty ManualAnnotationFontSizeProperty =
    DependencyProperty.Register(
        "ManualAnnotationFontSize",
        typeof(double?),
        typeof(Knob),
        new PropertyMetadata(null));

    public double? ManualAnnotationFontSize {
        get { return (double?)GetValue(ManualAnnotationFontSizeProperty); }
        set {
            SetValue(ManualAnnotationFontSizeProperty, value);
            DoFullRedraw();
        }
    }
    #endregion

    #region Value and Behavious Properties
    double _Value = 0;
    public double Value {
        get { return _Value; }
        set {
            _Value = value.Clamp(_ValueMin, _ValueMax);
            DoFullRedraw();
        }
    }

    double _ValueMin = 0;
    public double ValueMin {
        get { return _ValueMin; }
        set {
            _ValueMin = value;
            DoFullRedraw();
        }
    }

    double _ValueMax = 1;
    public double ValueMax {
        get { return _ValueMax; }
        set {
            _ValueMax = value;
            DoFullRedraw();
        }
    }

    double _minAngle = -45;
    double _maxAngle = 225;

    double _FullSweepAngle = 270;
    public double FullSweepAngle {
        get { return _FullSweepAngle; }
        set {
            _FullSweepAngle = value.Clamp(MIN_FULL_SWEEP_ANGLE, MAX_FULL_SWEEP_ANGLE);
            double halfSweep = FullSweepAngle / 2;

            _minAngle = 90 - halfSweep;
            _maxAngle = 90 + halfSweep;

            DoFullRedraw();
        }
    }

    // Null - continuous
    // Set to {x:Null} is xaml
    int? _NumPositions = null;
    public int? NumPositions {
        get { return _NumPositions; }
        // Allow continuous (NULL) or anything between 2 and 10 positions
        set { 
            _NumPositions = value == null ? null : ((int)value).Clamp<int>(2, MAX_NUM_TICK_POSITIONS);
            DoFullRedraw();
        }
    }
    #endregion

    #region Constructor and Init
    public Knob() {
        DoFullRedraw();

        // Register for mouse events
        MouseLeftButtonDown += OnMouseLeftButtonDown;
        MouseLeftButtonUp += OnMouseLeftButtonUp;
        MouseMove += OnMouseMove;
        this.SizeChanged += (o, e) => {
            double newSize = Math.Max(e.NewSize.Width, e.NewSize.Height);
            this.Width = newSize;
            this.Height = newSize;
            DoFullRedraw();
        };

    }
    #endregion

    #region Mouse Move
    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
        CaptureMouse();
        UpdateAngle(e.GetPosition(this));
    }

    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
        ReleaseMouseCapture();
    }

    private void OnMouseMove(object sender, MouseEventArgs e) {
        if (IsMouseCaptured) {
            UpdateAngle(e.GetPosition(this));
        }
    }

    void UpdateAngle(Point p) {
        // p use following co-ord system
        //  ^ -height
        //  |
        //  |
        // 0|
        //  .------------>x
        //  0           +width

        // Map p to p in mathematical co-ords
        var pMath = ScreenPointToMathPoint(p);

        _currentAngle = Math.Atan2(pMath.Y, pMath.X) * 180.0 / Math.PI;
        if (_currentAngle < -90)                    // Or 'standard' angles are -90 to 269.99999
            _currentAngle += 360.0;

        // Limit angle to the Full sweep angle symmertic around +ve Y axis  
        _currentAngle = _currentAngle.Clamp(_minAngle, _maxAngle);


        // Snap to nearest Angle
        if (_NumPositions != null) {
            double interval = (_maxAngle - _minAngle) / ((double)_NumPositions - 1);
            // Calculate the snapped value by rounding to the nearest snap position
            double _snapped = Math.Round((_currentAngle - _minAngle) / interval) * interval + _minAngle;
            // Ensure the snapped value is within the specified range
            double _snapAngle = Math.Max(_minAngle, Math.Min(_maxAngle, _snapped));

            // If snap angle changed, update everything
            if (_snapAngle != _currentAngle) {
                _currentAngle = _snapAngle;
                SetValueFromAngle();
                ValueChanged?.Invoke(this, _Value);
                InvalidateVisual();     // Don't need full redraw, just marker
            }
            return;
        }

        SetValueFromAngle();
        ValueChanged?.Invoke(this, _Value);
        InvalidateVisual();     // Don't need full redraw, just marker
    }

    Point ScreenPointToMathPoint(Point p) {
        // Map p to p in mathematical co-ords, so y must be inverted

        var x = p.X - ActualWidth / 2;
        var y = -p.Y + ActualHeight / 2;        
        return new Point(x, y);
    }
    #endregion

    #region Draw Control
    void DoFullRedraw() {
        _redrawKnob = true;
        InvalidateVisual();
    }

    protected override void OnRender(DrawingContext dc) {
        base.OnRender(dc);
        SetCurrentAngleFromCurrentValue();
        if(_redrawKnob)         // Need full refresh
            DrawKnob();
        
        DrawMarker(dc);

        _redrawKnob = false;
    }
    
    // When mouse dragging, we only need to update the marker, however
    // we do need to retrieve cahced background first
    // This is our cache
    private DrawingGroup _cachedDrawing = new();

    void DrawMarker(DrawingContext dc) {
        // Retrieve background from cache
        dc.DrawDrawing(_cachedDrawing);

        var markerPen = new Pen(new SolidColorBrush(MarkerColor), MarkerWidth);

        // Knob Marker
        if (MarkerStyle == MarkerStyleType.Dot) {
            var point = PointFromAngleAndRadius(_currentAngle, _knobWidth / 2 - _knobWidth / 8);
            dc.DrawEllipse(new SolidColorBrush(DotFillColor), markerPen, RecenterPointToScreen(point), _knobWidth / 15, _knobWidth / 15);
        } else {
            double r1 = 0, r2 = _knobWidth / 2;        // Go from centre to edge for Line1 
            if (MarkerStyle == MarkerStyleType.Line2 || MarkerStyle == MarkerStyleType.Line3)
                r2 = _knobWidth / 2 * 6 / 8;         // Don't go all the way to the edge for Line2
            if (MarkerStyle == MarkerStyleType.Line3)
                r1 = _knobWidth / 5;                 // Don't start from middle for Line3

            var fromPoint = PointFromAngleAndRadius(_currentAngle, r1);
            var toPoint = PointFromAngleAndRadius(_currentAngle, r2);

            // Points are relative to knob centre, so offset to give absolute

            dc.DrawLine(markerPen, RecenterPointToScreen(fromPoint), RecenterPointToScreen(toPoint));
        }
    }


    Point RecenterPointToScreen(Point p) {
        // We are caculating with (0, 0) at centre, so offset
        p.X += ActualWidth / 2;
        p.Y += ActualHeight / 2;
        return p;
    }


    void DrawKnob() {
        // Setup a special cached context
        DrawingVisual visual = new();
        using (DrawingContext cacheContext = visual.RenderOpen()) {

            // Debug rectangle
            //cacheContext.DrawRectangle(new SolidColorBrush(Colors.Aqua), null, new Rect(new Point(-ActualWidth / 2, -ActualHeight / 2), new Point(ActualWidth / 2, ActualHeight / 2)));

            _knobWidth = ActualWidth - 2;

            // Reserve room for ticks, annotations and caption
            if (ShowTicks)
                _knobWidth = _knobWidth * 6.25 / 8;
            if (AnnotationMode != AnnotationModeType.None)
                _knobWidth = _knobWidth * 6.25 / 8;


            // Setup Pen
            var outlinePen = new Pen(new SolidColorBrush(OutlineColor), OutlineWidth);

            // Knob Outline
            cacheContext.DrawEllipse(FillBrush, outlinePen, new Point(ActualWidth / 2, ActualWidth / 2), _knobWidth / 2, _knobWidth / 2);

            // Annotations
            if (ShowTicks)
                DrawTicks(cacheContext);

            if (AnnotationMode == AnnotationModeType.Labels || AnnotationMode == AnnotationModeType.LabelsAuto)
                DrawLabelAnnotations(cacheContext);

            if (AnnotationMode == AnnotationModeType.Images)
                DrawImageAnnotations(cacheContext);

            if (!string.IsNullOrWhiteSpace(Caption))
                DrawCaption(cacheContext);
        }

        // Create a new DrawingGroup and add the new drawing operations to it
        DrawingGroup newCachedDrawing = new();
        newCachedDrawing.Children.Add(visual.Drawing);

        // Assign the newDrawingGroup to cachedDrawing
        _cachedDrawing = newCachedDrawing;
    }

    public void DrawTicks(DrawingContext dc) {
        var tickPen = new Pen(new SolidColorBrush(TickColor), TickWidth);

        int numTicks = (_NumPositions ?? MAX_NUM_TICK_POSITIONS + 1) -1;  //       0-10 ticks if snapangle is off
        var angle = _minAngle;

        // Override auto tick positions if manually set
        var tickRadiusStart = ManualTickRadiusStart ?? 1.1;
        var tickRadiusEnd = ManualTickRadiusEnd ?? 1.3;


        for (int i = 0; i <= numTicks; i++) {
            var fromPoint = PointFromAngleAndRadius(angle, (_knobWidth / 2) * tickRadiusStart);
            var toPoint = PointFromAngleAndRadius(angle, (_knobWidth / 2) * tickRadiusEnd);


            dc.DrawLine(tickPen, RecenterPointToScreen(fromPoint), RecenterPointToScreen(toPoint));
            angle += (_maxAngle - _minAngle) / (numTicks);
        }
    }

    // Return true if we have displayed tick images
    public void DrawImageAnnotations(DrawingContext dc) {
        if (ActualHeight == 0)
            return;
   
        int numTicks = (_NumPositions ?? 11) - 1;  //       0-10 ticks if snapangle is off
        var angle = _minAngle;
        for (int i = 0; i <= numTicks; i++) {
            Point centrePoint;
            // If not showing ticks, bring annotations in a bit
            if (ShowTicks)
                centrePoint = PointFromAngleAndRadius(angle, (_knobWidth / 2) * 1.6);
            else
                centrePoint = PointFromAngleAndRadius(angle, (_knobWidth / 2) * 1.35);
            // If manual radius set, use it
            if (ManualAnnotationRadius != null)
                centrePoint = PointFromAngleAndRadius(angle, (_knobWidth / 2) * (double)ManualAnnotationRadius);

            centrePoint.Y = centrePoint.Y;
            centrePoint.X = -centrePoint.X;

            // Points are relative to knob centre, so offset to give absolute
            centrePoint = RecenterPointToScreen(centrePoint);

            //dc.DrawEllipse(FillBrush, new Pen(new SolidColorBrush(Colors.Red), 1), centrePoint, 4, 4);

            if (i >= AnnotationImageResourceKeys.Count)
                return;

            // Get Image by key

            if (this.FindResource(AnnotationImageResourceKeys[i]) is BitmapImage bitmapImage) {
                // Move centre of image to plot point
                centrePoint.X -= bitmapImage.Width / 2;
                centrePoint.Y -= bitmapImage.Height / 2;
                Rect imageRect = new(centrePoint, new Size(bitmapImage.Width, bitmapImage.Height));
                dc.DrawImage(bitmapImage, imageRect);

                angle += (_maxAngle - _minAngle) / (numTicks);
            }
        }
    }

    public void DrawLabelAnnotations(DrawingContext dc) {
        if(ActualHeight == 0) return;

        int numTicks = (_NumPositions ?? 11) - 1;  //       0-10 ticks if snapangle is off
        var angle = _minAngle + 180;
        for (int i = 0; i <= numTicks; i++) {
            var t = new FormattedText(
                GetAnnotationLabel(i),
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("Arial"),
                // Override font size if set, otherwise auto
                ManualAnnotationFontSize ?? (12 * ActualHeight / 100.0), // Autosize font according to control width, but with poerty scale
                new SolidColorBrush(AnnotationTextColor),
                1.0 // Default pixels per dip value
            );

            Point centrePoint;
            // If not showing ticks, bring annotations in a bit
            if (ShowTicks)
                centrePoint = PointFromAngleAndRadius(angle, (_knobWidth / 2) * 1.55);
            else
                centrePoint = PointFromAngleAndRadius(angle,(_knobWidth / 2) * 1.35);

            // If manual radius set, use it
            if(ManualAnnotationRadius != null)
                centrePoint = PointFromAngleAndRadius(angle, (_knobWidth / 2) * (double)ManualAnnotationRadius);

            // Transform mathematical to screen co-ords
            centrePoint.Y = -centrePoint.Y;

            centrePoint = RecenterPointToScreen(centrePoint);

            // Debug just to show centre of text
            //dc.DrawEllipse(FillBrush, new Pen(new SolidColorBrush(Colors.Red), 1), centrePoint, 4, 4);

            // Offset text due to its width and height
            var offsetPoint = new Point(centrePoint.X - t.Width/2, centrePoint.Y - t.Height/2);

            dc.DrawText(t, offsetPoint);

            angle += (_maxAngle - _minAngle) / (numTicks);
        }
    }

    public void DrawCaption(DrawingContext dc) {
        if (ActualHeight == 0)
            return;

        var t = new FormattedText(
                 Caption, System.Globalization.CultureInfo.CurrentCulture,
                 FlowDirection.LeftToRight,
                 new Typeface(new FontFamily("Arial"), FontStyles.Normal, CaptionBold ? FontWeights.Bold : FontWeights.Normal, FontStretches.Normal),
                 ManualCaptionFontSize ?? 17 * ActualHeight / 100.0,                // Autosize font according to control width, with baseline of 17pt
                 new SolidColorBrush(CaptionColor),
                 1.0                                       // Pixels per dip
             );

        var centrePoint = new Point(0, ManualCaptionRadius == null ? ( _knobWidth / 2 + _knobWidth/4) : (double)ManualCaptionRadius * _knobWidth / 2);

        // Points are relative to knob centre, so offset to give absolute
        centrePoint = RecenterPointToScreen(centrePoint);

        // Move up a bit if not displaying Annotations
        if (AnnotationMode == AnnotationModeType.None )
            centrePoint.Y -= _knobWidth / 10;

        // Offset text due to its width and height
        var offsetPoint = new Point(centrePoint.X - t.Width / 2, centrePoint.Y - t.Height / 2);
        dc.DrawText(t, offsetPoint);
    }
    #endregion

    #region Calc Utils
    void SetCurrentAngleFromCurrentValue() {
        // For given sweep angle, we want following min and max angles       
        //  90   45    135
        //  180  0     180
        //  270  -45   225
        //  350  -85   265
        //

        var fraction = (_Value - _ValueMin) / (_ValueMax - _ValueMin);
        _currentAngle = _maxAngle - (_maxAngle - _minAngle) * fraction;
    }

    void SetValueFromAngle() {
        // Calculate the fraction based on the angle and min/max angles
        var fraction = (_maxAngle - _currentAngle) / (_maxAngle - _minAngle);

        // Calculate the value using the fraction and min/max values
        _Value = _ValueMin + fraction * (_ValueMax - _ValueMin);
    }

    // Get co-oridinate of point [radius] distance away from (0, 0) at specified [angle] 
    static Point PointFromAngleAndRadius(double angle, double radius) {
        var p = new Point(Math.Cos(angle * Math.PI / 180.0) * radius, Math.Sin(angle * Math.PI / 180.0) * radius);

        p.Y = - p.Y;
        return p;
    }
    #endregion
}



