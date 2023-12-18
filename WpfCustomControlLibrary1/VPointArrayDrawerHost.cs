using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
            self.RegionWidth = self.ReviewWidth*1.1;
            self.RegionHeight=self.ReviewHeight*1.1;

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
                
                dc.DrawRectangle(null, new Pen(EdgeBorderBrush,1.0), GetRect());
                var pts = GetDefaultPoints();
                //Points = new ObservableCollection<Point>(GetDefaultPoints());
                foreach (var pt in pts)
                {
                    var center = ConvertToPixel(pt);
                    dc.DrawEllipse(null, new Pen(CircleBorderBrush, 1.0), center, mScale * ReviewDiameter / 2.0, mScale * ReviewDiameter / 2.0);
                }
                dc.Close();
                // 显示图形
                InvalidateVisual();
            });
        }

        public Rect GetRect()
        {//繪制預覽模板外形
            var halfWidth = ReviewWidth / 2.0;
            var halfHeight = ReviewHeight / 2.0;
            var p1 = ConvertToPixel(new Point(0.0 - halfWidth, 0.0 - halfHeight));
            var p2 = ConvertToPixel(new Point(halfWidth, halfHeight));
            Rect rect = new Rect(p1, p2);
            return rect;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            Draw();
        }
    }
}
