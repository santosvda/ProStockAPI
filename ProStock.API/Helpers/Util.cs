using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ProStock.API.Helpers
{
    public static class Util
    {
        public static DateTime StringToDate(string data)
        {
            return DateTime.ParseExact(data, "ddMMyyyy", CultureInfo.InvariantCulture);
        }
    }
}
