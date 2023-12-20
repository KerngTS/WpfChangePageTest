using System;
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
        }

        #region 依賴屬性


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
                DrawDimension(dc, new Point(0.0 - halfWidth, 0.0 - halfHeight), new Point(0,0), new Point(0.0 - halfWidth/2.0, 0.0 - halfHeight-20), "DIMENSION",0);
                dc.Close();
                // 显示图形
                InvalidateVisual();
            });
        }

        //public Rect GetRect()
        //{//繪制預覽模板外形
            
        //    return rect;
        //}

        public void DrawDimension(DrawingContext dc, Point p1,Point p2,Point txtPoint,string dimTxt)
        {
            double fontSize = 10.0;
            var arrowSize = fontSize * 0.8 ;
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
        }

        public void DrawXDimension(DrawingContext dc, Point p1, Point p2, Point txtPoint, string dimTxt)
        {
            double fontSize = 10.0;
            var arrowSize = fontSize * 0.8 *mScale;
            var arrowLen = arrowSize / 2.0 / (Math.Tan(Math.PI / 12.0))*mScale;
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
            dc.DrawLine(pen, ConvertToPixel(dimP1), ConvertToPixel(lftUpArrayPt));
            dc.DrawLine(pen, ConvertToPixel(dimP1), ConvertToPixel(lftBtmArrayPt));
            dc.DrawLine(pen, ConvertToPixel(dimP2), ConvertToPixel(rgtUpArrayPt));
            dc.DrawLine(pen, ConvertToPixel(dimP2), ConvertToPixel(rgtBtmArrayPt));

            //文字
            var txtLoc = ConvertToPixel(txtPoint);

            //dc.DrawEllipse(null,pen,ConvertToPixel(txtLoc),3,3);
            dc.DrawText(text, new Point(txtLoc.X-text.Width/2.0*mScale,txtLoc.Y-(text.Height*1.1)));

        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            Draw();
        }
    }
}
