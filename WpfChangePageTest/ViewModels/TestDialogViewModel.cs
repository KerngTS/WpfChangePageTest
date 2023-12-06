using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfChangePageTest.Dialog;
using WpfChangePageTest.Mvvm;

namespace WpfChangePageTest.ViewModels
{
    internal class TestDialogViewModel : KNotifyPropertyBase, IDialogHostAware
    {
        private bool? dialogResult;
        public bool? DialogResult
        {
            get { return this.dialogResult; }
            set { dialogResult = value; RaisePropertyChange(); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChange(); }
        }

        private string _content;
        public string Content
        {
            get { return _content; }
            set { _content = value; RaisePropertyChange(); }
        }

        public string DialogHostName { get; set; }

        public IDialogResult Result {get;private set;}

        public KDelegateCommand SaveCommand { get;private set; }
        public KDelegateCommand CancelCommand { get;private set; }

        public TestDialogViewModel()
        {
            SaveCommand = new KDelegateCommand(Save);
            CancelCommand = new KDelegateCommand(Cancel);
        }

        private void Cancel(object obj)
        {
            Result=new DialogResult(DialogStatus.Cancel);
            DialogResult = true;
        }

        private void Save(object obj)
        {
            var status = DialogStatus.OK;
            var param = new DialogParameters();
            param.Add("newTitle", "ResultTitle");
            param.Add("newContent", Content);
            Result = new DialogResult(status, param);
            
            DialogResult = true;
        }

        public void OnDialogOpend(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("title");
            Content = parameters.GetValue<string>("content");
            
        }

    }
}
