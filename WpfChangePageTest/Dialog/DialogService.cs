using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace WpfChangePageTest.Dialog
{
    public class DialogService : IDialogService
    {
        private Dictionary<string, Tuple<object, object>> viewModelPair = new Dictionary<string, Tuple<object, object>>();
        
        public IDialogResult ShowDialog(Type windowViewType, IDialogParameters parameters, string dialogHostName)
        {
            if (parameters == null)
                parameters = new DialogParameters();

            //从容器中取出弹出窗口的实例
            var content = Activator.CreateInstance(windowViewType);
            //验让实例的有效性
            if (!(content is Window dialogWindow))
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");

            if (!(dialogWindow.DataContext is IDialogHostAware viewModel))
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

            viewModel.DialogHostName = dialogHostName;
            viewModel.OnDialogOpend(parameters);
            
            IDialogResult result=new DialogResult(DialogStatus.None,new DialogParameters());
            dialogWindow.Closed += (s, e) =>
            {
                result = viewModel.Result;
            };
            var res = dialogWindow.ShowDialog();
            //result= viewModel.Result;
            return result;
        }
    }
}
