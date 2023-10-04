using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SynthCustomControls;

// To DO
// 1. Add to Github
// 2. Github using redme
// 3. Article


public class Knob : Control {
    public event EventHandler<double>? ValueChanged;
    
    double _knobWidth;
    bool _redrawKnob = false;

    readonly double MIN_FULL_SWEEP_ANGLE = 20;
    readonly double MAX_FULL_SWEEP_ANGLE = 340;
    double _currentAngle = 0;


    #region Appearance Properties
    public enum MarkerStyleType {
        Line1,          // centre to edge
        Line2,          // to edge
        Line3,          // away from edge
        Dot
    }
    MarkerStyleType _MarkerStyle = MarkerStyleType.Line1;
    public MarkerStyleType MarkerStyle {
        get { return _MarkerStyle; }
        set {
            _MarkerStyle = value;
            DoFullRedraw();
        }
    }

    Color _DotFillColor = Colors.White;
    public Color DotFillColor {
        get { return _DotFillColor; }
        set {
            _DotFillColor = value;
            DoFullRedraw();
        }
    }

    Brush _FillBrush = new SolidColorBrush(Colors.White);
    public Brush FillBrush {
        get { return _FillBrush; }
        set {
            _FillBrush = value;
            DoFullRedraw();
        }
    }

    Color _OutlineColor = Colors.Black;
    public Color OutlineColor {
        get { return _OutlineColor; }
        set {
            _OutlineColor = value;
            DoFullRedraw();
        }
    }

    int _OutlineWidth = 1;
    public int OutlineWidth {
        get { return _OutlineWidth; }
        set {
            _OutlineWidth = value;
            DoFullRedraw();
        }
    }

    int _MarkerWidth = 1;
    public int MarkerWidth {
        get { return _MarkerWidth; }
        set {
            _MarkerWidth = value;
            DoFullRedraw();
        }
    }

    string? _Caption = null;
    public string? Caption {
        get { return _Caption; }
        set {
            _Caption = (value??"").Trim();
            DoFullRedraw();
        }
    }

    bool _CaptionBold = false;
    public bool CaptionBold {
        get { return _CaptionBold; }
        set {
            _CaptionBold = value;
            DoFullRedraw();
        }
    }

    bool _ShowTicks = false;
    public bool ShowTicks {
        get { return _ShowTicks; }
        set {
            _ShowTicks = value;
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

    private AnnotationModeType _AnnotationMode = AnnotationModeType.None;
    public AnnotationModeType AnnotationMode {
        get {
            return _AnnotationMode;
        }
        set {
            _AnnotationMode = value;
            DoFullRedraw();
        }
    }

    // Either show a speicified Tick Label, otherwise show a number 0-n
    private string GetAnnotation(int index) {
        // Depening on Annotation mode, this will either return
        //     an auto number
        //     a string from Annotations property
        //     an image filename from Annotations property 

        switch (_AnnotationMode) {
            case AnnotationModeType.None:
                return "";
            case AnnotationModeType.LabelsAuto:
                return index.ToString();
            case AnnotationModeType.Labels:
                // If we don't have enough Manual Labels, use index
                if (index < (_Annotations?.Count ?? 0) && _Annotations != null)
                    return string.IsNullOrEmpty(_Annotations[index]) ? index.ToString() : _Annotations[index];
                else
                    return index.ToString();
            case AnnotationModeType.Images:
                if (index < (_Annotations?.Count ?? 0) && _Annotations != null)
                    return string.IsNullOrEmpty(_Annotations[index]) ? "" : _Annotations[index];
                else
                    return "";
            default:
                return "";
        }
     }

    List<string> _Annotations = new ();
    public List<string> Annotations {
        get { return _Annotations; }
        set {
            _Annotations = value;
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
    double _maxAngle = 255;

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
            _NumPositions = value == null ? null : ((int)value).Clamp<int>(2, 11);
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

        // COnvert to match point with knob centre at origin
        pMath.X += ActualWidth / 2;
        pMath.Y -= ActualWidth / 2;

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

        var markerPen = new Pen(new SolidColorBrush(OutlineColor), MarkerWidth);

        // Knob Marker
        if (MarkerStyle == MarkerStyleType.Dot)
            dc.DrawEllipse(new SolidColorBrush(_DotFillColor), markerPen, PointFromAngleAndRadius(_currentAngle, _knobWidth / 2 - _knobWidth / 8), _knobWidth / 15, _knobWidth / 15);
        else {
            double r1 = 0, r2 = _knobWidth / 2;        // Go from centre to edge for Line1 
            if (MarkerStyle == MarkerStyleType.Line2 || MarkerStyle == MarkerStyleType.Line3)
                r2 = _knobWidth / 2 * 6 / 8;         // Don't go all the way to the edge for Line2
            if (MarkerStyle == MarkerStyleType.Line3)
                r1 = _knobWidth / 5;                 // Don't start from middle for Line3
            dc.DrawLine(markerPen, PointFromAngleAndRadius(_currentAngle, r1), PointFromAngleAndRadius(_currentAngle, r2));
        }
    }

    void DrawKnob() {
        // Setup a special cached context
        DrawingVisual visual = new();
        using (DrawingContext cacheContext = visual.RenderOpen()) {

            // We can make know size smaller later when adding tick marks and labels
            cacheContext.DrawRectangle(new SolidColorBrush(Colors.Aqua), null, new Rect(new Point(-ActualWidth / 2, -ActualHeight / 2), new Point(ActualWidth / 2, ActualHeight / 2)));

            _knobWidth = ActualWidth - 2;

            // Reserve room for ticks, annotations and caption
            if (_ShowTicks)
                _knobWidth = _knobWidth * 6.25 / 8;
            if (_AnnotationMode != AnnotationModeType.None)
                _knobWidth = _knobWidth * 6.25 / 8;


            // Setup Pen
            var outlinePen = new Pen(new SolidColorBrush(OutlineColor), OutlineWidth);

            // Knob Outline
            cacheContext.DrawEllipse(FillBrush, outlinePen, new Point(0, 0), _knobWidth / 2, _knobWidth / 2);

            // Annotations
            if (_ShowTicks)
                DrawTicks(cacheContext, outlinePen);

            if (_AnnotationMode == AnnotationModeType.Labels || _AnnotationMode == AnnotationModeType.LabelsAuto)
                DrawLabelAnnotations(cacheContext);

            if (_AnnotationMode == AnnotationModeType.Images)
                DrawImageAnnotations(cacheContext);

            if (!string.IsNullOrWhiteSpace(_Caption))
                DrawCaption(cacheContext);
        }

        // Create a new DrawingGroup and add the new drawing operations to it
        DrawingGroup newCachedDrawing = new();
        newCachedDrawing.Children.Add(visual.Drawing);

        // Assign the newDrawingGroup to cachedDrawing
        _cachedDrawing = newCachedDrawing;
    }

    public void DrawTicks(DrawingContext dc, Pen outlinePen) {
        int numTicks = (_NumPositions ?? 11)-1;  //       0-10 ticks if snapangle is off
        var angle = _minAngle;
        for(int i = 0; i <= numTicks; i++) {
            dc.DrawLine(outlinePen, PointFromAngleAndRadius(angle, (_knobWidth/2)*1.1), PointFromAngleAndRadius(angle, (_knobWidth/2)*1.3));
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
            if (_ShowTicks)
                centrePoint = PointFromAngleAndRadius(angle, (_knobWidth / 2) * 1.6);
            else
                centrePoint = PointFromAngleAndRadius(angle, (_knobWidth / 2) * 1.35);

            centrePoint.Y =  centrePoint.Y;
            centrePoint.X = -centrePoint.X;
            //dc.DrawEllipse(FillBrush, new Pen(new SolidColorBrush(Colors.Red), 1), centrePoint, 4, 4);

            var imgFile = GetAnnotation(i);
            if (!string.IsNullOrWhiteSpace(imgFile)) {

                BitmapImage bitmapImage = new (new Uri(imgFile, UriKind.Relative));
                // Move centre of image to plot point
                centrePoint.X -= bitmapImage.Width / 2;
                centrePoint.Y -= bitmapImage.Height/ 2;
                Rect imageRect = new (centrePoint, new Size(bitmapImage.Width, bitmapImage.Height));
                dc.DrawImage(bitmapImage, imageRect);
            }
            angle += (_maxAngle - _minAngle) / (numTicks);
        }
    }

    public void DrawLabelAnnotations(DrawingContext dc) {
        if(ActualHeight == 0) return;

        int numTicks = (_NumPositions ?? 11) - 1;  //       0-10 ticks if snapangle is off
        var angle = _minAngle + 180;
        for (int i = 0; i <= numTicks; i++) {
            var t = new FormattedText(
                GetAnnotation(i),
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("Arial"),
                12 * ActualHeight / 100.0, // Autosize font according to control width, but with poerty scale
                new SolidColorBrush(OutlineColor),
                1.0 // Default pixels per dip value
            );

            Point centrePoint;
            // If not showing ticks, bring annotations in a bit
            if (_ShowTicks)
                centrePoint = PointFromAngleAndRadius(angle, (_knobWidth / 2) * 1.55);
            else
                centrePoint = PointFromAngleAndRadius(angle,(_knobWidth / 2) * 1.35);

            centrePoint.Y = -centrePoint.Y;

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
                 _Caption, System.Globalization.CultureInfo.CurrentCulture,
                 FlowDirection.LeftToRight,
                 new Typeface(new FontFamily("Arial"), FontStyles.Normal, CaptionBold ? FontWeights.Bold : FontWeights.Normal, FontStretches.Normal),
                 17 * ActualHeight / 100.0,                // Autosize font according to control width, with baseline of 17pt
                 new SolidColorBrush(OutlineColor),
                 1.0                                       // Pixels per dip
             );

        var centrePoint = new Point(0, _knobWidth / 2 + _knobWidth/4);
        
        // Move up a bit if not displaying Annotations
        if (_AnnotationMode == AnnotationModeType.None )
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
