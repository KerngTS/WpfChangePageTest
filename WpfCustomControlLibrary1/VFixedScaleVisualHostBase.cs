using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Schema;

namespace WpfCustomControlLibrary1
{
    public class VFixedScaleVisualHostBase : FrameworkElement
    {
        /// <summary>
        /// 图像容器
        /// </summary>
        private VisualCollection _children;
        //private DrawingVisual _backVisual;
        protected double mScale;
        protected double xMin;
        protected double xMax;
        protected double yMin;
        protected double yMax;
        /// <summary>
        /// 默认字体
        /// </summary>
        protected Typeface DefaultTypeFace => new Typeface("Verdana");

        public VFixedScaleVisualHostBase()
        {
            _children = new VisualCollection(this);
            //_backVisual = new DrawingVisual();
            //Add(_backVisual);
        }


        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Background.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(KVisualHostBase),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(RegionChanged)));



        public double RegionWidth
        {
            get { return (double)GetValue(RegionWidthProperty); }
            set { SetValue(RegionWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RegionWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegionWidthProperty =
            DependencyProperty.Register("RegionWidth", typeof(double), typeof(VFixedScaleVisualHostBase),
                new FrameworkPropertyMetadata(200.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(RegionChanged)));

        public double RegionHeight
        {
            get { return (double)GetValue(RegionHeightProperty); }
            set { SetValue(RegionHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RegionWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegionHeightProperty =
            DependencyProperty.Register("RegionHeight", typeof(double), typeof(VFixedScaleVisualHostBase),
                new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(RegionChanged)));

        private static void RegionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var v = d as VFixedScaleVisualHostBase;
            v.UpdatePixelRatio();
            //v.Draw();
        }

        protected void Add(DrawingVisual drawingVisual) => _children.Add(drawingVisual);

        /// <summary>
        /// 转换实际坐标的Y点为图像坐标值
        /// </summary>
        /// <param name="actY"></param>
        /// <returns></returns>
        protected double YAxisConvertYToPixel(double actY)
        {
            var orgY = actY - yMin;
            var orgYToPX = RenderSize.Height - orgY * mScale;
            return orgYToPX;
        }

        /// <summary>
        /// 转换实际坐标的X点为图像坐标值
        /// 水平坐标轴（X轴）值转换，X
        /// 将实际值转换成像素
        /// </summary>
        /// <param name="actX"></param>
        /// <returns></returns>
        protected double XAxisConvertXToPixel(double actX)
        {
            var orgX = actX - xMin;
            var orgXToPX = orgX * mScale;
            return orgXToPX;
        }

        protected Point ConvertToPixel(Point actP)
        {
            var pt = new Point();
            pt.X = XAxisConvertXToPixel(actP.X);
            pt.Y = YAxisConvertYToPixel(actP.Y);
            return pt;
        }

        

        protected override int VisualChildrenCount => _children.Count;

        protected override Visual GetVisualChild(int index)
        {
            return _children[index];
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            UpdatePixelRatio();
            // 显示图形
            //InvalidateVisual();
        }

        private void UpdatePixelRatio()
        {
            var xRate = RegionWidth/RenderSize.Width;
            var yRate = RegionHeight/RenderSize.Height;
            var maxRate = xRate>yRate ? xRate : yRate;
            var width = RenderSize.Width * maxRate;
            var height = RenderSize.Height * maxRate;
            var halfWidth = width / 2.0;
            var halfHeight = height / 2.0;
            xMin=-halfWidth;
            xMax=halfWidth;
            yMin=-halfHeight;
            yMax=halfHeight;
            mScale = 1.0 / maxRate;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            //Draw();
            drawingContext.DrawRectangle(Background, null, new Rect(0, 0, RenderSize.Width, RenderSize.Height));
            base.OnRender(drawingContext);
        }

        //private void Draw()
        //{
        //    this.Dispatcher.Invoke(() =>
        //    {
        //        var dc = _backVisual.RenderOpen();

        //        dc.DrawRectangle(Background, null, new Rect(0, 0, RenderSize.Width, RenderSize.Height));

        //        dc.Close();
        //        // 显示图形
        //        //InvalidateVisual();
        //    });
        //}
    }
}
