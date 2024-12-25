using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace Line98
{
    public class RadioButtonCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value !=null && value.ToString() == parameter.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked)
            {
                return int.Parse(parameter.ToString());
            }
            return Binding.DoNothing;
        }
    }
}
