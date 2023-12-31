﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WpfCustomControlLibrary1.Tools;

namespace WpfCustomControlLibrary1
{
    public class VPointArrayDrawerHost : VFixedScaleVisualHostBase
    {
        private DrawingVisual _visual;

        public VPointArrayDrawerHost()
        {
            _visual = new DrawingVisual();
            Add(_visual);
            SnapsToDevicePixels=true;
        }

        #region 依賴屬性



        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(VPointArrayDrawerHost),
                 new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(HostCallBack)));

        public double ArrowSize
        {
            get { return (double)GetValue(ArrowSizeProperty); }
            set { SetValue(ArrowSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArrowSizeProperty =
            DependencyProperty.Register("ArrowSize", typeof(double), typeof(VPointArrayDrawerHost),
                 new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(HostCallBack)));

        public double ReviewWidth
        {
            get { return (double)GetValue(ReviewWidthProperty); }
            set { SetValue(ReviewWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ReviewWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReviewWidthProperty =
            DependencyProperty.Register("ReviewWidth", typeof(double), typeof(VPointArrayDrawerHost),
                new FrameworkPropertyMetadata(200.0,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,new PropertyChangedCallback(HostCallBack),new CoerceValueCallback(ReviewWidthCoerceValue)));

        private static object ReviewWidthCoerceValue(DependencyObject d, object baseValue)
        {
            return baseValue;
            var self = d as VPointArrayDrawerHost;
            var newValue = (double)baseValue;
            var xRate = newValue / self.Width * 1.1;
            var yRate = self.ReviewHeight / self.Height * 1.1;
            var maxRate = xRate>yRate ? xRate : yRate;
            return newValue/maxRate;
        }

        public double ReviewHeight
        {
            get { return (double)GetValue(ReviewHeightProperty); }
            set { SetValue(ReviewHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ReviewHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReviewHeightProperty =
            DependencyProperty.Register("ReviewHeight", typeof(double), typeof(VPointArrayDrawerHost), 
                new FrameworkPropertyMetadata(150.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(HostCallBack), new CoerceValueCallback(ReviewHeightCoerceValue)));

        private static object ReviewHeightCoerceValue(DependencyObject d, object baseValue)
        {
            return baseValue;
            var self = d as VPointArrayDrawerHost;
            var newValue = (double)baseValue;
            var xRate = self.ReviewWidth / self.Width * 1.1;
            var yRate = newValue / self.Height * 1.1;
            var maxRate = xRate > yRate ? xRate : yRate;
            return newValue / maxRate;
        }

        public double ReviewDiameter
        {   
            get { return (double)GetValue(ReviewDiameterProperty); }
            set { SetValue(ReviewDiameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ReviewDiameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReviewDiameterProperty =
            DependencyProperty.Register("ReviewDiameter", typeof(double), typeof(VPointArrayDrawerHost), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(HostCallBack)));



        public ObservableCollection<Point> Points
        {
            get { return (ObservableCollection<Point>)GetValue(PointsProperty); }   
            set { SetValue(PointsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Points.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(ObservableCollection<Point>), typeof(VPointArrayDrawerHost), new FrameworkPropertyMetadata(new ObservableCollection<Point>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(HostCallBack)));



        public Brush EdgeBorderBrush
        {
            get { return (Brush)GetValue(EdgeBorderBrushProperty); }
            set { SetValue(EdgeBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EdgeBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EdgeBorderBrushProperty =
            DependencyProperty.Register("EdgeBorderBrush", typeof(Brush), typeof(VPointArrayDrawerHost), new FrameworkPropertyMetadata(default(Brush),FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(HostCallBack)));



        public Brush CircleBorderBrush
        {
            get { return (Brush)GetValue(CircleBorderBrushProperty); }
            set { SetValue(CircleBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CircleBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CircleBorderBrushProperty =
            DependencyProperty.Register("CircleBorderBrush", typeof(Brush), typeof(VPointArrayDrawerHost), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(HostCallBack)));


        public Brush SplineBrush
        {
            get { return (Brush)GetValue(SplineBrushProperty); }
            set { SetValue(SplineBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CircleBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplineBrushProperty =
            DependencyProperty.Register("SplineBrush", typeof(Brush), typeof(VPointArrayDrawerHost), new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(HostCallBack)));



        private static void HostCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = d as VPointArrayDrawerHost;
            self.RegionWidth = self.ReviewWidth*1.3;
            self.RegionHeight=self.ReviewHeight*1.3;

            self.Draw();
        }


        #endregion

        private  IList<Point> GetDefaultPoints()
        {
            var result = new List<Point>();
            result.Add(new Point(0, 0));

            var x = ReviewWidth / 2.0 - 20.0;
            var y = ReviewHeight / 2.0 - 20.0;
            result.Add(new Point(x, y));
            result.Add(new Point(-x, -y));
            result.Add(new Point(-x, y));
            result.Add(new Point(x, -y));


            return result;
        }

        private void Draw()
        {
            this.Dispatcher.Invoke(() =>
            {
                var dc = _visual.RenderOpen();
                var halfWidth = ReviewWidth / 2.0;
                var halfHeight = ReviewHeight / 2.0;
                var p1 = ConvertToPixel(new Point(0.0 - halfWidth, 0.0 - halfHeight));
                var p2 = ConvertToPixel(new Point(halfWidth, halfHeight));
                Rect rect = new Rect(p1, p2);
                dc.DrawRectangle(null, new Pen(EdgeBorderBrush,1.0), rect);
                var pts = GetDefaultPoints();
                //Points = new ObservableCollection<Point>(GetDefaultPoints());
                foreach (var pt in pts)
                {
                    var center = ConvertToPixel(pt);
                    dc.DrawEllipse(null, new Pen(CircleBorderBrush, 1.0), center, mScale * ReviewDiameter / 2.0, mScale * ReviewDiameter / 2.0);
                }
                //DrawDimension(dc, p1, ConvertToPixel(new Point(halfWidth, 0.0 - halfHeight)), ConvertToPixel( new Point(0, 0.0 - halfHeight)), "DIMENSION");
                DrawDimension(dc, new Point(0.0 - halfWidth, 0.0 - halfHeight), new Point(0,0), new Point(0.0 - halfWidth/2.0, 0.0 - halfHeight-30), "HDIMENSION",0);
                DrawDimension(dc, new Point(0.0 - halfWidth, 0.0 - halfHeight), new Point(0,0), new Point(0.0 + halfWidth+30, 0.0 - halfHeight/2.0), "VDIMENSION",1);
                DrawSpline(dc);
                dc.Close();
                // 显示图形
                InvalidateVisual();
            });
        }

        public void DrawSpline(DrawingContext dc)
        {
            //var gsc = new GradientStopCollection();
            //gsc.Add(new GradientStop(Colors.Blue, 0));
            //gsc.Add(new GradientStop(Colors.Green, 0.5));
            //gsc.Add(new GradientStop(Colors.Yellow, 1));
            //var brush = new LinearGradientBrush(gsc);
            var pen = new Pen(SplineBrush, 3);

            /*
            //X軸
            dc.DrawLine(pen, ConvertToPixel(new Point(-80, -60)), ConvertToPixel(new Point(80, -60)));
            dc.DrawLine(pen, ConvertToPixel(new Point(-80, -40)), ConvertToPixel(new Point(80, -40)));
            dc.DrawLine(pen, ConvertToPixel(new Point(-80, -20)), ConvertToPixel(new Point(80, -20)));
            dc.DrawLine(pen, ConvertToPixel(new Point(-80, 0)), ConvertToPixel(new Point(80, 0)));
            dc.DrawLine(pen, ConvertToPixel(new Point(-80, 20)), ConvertToPixel(new Point(80, 20)));
            dc.DrawLine(pen, ConvertToPixel(new Point(-80, 40)), ConvertToPixel(new Point(80, 40)));
            dc.DrawLine(pen, ConvertToPixel(new Point(-80, 60)), ConvertToPixel(new Point(80, 60)));

            //Y軸
            dc.DrawLine(pen, ConvertToPixel(new Point(-80, -60)), ConvertToPixel(new Point(-80, 60)));
            dc.DrawLine(pen, ConvertToPixel(new Point(-60, -60)), ConvertToPixel(new Point(-60, 60)));
            dc.DrawLine(pen, ConvertToPixel(new Point(-40, -60)), ConvertToPixel(new Point(-40, 60)));
            dc.DrawLine(pen, ConvertToPixel(new Point(-20, -60)), ConvertToPixel(new Point(-20, 60)));
            dc.DrawLine(pen, ConvertToPixel(new Point(0, -60)), ConvertToPixel(new Point(0, 60)));
            dc.DrawLine(pen, ConvertToPixel(new Point(20, -60)), ConvertToPixel(new Point(20, 60)));
            dc.DrawLine(pen, ConvertToPixel(new Point(40, -60)), ConvertToPixel(new Point(40, 60)));
            dc.DrawLine(pen, ConvertToPixel(new Point(60, -60)), ConvertToPixel(new Point(60, 60)));
            dc.DrawLine(pen, ConvertToPixel(new Point(80, -60)), ConvertToPixel(new Point(80, 60)));
            */

            //PolyQuadraticBezier
            var points = new[]
            {
                ConvertToPixel( new Point(-80, -60)),
                ConvertToPixel(new Point(-60, 60)),
                ConvertToPixel( new Point(-40, -60)),
                ConvertToPixel(new Point(-20, 60)),
                ConvertToPixel( new Point(0, -60)),
                ConvertToPixel(new Point(20, 60)),
                ConvertToPixel( new Point(40, -60)),
                ConvertToPixel(new Point(60, 60)),
                ConvertToPixel(new Point(80, -60)),
            };

            
            var path = MakeCurve(points,1.0);


            //PathGeometry myPathGeometry = new PathGeometry();
            //myPathGeometry.Figures .Add();

            // 使用 DrawGeometry 方法绘制定义的复杂路径
            dc.DrawGeometry(null,pen, (PathGeometry)path.Data);
        }

        private Path MakeCurve(Point[] points, double tension)
        {
            if (points.Length < 2) return null;
            Point[] result_points = MakeCurvePoints(points, tension);

            // Use the points to create the path.
            return MakeBezierPath(result_points.ToArray());
        }


        private Point[] MakeCurvePoints(Point[] points, double tension)
        {
            if (points.Length < 2) return null;
            double control_scale = tension / 0.5 * 0.175;

            List<Point> result_points = new List<Point>();
            result_points.Add(points[0]);

            for (int i = 0; i < points.Length - 1; i++)
            {
                // Get the point and its neighbors.
                Point pt_before = points[Math.Max(i - 1, 0)];
                Point pt = points[i];
                Point pt_after = points[i + 1];
                Point pt_after2 = points[Math.Min(i + 2, points.Length - 1)];

                double dx1 = pt_after.X - pt_before.X;
                double dy1 = pt_after.Y - pt_before.Y;

                Point p1 = points[i];
                Point p4 = pt_after;

                double dx = pt_after.X - pt_before.X;
                double dy = pt_after.Y - pt_before.Y;
                Point p2 = new Point(
                    pt.X + control_scale * dx,
                    pt.Y + control_scale * dy);

                dx = pt_after2.X - pt.X;
                dy = pt_after2.Y - pt.Y;
                Point p3 = new Point(
                    pt_after.X - control_scale * dx,
                    pt_after.Y - control_scale * dy);

                // Save points p2, p3, and p4.
                result_points.Add(p2);
                result_points.Add(p3);
                result_points.Add(p4);
            }

            // Return the points.
            return result_points.ToArray();
        }

        private Path MakeBezierPath(Point[] points)
        {
            // Create a Path to hold the geometry.
            Path path = new Path();

            // Add a PathGeometry.
            PathGeometry path_geometry = new PathGeometry();
            path.Data = path_geometry;

            // Create a PathFigure.
            PathFigure path_figure = new PathFigure();
            path_geometry.Figures.Add(path_figure);

            // Start at the first point.
            path_figure.StartPoint = points[0];

            // Create a PathSegmentCollection.
            PathSegmentCollection path_segment_collection =
                new PathSegmentCollection();
            path_figure.Segments = path_segment_collection;

            // Add the rest of the points to a PointCollection.
            PointCollection point_collection =
                new PointCollection(points.Length - 1);
            for (int i = 1; i < points.Length; i++)
                point_collection.Add(points[i]);

            // Make a PolyBezierSegment from the points.
            PolyBezierSegment bezier_segment = new PolyBezierSegment();
            bezier_segment.Points = point_collection;

            // Add the PolyBezierSegment to othe segment collection.
            path_segment_collection.Add(bezier_segment);

            return path;
        }
        //public Rect GetRect()
        //{//繪制預覽模板外形

        //    return rect;
        //}

        public void DrawDimension(DrawingContext dc, Point p1,Point p2,Point txtPoint,string dimTxt)
        {
            double fontSize = FontSize;
            var arrowSize = ArrowSize ;
            var arrowLen = arrowSize / 2.0 / (Math.Tan(Math.PI / 12.0));
            var text = new FormattedText(dimTxt, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), fontSize, Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip);
            var midPoint = new Point((p1.X + p2.X) / 2.0, (p1.Y + p2.Y) / 2.0);
            var ptDist = Math.Sqrt(Math.Pow(p2.X-p1.X,2)+Math.Pow(p2.Y-p1.Y,2));
            var txtLoc = new Point(txtPoint.X - text.Width / 2, txtPoint.Y - text.Height/2.0);

            var lftUpArrayPt = new Point(p1.X+arrowLen, p1.Y+arrowSize/2.0);
            var lftBtmArrayPt = new Point(p1.X+arrowLen, p1.Y-arrowSize/2.0);

            var rgtUpArrayPt = new Point(p2.X - arrowLen, p2.Y + arrowSize / 2.0);
            var rgtBtmArrayPt = new Point(p2.X - arrowLen, p2.Y - arrowSize / 2.0);

            var segDst = (ptDist - text.Width) / 2.0 - 3;
            var lftSegPt = new Point(p1.X + segDst, p1.Y);
            var rgtSegPt = new Point(p2.X - segDst, p2.Y);

            var pen = new Pen(new SolidColorBrush(Colors.Black), 0.3);

            //文字
            dc.PushTransform(new RotateTransform { Angle = 270 ,CenterX = midPoint.X,CenterY= midPoint.Y}); ;
            dc.DrawText(text, txtLoc);
            dc.Pop();

            //畫左邊
            dc.DrawLine(pen, p1, lftUpArrayPt);
            dc.DrawLine(pen, p1, lftBtmArrayPt);
            dc.DrawLine(pen, p1, lftSegPt);

            //畫右邊
            dc.DrawLine(pen, p2, rgtUpArrayPt);
            dc.DrawLine(pen, p2, rgtBtmArrayPt);
            dc.DrawLine(pen, p2, rgtSegPt);

            
        }

        public void DrawDimension(DrawingContext dc, Point p1, Point p2, Point txtPoint, string dimTxt,short xy)
        {
            if(xy==0) DrawXDimension(dc,p1,p2,txtPoint,dimTxt);
            else if(xy==1) DrawYDimension(dc,p1,p2,txtPoint,dimTxt);
        }

        public void DrawXDimension(DrawingContext dc, Point p1, Point p2, Point txtPoint, string dimTxt)
        {
            double fontSize = FontSize;
            var arrowSize = ArrowSize;
            var arrowLen = arrowSize / 2.0 / (Math.Tan(Math.PI / 12.0));
            var text = new FormattedText(dimTxt, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), fontSize, Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip);

            //計算標點到文字放置點的距離
            var xv = new Vector(1, 0);
            var yv = new Vector(0, 1);
            var dist1 = p1.DistTo(txtPoint, yv);
            var dist2 = p2.DistTo(txtPoint, yv);

            var pen = new Pen(new SolidColorBrush(Colors.Black), 0.5);
            //標注點
            var dimP1 = p1.Move(yv, dist1);
            var dimP2 = p2.Move(yv, dist2);

            //引線
            var leader1EndPoint = p1.Move(yv, dist1 + (dist1 >= 0 ? 5 : -5));
            var leader2EndPoint = p2.Move(yv, dist2 + (dist2 >= 0 ? 5 : -5));
            dc.DrawLine(pen, ConvertToPixel(p1), ConvertToPixel(leader1EndPoint));
            dc.DrawLine(pen, ConvertToPixel(p2), ConvertToPixel(leader2EndPoint));

            //尺寸線與箭頭
            dc.DrawLine(pen, ConvertToPixel(dimP1), ConvertToPixel(dimP2));

            var lftUpArrayPt = new Point(dimP1.X + arrowLen, dimP1.Y + arrowSize / 2.0);
            var lftBtmArrayPt = new Point(dimP1.X + arrowLen, dimP1.Y - arrowSize / 2.0);

            var rgtUpArrayPt = new Point(dimP2.X - arrowLen, dimP2.Y + arrowSize / 2.0);
            var rgtBtmArrayPt = new Point(dimP2.X - arrowLen, dimP2.Y - arrowSize / 2.0);

            var pixelDimP1 = ConvertToPixel(dimP1);
            var pixelDimP2 = ConvertToPixel(dimP2);
            dc.DrawLine(pen, pixelDimP1, new Point(pixelDimP1.X + arrowLen, pixelDimP1.Y + arrowSize / 2.0));
            dc.DrawLine(pen, pixelDimP1, new Point(pixelDimP1.X + arrowLen, pixelDimP1.Y - arrowSize / 2.0));
            dc.DrawLine(pen, pixelDimP2, new Point(pixelDimP2.X - arrowLen, pixelDimP2.Y + arrowSize / 2.0));
            dc.DrawLine(pen, pixelDimP2, new Point(pixelDimP2.X - arrowLen, pixelDimP2.Y - arrowSize / 2.0));

            //文字
            var txtLoc = ConvertToPixel(txtPoint);

            //dc.DrawEllipse(null,pen,ConvertToPixel(txtLoc),3,3);
            var txtLen = text.Width ;
            var txtWid = text.Height;
            dc.DrawText(text, new Point(txtLoc.X-txtLen/2.0,txtLoc.Y-(txtWid * 1.1)));

        }

        public void DrawYDimension(DrawingContext dc, Point p1, Point p2, Point txtPoint, string dimTxt)
        {
            double fontSize = FontSize;
            var arrowSize = ArrowSize;
            var arrowLen = arrowSize / 2.0 / (Math.Tan(Math.PI / 12.0));
            var text = new FormattedText(dimTxt, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), fontSize, Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip);

            //計算標點到文字放置點的距離
            var xv = new Vector(1, 0);
            var yv = new Vector(0, 1);
            var dist2 = p2.DistTo(txtPoint, xv);
            var dist1 = p1.DistTo(txtPoint, xv);

            var pen = new Pen(new SolidColorBrush(Colors.Black), 0.5);
            //標注點
            var dimP1 = p1.Move(xv, dist1);
            var dimP2 = p2.Move(xv, dist2);

            //引線
            var leader1EndPoint = p1.Move(xv, dist1 + (dist1 >= 0 ? 5 : -5));
            var leader2EndPoint = p2.Move(xv, dist2 + (dist2 >= 0 ? 5 : -5));
            dc.DrawLine(pen, ConvertToPixel(p1), ConvertToPixel(leader1EndPoint));
            dc.DrawLine(pen, ConvertToPixel(p2), ConvertToPixel(leader2EndPoint));

            //尺寸線與箭頭
            dc.DrawLine(pen, ConvertToPixel(dimP1), ConvertToPixel(dimP2));

            

            var pixelDimP1 = ConvertToPixel(dimP1);
            var pixelDimP2 = ConvertToPixel(dimP2);
            dc.DrawLine(pen, pixelDimP1, new Point(pixelDimP1.X + arrowSize / 2.0, pixelDimP1.Y - arrowLen));
            dc.DrawLine(pen, pixelDimP1, new Point(pixelDimP1.X - arrowSize / 2.0, pixelDimP1.Y - arrowLen));
            dc.DrawLine(pen, pixelDimP2, new Point(pixelDimP2.X + arrowSize / 2.0, pixelDimP2.Y + arrowLen));
            dc.DrawLine(pen, pixelDimP2, new Point(pixelDimP2.X - arrowSize / 2.0, pixelDimP2.Y + arrowLen));

            //文字
            var txtLoc = ConvertToPixel(txtPoint);

            //dc.DrawEllipse(null,pen,ConvertToPixel(txtLoc),3,3);
            var txtLen = text.Width;
            var txtWid = text.Height;
            dc.PushTransform(new RotateTransform(270, txtLoc.X , txtLoc.Y ));
            dc.DrawText(text, new Point(txtLoc.X - txtLen / 2.0, txtLoc.Y - (txtWid * 1.1)));
            dc.Pop();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            Draw();
        }
    }
}
