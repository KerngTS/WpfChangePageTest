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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfCustomControlLibrary1;
using static System.Collections.Specialized.BitVector32;

namespace WpfChangePageTest
{
    /// <summary>
    /// Windows1.xaml 的互動邏輯
    /// </summary>
    public partial class Windows1 : VWindow
    {
        
        public Windows1()
        {
            InitializeComponent();
            DataContext = new WindowViewModel();
            var res = this.Resources;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var res = this.Resources;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            var res = this.Resources;
        }
    }

    public struct ColorValue
    {
        public static ColorValue ColorValaue()=>new ColorValue(70,70,70,255);   
        public ColorValue(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
            C=Color.FromArgb(A,R,G,B);
        }

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }
        public Color C { get; set; }
    }

    public class Cfg
    {
        public ColorValue BackgroundColor { get; private set; }
        public ColorValue TextColor { get; private set; }
        public ColorValue SbTextColor { get; private set; }
        public ColorValue UiLinesColor { get; private set; }
        public ColorValue SbBackgroundColor { get; private set; }
        public ColorValue TextHighlightedNonFocusedBackgroundColor { get; private set; }
        public ColorValue WidgetBackgroundColor { get; private set; }
        public ColorValue TabTextColor { get; private set; }
        public ColorValue TabTextColorWinSelector { get; private set; }
        public ColorValue TabHighlightedTextColor { get; private set; }
        public ColorValue TabHighlightedTextColorWinSelector { get; private set; }
        public ColorValue TabNormalColor { get; private set; }
        public ColorValue TabHighlightedColor { get; private set; }
        public ColorValue TabSelectedColor { get; private set; }
        public ColorValue DockbarCaptionColor { get; private set; }
        public ColorValue ProptreeButtonColor { get; private set; }
        public ColorValue BrushButtonHighlightColor { get; private set; }
        public ColorValue BrushButtonDownColor { get; private set; }
        public ColorValue PenButtonHighlightColor { get; private set; }
        public ColorValue PenButtonDownColor { get; private set; }

        public const string Key01BackgroundColor = "SCBBackgroundColor";
        public const string Key02TextColor = "SCBTextColor";
        public const string Key03SbTextColor = "SCBSBTextColor";
        public const string Key04UiLinesColor = "SCBUILineColor";
        public const string Key05SbBackgroundColor = "SCBSBBackgroundColor";
        public const string Key06TextHighlightedNonFocusedBackgroundColor = "SCBTextHighlightedNonFocusedBackground";
        public const string Key07WidgetBackgroundColor = "SCBWidgetBackground";
        public const string Key08TabTextColor = "SCBTabTextColor";
        public const string Key09TabTextColorWinSelector = "SCBTabTextColorWinSelector";
        public const string Key10TabHighlightedTextColor = "SCBTabHighlightedTextColor";
        public const string Key11TabHighlightedTextColorWinSelector = "SCBTabHighlightedTextColorWinSelector";
        public const string Key12TabNormalColor = "SCBTabNormColor";
        public const string Key13TabHighlightedColor = "SCBTabHighlightedColor";
        public const string Key14TabSelectedColor = "SCBTabSelectedColor";
        public const string Key15DockbarCaptionColor = "SCBDockbarCaptionColor";
        public const string Key16ProptreeButtonColor = "SCBProptreeButtonColor";
        public const string Key17BrushButtonHighlightColor = "SCBBurshButtonHighlightColor";
        public const string Key18BrushButtonDownColor = "SCBBurshButtonDownColor";
        public const string Key19PenButtonHighlightColor = "SCBPenButtonHighlightColor";
        public const string Key20PenButtonDownColor = "SCBPenButtonDownColor";
        public ColorValue CV { get;private set; }
        public Cfg()
        {
            CV= new ColorValue(70,70,70,255);
            GetValues();
        }

        private void GetValues()
        {
            BackgroundColor = GetColorValue();

            TextColor = GetColorValue();

            SbTextColor = GetColorValue();

            UiLinesColor = GetColorValue();

            SbBackgroundColor = GetColorValue();

            TextHighlightedNonFocusedBackgroundColor = GetColorValue();

            WidgetBackgroundColor = GetColorValue();

            TabTextColor = GetColorValue();

            TabTextColorWinSelector = GetColorValue();

            TabHighlightedTextColor = GetColorValue();

            TabHighlightedTextColorWinSelector = GetColorValue();

            TabNormalColor = GetColorValue();

            TabHighlightedColor = GetColorValue();

            TabSelectedColor = GetColorValue();

            DockbarCaptionColor = GetColorValue();

            ProptreeButtonColor = GetColorValue();

            BrushButtonHighlightColor = GetColorValue();

            BrushButtonDownColor = GetColorValue();

            PenButtonHighlightColor = GetColorValue();

            PenButtonDownColor = GetColorValue();

        }

        private ColorValue GetColorValue( )
        {

            var r = GetbyteStr();
            var g = GetbyteStr();
            var b = GetbyteStr();
            var a = GetbyteStr();
            return GetColorValue(r, g, b, "255");
        }


        private ColorValue GetColorValue(string red, string green, string blue, string alpha)
        {
            var r = byte.Parse(red);
            var g = byte.Parse(green);
            var b = byte.Parse(blue);
            var a = byte.Parse(alpha);
            if (a < 0) a = 255;
            return new ColorValue(r, g, b, a);
        }
        Random rd = new Random();
        private string GetbyteStr()
        {
            var v = rd.Next(int.MaxValue);
            var mod = v % 256;
            return mod.ToString();
        }
    }
}
