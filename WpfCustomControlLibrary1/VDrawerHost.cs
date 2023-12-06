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
    ///     <MyNamespace:VDrawerHost/>
    ///
    /// </summary>
    [TemplatePart(Name = DrawerMaskName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = LeftDrawerName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = TopDrawerName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = RightDrawerName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = BottomDrawerName, Type = typeof(FrameworkElement))]
    [TemplateVisualState(GroupName = LeftDrawerGroupName, Name = LeftOpenStateName)]
    [TemplateVisualState(GroupName = LeftDrawerGroupName, Name = LeftClosedStateName)]
    [TemplateVisualState(GroupName = RightDrawerGroupName, Name = RightOpenStateName)]
    [TemplateVisualState(GroupName = RightDrawerGroupName, Name = RightClosedStateName)]
    [TemplateVisualState(GroupName = TopDrawerGroupName, Name = TopOpenStateName)]
    [TemplateVisualState(GroupName = TopDrawerGroupName, Name = TopClosedStateName)]
    [TemplateVisualState(GroupName = BottomDrawerGroupName, Name = BottomOpenStateName)]
    [TemplateVisualState(GroupName = BottomDrawerGroupName, Name = BottomClosedStateName)]
    public class VDrawerHost : Control
    {
        #region 名稱常量
        public const string LeftDrawerGroupName = "LeftDrawerViewStates";
        public const string RightDrawerGroupName = "RightDrawerViewStates";
        public const string TopDrawerGroupName = "TopDrawerViewStates";
        public const string BottomDrawerGroupName = "BottomDrawerViewStates";

        public const string LeftOpenStateName = "LeftDrawerOpen";
        public const string LeftClosedStateName = "LeftDrawerClosed";
        public const string RightOpenStateName = "RightDrawerOpen";
        public const string RightClosedStateName = "RightDrawerClosed";
        public const string TopOpenStateName = "TopDrawerOpen";
        public const string TopClosedStateName = "TopDrawerClosed";
        public const string BottomOpenStateName = "BottomDrawerOpen";
        public const string BottomClosedStateName = "BottomDrawerClosed";

        public const string DrawerMaskName = "PART_DrawerMask";
        public const string LeftDrawerName = "PART_LeftDrawer";
        public const string TopDrawerName = "PART_TopDrawer";
        public const string RightDrawerName = "PART_RightDrawer";
        public const string BottomDrawerName = "PART_BottomDrawer";

        public static readonly RoutedCommand OpenDrawerCommand = new RoutedCommand();
        public static readonly RoutedCommand CloseDrawerCommand = new RoutedCommand();

        private FrameworkElement _drawerMaskElement;
        private FrameworkElement _leftDrawerElement;
        private FrameworkElement _topDrawerElement;
        private FrameworkElement _rightDrawerElement;
        private FrameworkElement _bottomDrawerElement;

        private bool _lockZIndexes;

        #endregion
        static VDrawerHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VDrawerHost), new FrameworkPropertyMetadata(typeof(VDrawerHost)));
        }

        #region 內容區


        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(VDrawerHost), new PropertyMetadata(default(object)));


        #endregion

        #region 抽屜


        public object LeftDrawer
        {
            get { return (object)GetValue(LeftDrawerProperty); }
            set { SetValue(LeftDrawerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftDrawer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftDrawerProperty =
            DependencyProperty.Register("LeftDrawer", typeof(object), typeof(VDrawerHost), new PropertyMetadata(default(object)));



        public object RightDrawer
        {
            get { return (object)GetValue(RightDrawerProperty); }
            set { SetValue(RightDrawerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightDrawer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightDrawerProperty =
            DependencyProperty.Register("RightDrawer", typeof(object), typeof(VDrawerHost), new PropertyMetadata(default(object)));



        public object TopDrawer
        {
            get { return (object)GetValue(TopDrawerProperty); }
            set { SetValue(TopDrawerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopDrawer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopDrawerProperty =
            DependencyProperty.Register("TopDrawer", typeof(object), typeof(VDrawerHost), new PropertyMetadata(default(object)));



        public object BottomDrawer
        {
            get { return (object)GetValue(BottomDrawerProperty); }
            set { SetValue(BottomDrawerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomDrawer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomDrawerProperty =
            DependencyProperty.Register("BottomDrawer", typeof(object), typeof(VDrawerHost), new PropertyMetadata(default(object)));


        #endregion

        #region 打開狀態


        public bool IsLeftDrawerOpen
        {
            get { return (bool)GetValue(IsLeftDrawerOpenProperty); }
            set { SetValue(IsLeftDrawerOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsLeftDrawerOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLeftDrawerOpenProperty =
            DependencyProperty.Register("IsLeftDrawerOpen", typeof(bool), typeof(VDrawerHost), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OpenPropertyChangedCallback)));

        private static void OpenPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = d as VDrawerHost;
            if (self!=null)
            {
                self.UpdateVisualStates(false);
            }
        }

        public bool IsRightDrawerOpen
        {
            get { return (bool)GetValue(IsRightDrawerOpenProperty); }
            set { SetValue(IsRightDrawerOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsRightDrawerOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRightDrawerOpenProperty =
            DependencyProperty.Register("IsRightDrawerOpen", typeof(bool), typeof(VDrawerHost), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OpenPropertyChangedCallback)));



        public bool IsTopDrawerOpen
        {
            get { return (bool)GetValue(IsTopDrawerOpenProperty); }
            set { SetValue(IsTopDrawerOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTopDrawerOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTopDrawerOpenProperty =
            DependencyProperty.Register("IsTopDrawerOpen", typeof(bool), typeof(VDrawerHost), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OpenPropertyChangedCallback)));



        public bool IsBottomDrawerOpen
        {
            get { return (bool)GetValue(IsBottomDrawerOpenProperty); }
            set { SetValue(IsBottomDrawerOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsBottomDrawerOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBottomDrawerOpenProperty =
            DependencyProperty.Register("IsBottomDrawerOpen", typeof(bool), typeof(VDrawerHost), new FrameworkPropertyMetadata(false,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OpenPropertyChangedCallback)));


        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _drawerMaskElement = GetTemplateChild(DrawerMaskName) as FrameworkElement;
            if (_drawerMaskElement != null)
                _drawerMaskElement.PreviewMouseLeftButtonUp += MaskElementOnPreviewMouseLeftButtonUp;
            _leftDrawerElement = GetTemplateChild(LeftDrawerName) as FrameworkElement;
            _topDrawerElement = GetTemplateChild(TopDrawerName) as FrameworkElement;
            _rightDrawerElement = GetTemplateChild(RightDrawerName) as FrameworkElement;
            _bottomDrawerElement = GetTemplateChild(BottomDrawerName) as FrameworkElement;

            UpdateVisualStates(false);
        }

        

        

        private void MaskElementOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            IsLeftDrawerOpen=false; IsRightDrawerOpen=false;IsTopDrawerOpen=false;IsBottomDrawerOpen=false;
            UpdateVisualStates(false);
        }

        private void UpdateVisualStates(bool useTransitions )
        {
            VisualStateManager.GoToState(this,IsLeftDrawerOpen&&_leftDrawerElement!=null ? LeftOpenStateName : LeftClosedStateName, useTransitions);
            VisualStateManager.GoToState(this,IsRightDrawerOpen&&_rightDrawerElement!=null ? RightOpenStateName : RightClosedStateName, useTransitions);
            VisualStateManager.GoToState(this,IsTopDrawerOpen&&_topDrawerElement!=null ? TopOpenStateName : TopClosedStateName, useTransitions);
            VisualStateManager.GoToState(this,IsBottomDrawerOpen&&_bottomDrawerElement!=null ? BottomOpenStateName : BottomClosedStateName, useTransitions);
        }
    }
}
