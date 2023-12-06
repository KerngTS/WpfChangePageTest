using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChangePageTest.Dialog
{
    public interface IDialogParameters
    {
        int Count { get; }
        IEnumerable<string> Keys { get; }
        void Add(string key, object value);
        bool ContainsKey(string key);
        T GetValue<T>(string key);
    }
}
