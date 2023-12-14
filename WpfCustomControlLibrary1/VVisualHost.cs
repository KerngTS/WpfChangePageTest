using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfCustomControlLibrary1
{
    public class VVisualHost : KVisualHostBase
    {
        //創建一個容器
        //private VisualCollection _children;
        DrawingVisual RecVisual;
        public VVisualHost()
        {
            //_children = new VisualCollection(this);
            RecVisual = new DrawingVisual();
            //_children.Add(CreateDrawingVisualRectangle());
            Add(RecVisual);
            //_children.Add(CreateDrawingVisualEllipses());
            //_children.Add(CreateDrawingVisualPath());

            this.MouseLeftButtonUp += VVisualHost_MouseLeftButtonUp;

            //System.Collections.Generic.ICollection<Typeface> typefaces = Fonts.GetTypefaces("file:///C:\\Windows\\Fonts\\#Georgia");

            //// Enumerate the typefaces in the collection.
            //foreach (Typeface face in typefaces)
            //{
            //    // Separate the URI directory source info from the font family name.
            //    string[] familyName = face.FontFamily.Source.Split('#');

            //    // Add the font family name, weight, and style values to the typeface combo box.
            //    var rr = familyName[familyName.Length - 1] + " " + face.Weight + " " + face.Style;
            //}
        }



        private void VVisualHost_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition((UIElement)sender);
            VisualTreeHelper.HitTest(this, null, new HitTestResultCallback(MyCallBack), new PointHitTestParameters(pt));
        }

        private HitTestResultBehavior MyCallBack(HitTestResult result)
        {
            if (result.VisualHit.GetType() == typeof(DrawingVisual))
            {
                if (((DrawingVisual)result.VisualHit).Opacity == 1.0)
                {
                    ((DrawingVisual)result.VisualHit).Opacity = 0.4;
                }
                else
                {
                    ((DrawingVisual)result.VisualHit).Opacity = 1.0;
                }
            }
            return HitTestResultBehavior.Stop;
        }

        private DrawingVisual CreateDrawingVisualEllipses()
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            drawingContext.DrawEllipse(Brushes.SkyBlue, null, new Point(320, 140), 160, 40);
            drawingContext.Close();
            return drawingVisual;
        }

        private DrawingVisual CreateDrawingVisualPath()
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            //PathFigure pathFigure = new PathFigure();

            PathGeometry myPathGeometry = new PathGeometry();

            // Create a figure.
            PathFigure pathFigure1 = new PathFigure();
            //pathFigure1.StartPoint = new Point(105, 100);
            //PolyLineSegment myPolyLineSegment=new PolyLineSegment(new List<Point>()
            //{
            //    /*new Point(105,100),*/new Point(295,100),new Point(300,105),new Point(300,105),new Point(300,195),new Point(295,200),new Point(105,200),new Point(100,195),new Point(100,105),new Point(105,100)
            //},true);

            //pathFigure1.Segments.Add(myPolyLineSegment);

            pathFigure1.StartPoint = ConvertToPixel(new Point(100, 105));
            pathFigure1.Segments.Add(
            new ArcSegment(
                ConvertToPixel(new Point(105, 100)),
                new Size(5, 5),
                0,
                false, /* IsLargeArc */
                SweepDirection.Clockwise,
                true /* IsStroked */ ));
            pathFigure1.Segments.Add(new LineSegment(ConvertToPixel(new Point(295, 100)), true));

            pathFigure1.Segments.Add(
            new ArcSegment(
                ConvertToPixel(new Point(300, 105)),
                new Size(5, 5),
                0,
                false, /* IsLargeArc */
                SweepDirection.Clockwise,
                true /* IsStroked */ ));
            pathFigure1.Segments.Add(new LineSegment(ConvertToPixel(new Point(300, 195)), true));

            pathFigure1.Segments.Add(
            new ArcSegment(
                ConvertToPixel(new Point(295, 200)),
                new Size(5, 5),
                0,
                false, /* IsLargeArc */
                SweepDirection.Clockwise,
                true /* IsStroked */ ));
            pathFigure1.Segments.Add(new LineSegment(ConvertToPixel(new Point(105, 200)), true));

            pathFigure1.Segments.Add(
            new ArcSegment(
               ConvertToPixel(new Point(100, 195)),
                new Size(5, 5),
                0,
                false, /* IsLargeArc */
                SweepDirection.Clockwise,
                true /* IsStroked */ ));
            pathFigure1.Segments.Add(new LineSegment(ConvertToPixel( new Point(100, 105)), true));

            myPathGeometry.Figures.Add(pathFigure1);

            //pathFigure.StartPoint = new Point(10, 100);
            //PointCollection myPointCollection = new PointCollection(6);
            //myPointCollection.Add(new Point(0, 0));
            //myPointCollection.Add(new Point(200, 0));
            //myPointCollection.Add(new Point(300, 100));
            //myPointCollection.Add(new Point(300, 0));
            //myPointCollection.Add(new Point(400, 0));
            //myPointCollection.Add(new Point(600, 100));

            //PolyQuadraticBezierSegment myBezierSegment = new PolyQuadraticBezierSegment();
            //PolyBezierSegment myBezierSegment = new PolyBezierSegment();
            //myBezierSegment.Points = myPointCollection;
            //pathFigure1.Segments.Add(myBezierSegment);
            //pathFigure1.Segments.Add(arcSegment1);
            //myPathGeometry.Figures.Add(pathFigure1);

            // 使用 DrawGeometry 方法绘制定义的复杂路径
            drawingContext.DrawGeometry(Brushes.Green, new Pen(Brushes.Red, 1.0), myPathGeometry);

            drawingContext.Close();

            return drawingVisual;
        }

        private DrawingVisual CreateDrawingVisualPath1()
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            PathGeometry myPathGeometry = new PathGeometry();

            PathFigure pathFigure1 = new PathFigure();
            pathFigure1.StartPoint = new Point(0, 0);
            ArcSegment arcSegment1 = new ArcSegment(new Point(100, 100), new Size(50, 25), 0, false, SweepDirection.Clockwise, false);
            pathFigure1.Segments.Add(arcSegment1);
            myPathGeometry.Figures.Add(pathFigure1);

            // 使用 DrawGeometry 方法绘制定义的复杂路径
            drawingContext.DrawGeometry(null, new Pen(Brushes.Red, 1.0), myPathGeometry);

            drawingContext.Close();

            return drawingVisual;
        }

        private DrawingVisual CreateDrawingVisualText()
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            var pt = new Point(0, 100);
            var text = new FormattedText("冲床钢", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), 50, Brushes.SeaGreen, VisualTreeHelper.GetDpi(this).PixelsPerDip);
            var geo = text.BuildGeometry(pt);
            var pathGeo = geo.GetOutlinedPathGeometry();
            pathGeo.FillRule = FillRule.EvenOdd;
            drawingContext.DrawGeometry(Brushes.Green, new Pen(Brushes.Red, 1), geo);
            //drawingContext.DrawText(text, pt);
            drawingContext.Close();
            return drawingVisual;
        }

        private DrawingVisual CreateDrawingVisualRectangle()
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            Rect rect = new Rect(ConvertToPixel(new Point(100, 100)), new Size(100, 80));
            drawingContext.DrawRectangle(Brushes.Red, null, rect);
            drawingContext.Close();
            return drawingVisual;
        }

        //protected override int VisualChildrenCount => _children.Count;

        //protected override Visual GetVisualChild(int index)
        //{
        //    if (index < 0 || index >= _children.Count)
        //        throw new ArgumentOutOfRangeException("index");
        //    return _children[index];
        //}

        private void Draw()
        {
            this.Dispatcher.Invoke(() =>
            {
                var dc = RecVisual.RenderOpen();
                var p1 = ConvertToPixel(new Point(-25, -25));
                var p2 = ConvertToPixel(new Point(25, 25));
                var w  = p2.X - p1.X;
                var h = p2.Y - p1.Y;

                Rect rect = new Rect(p1, p2);
                dc.DrawRectangle(Brushes.Red, null, rect);
                dc.Close();
                // 显示图形
                InvalidateVisual();
            });
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            Draw();
        }
    }
}
