using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCustomControlLibrary1
{
    public class VDialog : VWindow
    {
        static VDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VWindow), new FrameworkPropertyMetadata(typeof(VWindow)));
        }
    }
}
