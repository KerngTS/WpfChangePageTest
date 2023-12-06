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
using WpfChangePageTest.ViewModels;

namespace WpfChangePageTest.Views
{
    /// <summary>
    /// TestDialogView.xaml 的互動邏輯
    /// </summary>
    public partial class TestDialogView : Window
    {
        public TestDialogView()
        {
            InitializeComponent();
            this.DataContext = new TestDialogViewModel();
        }
    }
}
