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
            CommandBindings.Add(new CommandBinding(OpenDialogCommand, OpenDialogHandler));
            CommandBindings.Add(new CommandBinding(CloseDialogCommand, CloseDialogHandler, CloseDialogCanExecute));
        }

        #region MD DialogHost

        
        private void CloseDialogCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            
        }

        private void CloseDialogHandler(object sender, ExecutedRoutedEventArgs e)
        {
           
        }

        private void OpenDialogHandler(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        public static readonly RoutedEvent DialogOpenedEvent =
            EventManager.RegisterRoutedEvent(
                "DialogOpened",
                RoutingStrategy.Bubble,
                typeof(DialogOpenedEventHandler),
                typeof(VWindow));

        
        public event DialogOpenedEventHandler DialogOpened
        {
            add { AddHandler(DialogOpenedEvent, value); }
            remove { RemoveHandler(DialogOpenedEvent, value); }
        }

        public static readonly DependencyProperty DialogOpenedCallbackProperty = DependencyProperty.Register(
            nameof(DialogOpenedCallback), typeof(DialogOpenedEventHandler), typeof(VWindow), new PropertyMetadata(default(DialogOpenedEventHandler)));

        public DialogOpenedEventHandler DialogOpenedCallback
        {
            get => (DialogOpenedEventHandler)GetValue(DialogOpenedCallbackProperty);
            set => SetValue(DialogOpenedCallbackProperty, value);
        }

        protected void OnDialogOpened(DialogOpenedEventArgs eventArgs)
            => RaiseEvent(eventArgs);

        public static readonly RoutedEvent DialogClosingEvent =
            EventManager.RegisterRoutedEvent(
                "DialogClosing",
                RoutingStrategy.Bubble,
                typeof(DialogClosingEventHandler),
                typeof(VWindow));

        public event DialogClosingEventHandler DialogClosing
        {
            add { AddHandler(DialogClosingEvent, value); }
            remove { RemoveHandler(DialogClosingEvent, value); }
        }

        public static readonly DependencyProperty DialogClosingCallbackProperty = DependencyProperty.Register(
            nameof(DialogClosingCallback), typeof(DialogClosingEventHandler), typeof(VWindow), new PropertyMetadata(default(DialogClosingEventHandler)));

        public DialogClosingEventHandler DialogClosingCallback
        {
            get => (DialogClosingEventHandler)GetValue(DialogClosingCallbackProperty);
            set => SetValue(DialogClosingCallbackProperty, value);
        }

        protected void OnDialogClosing(DialogClosingEventArgs eventArgs)
            => RaiseEvent(eventArgs);

        public static readonly RoutedEvent DialogClosedEvent =
            EventManager.RegisterRoutedEvent(
                "DialogClosed",
                RoutingStrategy.Bubble,
                typeof(DialogClosedEventHandler),
                typeof(VWindow));

        public event DialogClosedEventHandler DialogClosed
        {
            add { AddHandler(DialogClosedEvent, value); }
            remove { RemoveHandler(DialogClosedEvent, value); }
        }

        public static readonly DependencyProperty DialogClosedCallbackProperty = DependencyProperty.Register(
            nameof(DialogClosedCallback), typeof(DialogClosedEventHandler), typeof(VWindow), new PropertyMetadata(default(DialogClosedEventHandler)));

        public DialogClosedEventHandler DialogClosedCallback
        {
            get => (DialogClosedEventHandler)GetValue(DialogClosedCallbackProperty);
            set => SetValue(DialogClosedCallbackProperty, value);
        }

        protected void OnDialogClosed(DialogClosedEventArgs eventArgs)
            => RaiseEvent(eventArgs);

        public static readonly RoutedCommand OpenDialogCommand = new RoutedCommand();
        public static readonly RoutedCommand CloseDialogCommand = new RoutedCommand();

        #endregion

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


    public delegate void DialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs);
    public delegate void DialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs);
    public delegate void DialogClosedEventHandler(object sender, DialogClosedEventArgs eventArgs);

    public class DialogOpenedEventArgs : RoutedEventArgs
    {
        public DialogOpenedEventArgs(RoutedEvent routedEvent)
        {

            RoutedEvent = routedEvent;
        }

    }

    public class DialogClosedEventArgs : RoutedEventArgs
    {
        public DialogClosedEventArgs(object closeParameter, RoutedEvent routedEvent)
            : base(routedEvent)
        {
            Parameter = closeParameter;
        }

        public object Parameter { get; private set; }

    }

    public class DialogClosingEventArgs : RoutedEventArgs
    {
        public DialogClosingEventArgs(object closeParameter, RoutedEvent routedEvent)
            : base(routedEvent)
        {
            Parameter=closeParameter;
        }

        public void Cancel() => IsCancelled = true;

        public bool IsCancelled { get; private set; }

        public object Parameter { get; private set; }

    }

}
