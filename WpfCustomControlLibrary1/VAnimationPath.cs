using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WpfCustomControlLibrary1
{
    public class VAnimationPath : Shape
    {
        private Storyboard _storyboard;

        private double _pathLength;

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data),
            typeof(Geometry), typeof(VAnimationPath), new FrameworkPropertyMetadata(null,
                OnPropertiesChanged));

        private static void OnPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is VAnimationPath path)
            {
                path.UpdatePath();
            }
        }

        public Geometry Data
        {
            get => (Geometry)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        protected override Geometry DefiningGeometry => Data ?? Geometry.Empty;

        public static readonly DependencyProperty PathLengthProperty = DependencyProperty.Register(
            nameof(PathLength), typeof(double), typeof(VAnimationPath), new FrameworkPropertyMetadata(0.0, OnPropertiesChanged));

        public double PathLength
        {
            get => (double)GetValue(PathLengthProperty);
            set => SetValue(PathLengthProperty, value);
        }

        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(
            nameof(Duration), typeof(Duration), typeof(VAnimationPath), new FrameworkPropertyMetadata(new Duration(TimeSpan.FromSeconds(2)),
                OnPropertiesChanged));

        public Duration Duration
        {
            get => (Duration)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register(
            nameof(IsPlaying), typeof(bool), typeof(VAnimationPath), new FrameworkPropertyMetadata(true, (o, args) =>
            {
                var ctl = (VAnimationPath)o;
                var v = (bool)args.NewValue;
                if (v)
                {
                    ctl.UpdatePath();
                }
                else
                {
                    ctl._storyboard?.Pause();
                }
            }));

        public bool IsPlaying
        {
            get => (bool)GetValue(IsPlayingProperty);
            set => SetValue(IsPlayingProperty, value);
        }

        public static readonly DependencyProperty RepeatBehaviorProperty =
            Timeline.RepeatBehaviorProperty.AddOwner(typeof(VAnimationPath),
                new PropertyMetadata(RepeatBehavior.Forever));

        public RepeatBehavior RepeatBehavior
        {
            get => (RepeatBehavior)GetValue(RepeatBehaviorProperty);
            set => SetValue(RepeatBehaviorProperty, value);
        }

        public static readonly DependencyProperty FillBehaviorProperty =
            Timeline.FillBehaviorProperty.AddOwner(typeof(VAnimationPath), new PropertyMetadata(FillBehavior.Stop));

        public FillBehavior FillBehavior
        {
            get { return (FillBehavior)GetValue(FillBehaviorProperty); }
            set { SetValue(FillBehaviorProperty, value); }
        }

        static VAnimationPath()
        {
            StretchProperty.AddOwner(typeof(VAnimationPath), new FrameworkPropertyMetadata(Stretch.Uniform,
                OnPropertiesChanged));

            StrokeThicknessProperty.AddOwner(typeof(VAnimationPath), new FrameworkPropertyMetadata(1.0,
                OnPropertiesChanged));
        }

        public VAnimationPath() => Loaded += (s, e) => UpdatePath();

        public static readonly RoutedEvent CompletedEvent =
            EventManager.RegisterRoutedEvent("Completed", RoutingStrategy.Bubble,
                typeof(EventHandler), typeof(VAnimationPath));

        public event EventHandler Completed
        {
            add => AddHandler(CompletedEvent, value);
            remove => RemoveHandler(CompletedEvent, value);
        }

        private void UpdatePath()
        {
            if (!Duration.HasTimeSpan || !IsPlaying) return;

            _pathLength = PathLength > 0 ? PathLength : GetTotalLength(Data,new Size(ActualWidth, ActualHeight), StrokeThickness);

            if (Math.Abs(_pathLength) < 1E-06) return;

            StrokeDashOffset = _pathLength;
            StrokeDashArray = new DoubleCollection(new List<double>
        {
            _pathLength,
            _pathLength
        });

            if (_storyboard != null)
            {
                _storyboard.Stop();
                _storyboard.Completed -= Storyboard_Completed;
            }
            _storyboard = new Storyboard
            {
                RepeatBehavior = RepeatBehavior,
                FillBehavior = FillBehavior
            };
            _storyboard.Completed += Storyboard_Completed;

            var frames = new DoubleAnimationUsingKeyFrames();

            var frameIn = new LinearDoubleKeyFrame
            {
                Value = _pathLength,
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero)
            };
            frames.KeyFrames.Add(frameIn);

            var frameOut = new LinearDoubleKeyFrame
            {
                Value = FillBehavior == FillBehavior.Stop ? -_pathLength : 0,
                KeyTime = KeyTime.FromTimeSpan(Duration.TimeSpan)
            };
            frames.KeyFrames.Add(frameOut);

            Storyboard.SetTarget(frames, this);
            Storyboard.SetTargetProperty(frames, new PropertyPath(StrokeDashOffsetProperty));
            _storyboard.Children.Add(frames);

            _storyboard.Begin();
        }

        private void Storyboard_Completed(object sender, EventArgs e) => RaiseEvent(new RoutedEventArgs(CompletedEvent));

        /// <summary>
        ///     获取路径总长度
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static double GetTotalLength(Geometry geometry)
        {
            if (geometry == null) return 0;

            var pathGeometry = PathGeometry.CreateFromGeometry(geometry);
            pathGeometry.GetPointAtFractionLength(1e-4, out var point, out _);
            var length = (pathGeometry.Figures[0].StartPoint - point).Length * 1e+4;

            return length;
        }

        /// <summary>
        ///     获取路径总长度
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="size"></param>
        /// <param name="strokeThickness"></param>
        /// <returns></returns>
        public static double GetTotalLength( Geometry geometry, Size size, double strokeThickness = 1)
        {
            if (geometry == null) return 0;

            if (IsVerySmall(size.Width) || IsVerySmall(size.Height)) return 0;

            var length = GetTotalLength(geometry);
            var sw = geometry.Bounds.Width / size.Width;
            var sh = geometry.Bounds.Height / size.Height;
            var min = Math.Min(sw, sh);

            if (IsVerySmall(min) || IsVerySmall(strokeThickness)) return 0;

            length /= min;
            return length / strokeThickness;
        }

        private static bool IsVerySmall(double value)
        {
            return Math.Abs(value) < 1E-06;
        }
    }
}
