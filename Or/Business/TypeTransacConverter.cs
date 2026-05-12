using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Or.Business
{
    internal class TypeTransacConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string res;

            switch(value)
            {
                case Operation.DepotSimple:
                    res = "Dépôt";
                    break;
                case Operation.RetraitSimple:
                    res = "Retrait";
                    break;
                case Operation.InterCompte:
                    res = "Virement";
                    break;
                default:
                    res = "ERROR";
                    break;
            }

            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
