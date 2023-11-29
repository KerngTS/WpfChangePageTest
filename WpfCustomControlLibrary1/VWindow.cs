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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCustomControlLibrary1
{
    /// <summary>
    /// 依照步驟 1a 或 1b 執行，然後執行步驟 2，以便在 XAML 檔中使用此自訂控制項。
    ///
    /// 步驟 1a) 於存在目前專案的 XAML 檔中使用此自訂控制項。
    /// 加入此 XmlNamespace 屬性至標記檔案的根項目為 
    ///要使用的: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary1"
    ///
    ///
    /// 步驟 1b) 於存在其他專案的 XAML 檔中使用此自訂控制項。
    /// 加入此 XmlNamespace 屬性至標記檔案的根項目為 
    ///要使用的: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary1;assembly=WpfCustomControlLibrary1"
    ///
    /// 您還必須將 XAML 檔所在專案的專案參考加入
    /// 此專案並重建，以免發生編譯錯誤: 
    ///
    ///     在 [方案總管] 中以滑鼠右鍵按一下目標專案，並按一下
    ///     [加入參考]->[專案]->[瀏覽並選取此專案]
    ///
    ///
    /// 步驟 2)
    /// 開始使用 XAML 檔案中的控制項。
    ///
    ///     <MyNamespace:VWindow/>
    ///
    /// </summary>
    public class VWindow : Window
    {
        static VWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VWindow), new FrameworkPropertyMetadata(typeof(VWindow)));
        }

        public VWindow()
        {
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, (s, e) => { SystemCommands.CloseWindow(this); }));
        }



        public SolidColorBrush TitleBackgroundBrush
        {
            get { return (SolidColorBrush)GetValue(TitleBackgroundBrushProperty); }
            set { SetValue(TitleBackgroundBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleBackgroundBrushProperty =
            DependencyProperty.Register("TitleBackgroundBrush", typeof(SolidColorBrush), typeof(VWindow), new PropertyMetadata(new SolidColorBrush()));



        public SolidColorBrush TitleForegroundBrush
        {
            get { return (SolidColorBrush)GetValue(TitleForegroundBrushProperty); }
            set { SetValue(TitleForegroundBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleForegroundBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleForegroundBrushProperty =
            DependencyProperty.Register("TitleForegroundBrush", typeof(SolidColorBrush), typeof(VWindow), new PropertyMetadata(new SolidColorBrush()));


    }
}
