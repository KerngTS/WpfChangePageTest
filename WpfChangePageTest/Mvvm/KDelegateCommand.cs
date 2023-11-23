using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfChangePageTest.Mvvm
{
    public class KDelegateCommand : ICommand
    {
        /// <summary>
        /// 命令是否可用状态发生改变时通知命令调用者
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 判断命令是否可用
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CanExecute(object parameter)
        {
            if (CanExecuteFunc != null)
            {
                return this.CanExecuteFunc(parameter);
            }
            return true;
        }

        /// <summary>
        /// 命令执行时需要做的事情
        /// </summary>
        /// <param name="parameter"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Execute(object parameter)
        {
            if (ExecuteAction != null)
                ExecuteAction(parameter);
        }

        public Action<object> ExecuteAction { get; set; }
        public Func<object, bool> CanExecuteFunc { get; set; }

        public KDelegateCommand(Action<object> execute, Func<object, bool>canExecute=null)
        {
            ExecuteAction=execute;
            CanExecuteFunc=canExecute;
        }
    }
}
