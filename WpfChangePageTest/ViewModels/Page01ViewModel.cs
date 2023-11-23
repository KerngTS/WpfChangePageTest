using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfChangePageTest.Mvvm;

namespace WpfChangePageTest.ViewModels
{
    internal class Page01ViewModel:KNotifyPropertyBase
    {
        private string _message="here is page 01";
        public string Message
        {
            get { return _message; }
            set { _message = value; RaisePropertyChange(); }
        }

        public KDelegateCommand Changed { get; private set; }
        public Page01ViewModel()
        {
            Changed = new KDelegateCommand(c =>
            {
                Message += "!";
            });
        }
    }
}
