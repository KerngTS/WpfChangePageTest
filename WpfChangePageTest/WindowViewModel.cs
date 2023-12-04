using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using WpfChangePageTest.Mvvm;
using WpfChangePageTest.Views;

namespace WpfChangePageTest
{
    internal class WindowViewModel:KNotifyPropertyBase
    {
        private string _curPageName = "NONE";
        public string CurPageName
        {
            get { return _curPageName; }
            set { _curPageName = value; RaisePropertyChange(); }
        }

        private UserControl _currentPage;
        public UserControl CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; RaisePropertyChange(); }
        }

        private List<ColorValue> colorValues;
        public List<ColorValue> ColorValues
        {
            get { return colorValues; }
            set { colorValues = value;RaisePropertyChange(); }
        }

        public KDelegateCommand Page01Command { get;private set; }
        public KDelegateCommand Page02Command { get;private set; }

        public WindowViewModel()
        {
            Page01Command = new KDelegateCommand(ToPage01);
            Page02Command = new KDelegateCommand(ToPage02);
            ColorValues = new List<ColorValue>();
            var cfg = new Cfg();
            ColorValues.Add(cfg.BackgroundColor);
            ColorValues.Add(cfg.TextColor);
            ColorValues.Add(cfg.SbTextColor);
            ColorValues.Add(cfg.UiLinesColor);
            ColorValues.Add(cfg.SbBackgroundColor);
        }

        private void ToPage01(object obj)
        {
            //IntPtr hwnd = new WindowInteropHelper(this).Handle; //this就是要获取句柄的窗体的类名；
            //IntPtr hwnd = ((HwndSource)PresentationSource.FromVisual(page)).Handle;//uielement就是要获取句柄的控件，该控
            var type = typeof(Page01);
            if(!pages.ContainsKey(type))
            {
                var page = new Page01();
                
                pages.Add(type, page);
            }
            CurrentPage = pages[type];
            CurPageName = "PAGE_01";
        }

        private void ToPage02(object obj)
        {
            var type = typeof(Page02);
            if (!pages.ContainsKey(type))
            {
                var page = new Page02();
                pages.Add(type, page);
            }
            CurrentPage = pages[type];
            CurPageName = "PAGE_02";
        }

        private Dictionary<Type,UserControl> pages = new Dictionary<Type,UserControl>();
    }
}
