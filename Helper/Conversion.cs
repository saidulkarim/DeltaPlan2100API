using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaPlan2100API.Helper
{
    public static class Conversion
    {
        public static decimal ToDecimal(this string val)
        {
            decimal result = string.IsNullOrEmpty(val) ? 0 : decimal.Parse(val);
            return Convert.ToDecimal(string.Format("{0:0.00}", result));
        }
    }
}
