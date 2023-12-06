using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfChangePageTest.Mvvm;

namespace WpfChangePageTest.Dialog
{
    /// <summary>
    /// 如果有需要，可以继承Prism里的IDilogAware
    /// 对话框的viewmodel需继承这个接口
    /// </summary>
    public interface IDialogHostAware
    {
        /// <summary>
        /// DialogHost名称，关闭时需要指定名字
        /// </summary>
        string DialogHostName { get; set; }
        IDialogResult Result { get;  }
        void OnDialogOpend(IDialogParameters parameters);
        KDelegateCommand SaveCommand { get;  }
        KDelegateCommand CancelCommand { get;  }
    }
}
