using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfCustomControlLibrary1
{
    public class KFloatingTextBox:TextBox
    {
        static KFloatingTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KFloatingTextBox), new FrameworkPropertyMetadata(typeof(KFloatingTextBox)));
        }



        public string FloatingText
        {
            get { return (string)GetValue(FloatingTextProperty); }
            set { SetValue(FloatingTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FloatingText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FloatingTextProperty =
            DependencyProperty.Register("FloatingText", typeof(string), typeof(KFloatingTextBox), new PropertyMetadata(default(string)));


    }
}
