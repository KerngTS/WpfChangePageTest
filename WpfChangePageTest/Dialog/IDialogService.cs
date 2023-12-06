using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChangePageTest.Dialog
{
    public interface IDialogService
    {
        /// <summary>
        /// 展示对话框
        /// </summary>
        /// <param name="name">模块</param>
        /// <param name="parameters">参数</param>
        /// <param name="dialogHostName"></param>
        /// <returns></returns>
        IDialogResult ShowDialog(Type windowViewType, IDialogParameters parameters, string dialogHostName);
    }
}
