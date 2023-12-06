using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChangePageTest.Dialog
{
    public class DialogResult : IDialogResult
    {
        public IDialogParameters Parameters { get;  set; }

        public DialogStatus Result {get; set;}=DialogStatus.None;

        public DialogResult()
        {
            
        }

        public DialogResult(DialogStatus result)
        {
            Result = result;
        }

        public DialogResult(DialogStatus result, IDialogParameters parameters)
        {
            Result = result;
            Parameters = parameters;
        }
    }
}
