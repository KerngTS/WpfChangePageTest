using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChangePageTest.Dialog
{
    public class DialogParameters :  IDialogParameters
    {
        private Dictionary<string,object> parameters;>
        public DialogParameters()
        {
            parameters = new Dictionary<string,object>();
        }
        public int Count => parameters.Count;

        public IEnumerable<string> Keys => parameters.Keys;

        public void Add(string key, object value)
        {
            parameters.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return parameters.ContainsKey(key);
        }

        public T GetValue<T>(string key)
        {
            var type = typeof(T);
            if(this.ContainsKey(key))
            {
                var value = parameters[key];
                return (T)value;
            }
            return default(T);
        }
    }
}
