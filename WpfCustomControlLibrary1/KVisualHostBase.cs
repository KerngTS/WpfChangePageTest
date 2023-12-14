using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace WpfCustomControlLibrary1
{
    public class KVisualHostBase:FrameworkElement
    {
        /// <summary>
        /// 图像容器
        /// </summary>
        private VisualCollection _children;
        /// <summary>
        /// 默认字体
        /// </summary>
        protected Typeface DefaultTypeFace => new Typeface("Verdana");
        /// <summary>
        /// 绘图区域的宽：X
        /// </summary>
        protected double DrawingRegionWidth { get; set; } = 200;
        /// <summary>
        /// 绘图区域的高：Y
        /// </summary>
        protected double DrawingRegionHeight { get; set; } = 200;
        /// <summary>
        /// X
        /// 水平方向
        /// 像素比
        /// 1单位数据对应多少像素
        /// 根据实际场景宽度和控件宽度得到
        /// </summary>
        protected double XPixelRatio { get; set; } = 10;
        /// <summary>
        /// Y
        /// 垂直方向
        /// 像素比
        /// 1单位数据对应多少像素
        /// 根据实际场景长(高)度和控件高度得到
        /// </summary>
        protected double YPixelRatio { get; set; } = 10;

        public KVisualHostBase()
        {
            _children = new VisualCollection(this); 
        }


        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Background.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(KVisualHostBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Y轴最大值
        /// </summary>
        public double YMax
        {
            get { return (double)GetValue(YMaxProperty); }
            set { SetValue(YMaxProperty, value); }
        }
        private static readonly DependencyProperty YMaxProperty =
            DependencyProperty.Register("YMax", typeof(double), typeof(KVisualHostBase),
                new FrameworkPropertyMetadata(100D, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(YRangeChanged)));

        /// <summary>
        /// Y轴最小值
        /// </summary>
        public double YMin
        {
            get { return (double)GetValue(YMinProperty); }
            set { SetValue(YMinProperty, value); }
        }
        public static readonly DependencyProperty YMinProperty =
            DependencyProperty.Register("YMin", typeof(double), typeof(KVisualHostBase),
                new FrameworkPropertyMetadata(-100D, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(YRangeChanged)));

        /// <summary>
        /// X轴最大值
        /// </summary>
        public double XMax
        {
            get { return (double)GetValue(XMaxProperty); }
            set { SetValue(XMaxProperty, value); }
        }
        public static readonly DependencyProperty XMaxProperty =
            DependencyProperty.Register("XMax", typeof(double), typeof(KVisualHostBase),
                new FrameworkPropertyMetadata(100D, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(XRangeChanged)));

        /// <summary>
        /// X轴最小值
        /// </summary>
        public double XMin
        {
            get { return (double)GetValue(XMinProperty); }
            set { SetValue(XMinProperty, value); }
        }
        public static readonly DependencyProperty XMinProperty =
            DependencyProperty.Register("XMin", typeof(double), typeof(KVisualHostBase),
                new FrameworkPropertyMetadata(-100D,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,new PropertyChangedCallback(XRangeChanged)));

        private static void YRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is KVisualHostBase g)
            {
                g.UpdatePixelRatio();
            }
        }

        private static void XRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is KVisualHostBase g)
            {
                g.UpdatePixelRatio();
            }
        }

        protected void Add(DrawingVisual drawingVisual) => _children.Add(drawingVisual);

        /// <summary>
        /// 转换实际坐标的Y点为图像坐标值
        /// </summary>
        /// <param name="actY"></param>
        /// <returns></returns>
        protected double YAxisConvertYToPixel(double actY)
        {
            var orgY = actY - YMin;
            var orgYToPX = RenderSize.Height - orgY * YPixelRatio;
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
            var orgX = actX - XMin;
            var orgXToPX = orgX * XPixelRatio;
            return orgXToPX;
        }

        protected double YAxisConvertToActual(double pixelY)
        {
            var cur = (RenderSize.Height - pixelY) / YPixelRatio;
            return cur + YMin;
        }

        protected double XAxisConvertToActual(double pixelX)
        {
            var cur = (pixelX) / YPixelRatio;
            return cur + XMin;
        }

        /// <summary>
        /// 坐标点转换
        /// </summary>
        /// <param name="actP"></param>
        /// <returns></returns>
        protected Point ConvertToPixel(Point actP)
        {
            var pt = new Point();
            pt.X = XAxisConvertXToPixel(actP.X);
            pt.Y = YAxisConvertYToPixel(actP.Y);
            return pt;
        }

        protected Point ConvertToActual(Point pixcel)
        {
            var pt = new Point();
            pt.X = XAxisConvertToActual(pixcel.X);
            pt.Y = YAxisConvertToActual(pixcel.Y);
            return pt;
        }

        /// <summary>
        /// 更新像素比
        /// </summary>
        protected void UpdatePixelRatio()
        {
            // 场景实际大小情况
            DrawingRegionWidth = XMax - XMin;
            DrawingRegionHeight = YMax - YMin;

            // 实际场景中1m对应多少像素点
            XPixelRatio = RenderSize.Width / DrawingRegionWidth;
            YPixelRatio = RenderSize.Height / DrawingRegionHeight;
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

        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Background, null, new Rect(0, 0, RenderSize.Width, RenderSize.Height));
            base.OnRender(drawingContext);
        }
    }
}
