using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChangePageTest.Dialog
{
    public interface IDialogResult
    {
        IDialogParameters Parameters { get; set; }
        DialogStatus Result { get; set; }
    }
}
