using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfChangePageTest;

namespace Test
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var win = new Windows1();
            win.ShowDialog();
            //Console.ReadKey();
        }
    }
}
