using KEventAggregator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using WpfChangePageTest.Dialog;
using WpfChangePageTest.Mvvm;
using WpfChangePageTest.Views;

namespace WpfChangePageTest
{
    internal class WindowViewModel:KNotifyPropertyBase
    {
        private bool _isLeftOpen;
        public bool IsLeftOpen
        {
            get { return _isLeftOpen; }
            set { _isLeftOpen = value; RaisePropertyChange();Console.WriteLine(IsLeftOpen); }
        }
        private bool _isRightOpen;
        public bool IsRightOpen
        {
            get { return _isRightOpen; }
            set { _isRightOpen = value; RaisePropertyChange(); }
        }
        private string _curPageName = "NONE";
        public string CurPageName
        {
            get { return _curPageName; }
            set { _curPageName = value; RaisePropertyChange(); }
        }

        private string _roleMessage;
        public string RoleMessage1
        {
            get { return _roleMessage; }
            set { _roleMessage = value; RaisePropertyChange(); }
        }

        private string _roleMessage2;
        public string RoleMessage2
        {
            get { return _roleMessage2; }
            set { _roleMessage2 = value; RaisePropertyChange(); }
        }

        private UserControl _currentPage;
        public UserControl CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; RaisePropertyChange(); }
        }

        private List<ColorValue> colorValues;
        public List<ColorValue> ColorValues
        {
            get { return colorValues; }
            set { colorValues = value;RaisePropertyChange(); }
        }

        private ObservableCollection<TestModel> _modelList;
        public ObservableCollection<TestModel> ModelList
        {
            get { return _modelList; }
            set { _modelList = value; RaisePropertyChange(); }
        }

        private IList<string> _dateStr;
        public IList<string> DateStrs
        {
            get { return _dateStr; }
            set { _dateStr = value; RaisePropertyChange(); }
        }

        public KDelegateCommand Page01Command { get;private set; }
        public KDelegateCommand Page02Command { get;private set; }

        public KDelegateCommand LeftCommand { get;private set; }
        public KDelegateCommand RightCommand { get;private set; }
        public KDelegateCommand DialogCommand { get;private set; }

        public WindowViewModel()
        {
            DialogCommand = new KDelegateCommand(Dialog);
            Page01Command = new KDelegateCommand(ToPage01);
            Page02Command = new KDelegateCommand(ToPage02);
            LeftCommand=new KDelegateCommand(LeftOpen);
            RightCommand = new KDelegateCommand(RightOpen);
            ColorValues = new List<ColorValue>();
            var cfg = new Cfg();
            ColorValues.Add(cfg.BackgroundColor);
            ColorValues.Add(cfg.TextColor);
            ColorValues.Add(cfg.SbTextColor);
            ColorValues.Add(cfg.UiLinesColor);
            ColorValues.Add(cfg.SbBackgroundColor);

            ModelList=new ObservableCollection<TestModel>(TestModel.GetDefaultModels());
            //DateStrs=new List<string>();
            DateStrs = ModelList[0].DateValues.Select(c=>c.Date.ToString("yyyy-MM-dd")).OrderBy(c=>c).ToList();
        }

        private void Dialog(object obj)
        {
            IDialogService dlgService = new DialogService();
            IDialogParameters parameters = new DialogParameters();
            parameters.Add("title", "DialogTitle");
            parameters.Add("content", "DialogContent");
            var res = dlgService.ShowDialog(typeof(TestDialogView), parameters, "");
            if (res.Result == DialogStatus.OK)
                MessageBox.Show(res.Parameters.GetValue<string>("newTitle") + "\n" + res.Parameters.GetValue<string>("newContent"));
            else
                MessageBox.Show(res.Result.ToString());
        }

        private void RightOpen(object obj)
        {
            //IsRightOpen = true;
            KEventAggregator.KEventAggregator1.Instance.Publish<TestEvent>(new TestEvent { Filter = "ROOT", Message = "ROOTRightOpen!" });
            KEventAggregator.KEventAggregator1.Instance.Publish<TestEvent>(new TestEvent { Filter = "CHILD", Message = "CHILDRightOpen!" });
        }

        private void LeftOpen(object obj)
        {
            //IsLeftOpen = true;
            KEventAggregator.KEventAggregator1.Instance.Publish<TestEvent>(new TestEvent { Filter = "ROOT", Message = "ROOTLEFTtOpen!" });
            KEventAggregator.KEventAggregator1.Instance.Publish<TestEvent>(new TestEvent { Filter = "CHILD", Message = "CHILDLEFTOpen!" });
        }

        private void ToPage01(object obj)
        {
            //IntPtr hwnd = new WindowInteropHelper(this).Handle; //this就是要获取句柄的窗体的类名；
            //IntPtr hwnd = ((HwndSource)PresentationSource.FromVisual(page)).Handle;//uielement就是要获取句柄的控件，该控
            var type = typeof(Page01);
            if(!pages.ContainsKey(type))
            {
                var page = new Page01();
                
                pages.Add(type, page);
            }
            CurrentPage = pages[type];
            CurPageName = "PAGE_01";
        }

        private void ToPage02(object obj)
        {
            var type = typeof(Page02);
            if (!pages.ContainsKey(type))
            {
                var page = new Page02();
                pages.Add(type, page);
            }
            CurrentPage = pages[type];
            CurPageName = "PAGE_02";
        }

        private Dictionary<Type,UserControl> pages = new Dictionary<Type,UserControl>();
    }

    internal class TestModel:KNotifyPropertyBase
    {
        private string _itemNumber;
        public string ItemNumber
        {
            get { return _itemNumber; }
            set { _itemNumber = value; RaisePropertyChange(); }
        }
        private string _project;
        public string Project
        {
            get { return _project; }
            set { _project = value; RaisePropertyChange(); }
        }
        private string _process;
        public string Process
        {
            get { return _process; }
            set { _process = value; RaisePropertyChange(); }
        }

        private IList<DateValueModel> _dateValues;
        public IList<DateValueModel> DateValues
        {
            get { return _dateValues; }
            set { _dateValues = value; RaisePropertyChange(); }
        }

        public TestModel()
        {
            DateValues=new List<DateValueModel>();
        }

        public static List<TestModel> GetDefaultModels()
        {
            var rd = new Random();
            var result = new List<TestModel>();
            var projs = new[] {"R12D","C19","R4","H3B" };
            var process = new[] { "SMT", "FATP", "PACKOUT" };
            var startDate = DateTime.Now.Date;
            var endDate = startDate.AddDays(14);
            for (var i = 0;i<projs.Length;i++)
            {
                var proj = projs[i];
                for (var j = 0;j<process.Length;j++)
                {
                    var pros = process[j];
                    var itemNum = $"ITEM-{proj}-{pros}";
                    var model = new TestModel()
                    {
                        ItemNumber = itemNum,
                        Process = pros,
                        Project = proj
                    };
                    var date = startDate.Date;
                    while(date<endDate)
                    {
                        var dateValue = new DateValueModel
                        {
                            Date = date,
                            InputQty = rd.Next(1500,2500),
                            OutputQty = rd.Next(1000, 2000),
                        };
                        model.DateValues.Add(dateValue);
                        date = date.AddDays(1);
                    }
                    result.Add(model);
                }
            }
            return result;
        }
    }

    internal class DateValueModel:KNotifyPropertyBase
    {
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; RaisePropertyChange(); }
        }
        private double _inputQty;
        public double InputQty
        {
            get { return _inputQty; }
            set { _inputQty = value; RaisePropertyChange(); }
        }
        private double _outputQty;
        public double OutputQty
        {
            get { return _outputQty; }
            set { _outputQty = value; RaisePropertyChange(); }
        }
    }
}
