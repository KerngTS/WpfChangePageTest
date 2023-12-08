using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfCustomControlLibrary1.KValidationRules
{
    public class RegexValidationRule : ValidationRule
    {
        public string Pattern { get; set; }
        public bool AllowEmpty { get; set; }=false;
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return AllowEmpty? ValidationResult.ValidResult:new ValidationResult(false, "Value cannot be empty.");
            }
            string inputValue = value.ToString();
            if(string.IsNullOrEmpty(Pattern))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                if (Regex.IsMatch(inputValue, Pattern))
                    return ValidationResult.ValidResult;
                else
                    return new ValidationResult(false, "Invalid input format.");
            }
        }
    }
}
