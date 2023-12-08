using KEventAggregator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfChangePageTest;
using WpfCustomControlLibrary1;
using WpfCustomControlLibrary1.Styles;

namespace Test
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            KEventAggregator.KEventAggregator1.Instance.Subscribe<TestEvent>(arg => Console.WriteLine($"HasFilter:{DateTime.Now:yyyy-MM-dd hh:mm:ss} {arg.Message}"),f=>f.Filter=="ROOT") ;
            var token = KEventAggregator.KEventAggregator1.Instance.Subscribe<TestEvent>(arg => Console.WriteLine($"NoFilter:{DateTime.Now:yyyy-MM-dd hh:mm:ss} {arg.Message}")) ;
            KEventAggregator.KEventAggregator1.Instance.UnSubscribe<TestEvent>(token);
            var win = new Windows1();
            //var cfg = new Cfg();
            //var r = win.Resources;
            //win.Resources[Cfg.Key01BackgroundColor] = new SolidColorBrush(cfg.BackgroundColor.C);
            //win.Resources[Cfg.Key02TextColor] = new SolidColorBrush(cfg.TextColor.C);
            //win.Resources[Cfg.Key03SbTextColor] = new SolidColorBrush(cfg.SbTextColor.C);
            //win.Resources[Cfg.Key04UiLinesColor] = new SolidColorBrush(cfg.UiLinesColor.C);
            //win.Resources[Cfg.Key05SbBackgroundColor] = new SolidColorBrush(cfg.SbBackgroundColor.C);
            //win.Resources[Cfg.Key06TextHighlightedNonFocusedBackgroundColor] = new SolidColorBrush(cfg.TextHighlightedNonFocusedBackgroundColor.C);
            //win.Resources[Cfg.Key07WidgetBackgroundColor] = new SolidColorBrush(cfg.WidgetBackgroundColor.C);
            //win.Resources[Cfg.Key08TabTextColor] = new SolidColorBrush(cfg.TabTextColor.C);
            //win.Resources[Cfg.Key09TabTextColorWinSelector] = new SolidColorBrush(cfg.TabTextColorWinSelector.C);
            //win.Resources[Cfg.Key10TabHighlightedTextColor] = new SolidColorBrush(cfg.TabHighlightedTextColor.C);
            //win.Resources[Cfg.Key11TabHighlightedTextColorWinSelector] = new SolidColorBrush(cfg.TabHighlightedTextColorWinSelector.C);
            //win.Resources[Cfg.Key12TabNormalColor] = new SolidColorBrush(cfg.TabNormalColor.C);
            //win.Resources[Cfg.Key13TabHighlightedColor] = new SolidColorBrush(cfg.TabHighlightedColor.C);
            //win.Resources[Cfg.Key14TabSelectedColor] = new SolidColorBrush(cfg.TabSelectedColor.C);
            //win.Resources[Cfg.Key15DockbarCaptionColor] = new SolidColorBrush(cfg.DockbarCaptionColor.C);
            //win.Resources[Cfg.Key16ProptreeButtonColor] = new SolidColorBrush(cfg.ProptreeButtonColor.C);
            //win.Resources[Cfg.Key17BrushButtonHighlightColor] = new SolidColorBrush(cfg.BrushButtonHighlightColor.C);
            //win.Resources[Cfg.Key18BrushButtonDownColor] = new SolidColorBrush(cfg.BrushButtonDownColor.C);
            //win.Resources[Cfg.Key19PenButtonHighlightColor] = new SolidColorBrush(cfg.PenButtonHighlightColor.C);
            //win.Resources[Cfg.Key20PenButtonDownColor] = new SolidColorBrush(cfg.PenButtonDownColor.C);
            win.ShowDialog();
            //Console.ReadKey();
        }
    }
}
