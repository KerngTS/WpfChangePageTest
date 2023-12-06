using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfCustomControlLibrary1
{
    public class VDialogHost:ContentControl
    {
        static VDialogHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VDialogHost), new FrameworkPropertyMetadata(typeof(VDialogHost)));
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius), typeof(CornerRadius), typeof(VDialogHost), new PropertyMetadata(default(CornerRadius)));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            nameof(IsOpen), typeof(bool), typeof(VDialogHost), new FrameworkPropertyMetadata(false,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback( IsOpenPropertyChangedCallback)));

        private static void IsOpenPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dialogHost = (VDialogHost)d;
            if(dialogHost.Content == null) { return; }
        }

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }
    }
}
