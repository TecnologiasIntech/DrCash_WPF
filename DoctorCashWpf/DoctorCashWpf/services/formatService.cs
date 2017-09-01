using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorCashWpf
{
    class formatService
    {

        public string toDateTimeForQuery(string year, string month, string day, string hour)
        {
            if(Convert.ToInt32(month) < 10 && month.Length == 1)
            {
                month = "0" + month;
            }

            if (Convert.ToInt32(day) < 10 && month.Length == 1)
            {
                day = "0" + day;
            }

            string format = year + "-" + month + "-" + day + hour;

            return format;
        }

    }
}
