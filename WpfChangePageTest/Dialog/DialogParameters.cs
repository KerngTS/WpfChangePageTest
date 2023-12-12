using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChangePageTest.Dialog
{
    public class DialogParameters :  IDialogParameters
    {
        private Dictionary<string,object> parameters;
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

        public bool TryGetValue<T>(string key,out T tValue)
        {
            var success = false;
            var type = typeof(T);
            var value = GetDefault(type);
            if (this.ContainsKey(key))
            {
                value = parameters[key];
            }

            if  (value == null)
            {
                success = true;
            }
            else if (value.GetType() == type)
            {
                success = true;
            }
            else if (type.IsAssignableFrom(value.GetType()))
            {
                success = true;
            }
            else if (type.IsEnum)
            {
                var valueAsString = value.ToString();
                if (Enum.IsDefined(type, valueAsString))
                {
                    success = true;
                    value = Enum.Parse(type, valueAsString);
                }
                else if (int.TryParse(valueAsString, out var numericValue))
                {
                    success = true;
                    value = Enum.ToObject(type, numericValue);
                }
            }

            if (!success && type.GetInterface("System.IConvertible") != null)
            {
                success = true;
                value = Convert.ChangeType(value, type);
            }
            tValue=(T)value;
            return success;
        }

        private static bool TryGetValueInternal(KeyValuePair<string, object> kvp, Type type, out object value)
        {
            value = GetDefault(type);
            var success = false;
            if (kvp.Value == null)
            {
                success = true;
            }
            else if (kvp.Value.GetType() == type)
            {
                success = true;
                value = kvp.Value;
            }
            else if (type.IsAssignableFrom(kvp.Value.GetType()))
            {
                success = true;
                value = kvp.Value;
            }
            else if (type.IsEnum)
            {
                var valueAsString = kvp.Value.ToString();
                if (Enum.IsDefined(type, valueAsString))
                {
                    success = true;
                    value = Enum.Parse(type, valueAsString);
                }
                else if (int.TryParse(valueAsString, out var numericValue))
                {
                    success = true;
                    value = Enum.ToObject(type, numericValue);
                }
            }

            if (!success && type.GetInterface("System.IConvertible") != null)
            {
                success = true;
                value = Convert.ChangeType(kvp.Value, type);
            }

            return success;
        }

        private static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}
