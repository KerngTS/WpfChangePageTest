using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDoubleAnimation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //AddDoubleAnimation();
        }

        //代碼的方式實現動畫
        private Storyboard myStoryboard;

        private void AddDoubleAnimation()
        {
            StackPanel myPanel = new StackPanel();
            myPanel.Margin=new Thickness(10);

            Rectangle myRectangle = new Rectangle();
            myRectangle.Name=nameof(myRectangle);
            this.RegisterName(myRectangle.Name,myRectangle);
            myRectangle.Width = 200;
            myRectangle.Height = 100;
            myRectangle.Fill = Brushes.Green;

            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 1.0;
            myDoubleAnimation.To = 0.0;
            myDoubleAnimation.Duration=new Duration(TimeSpan.FromSeconds(5));
            myDoubleAnimation.AutoReverse = true;
            myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            myStoryboard=new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTargetName(myDoubleAnimation, myRectangle.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Rectangle.OpacityProperty));

            myRectangle.Loaded += (s, e) =>
            {
                myStoryboard.Begin(this);
            };
            myPanel.Children.Add(myRectangle);
            this.Content= myPanel;
        }
    }
}
