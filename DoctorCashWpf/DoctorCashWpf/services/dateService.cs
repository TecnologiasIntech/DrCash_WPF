using System;

namespace DoctorCashWpf
{
    class dateService
    {
        private formatService format = new formatService();
        private string formatDate = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

        public string getCurrentDate()
        {
            var currentDay = DateTime.Today;

            var day = currentDay.Day.ToString();
            var month = currentDay.Month.ToString();
            var year = currentDay.Year.ToString();

            return format.toDateTimeForQuery(year, month, day, "T00:00:00.000");
        }

        public string getInitialDate()
        {
            var currentDay = DateTime.Today;

            var day = currentDay.Day.ToString();
            var month = currentDay.Month.ToString();
            var year = currentDay.Year.ToString();

            return format.toDateTimeForQuery(year, month, day, "T00:00:00.000");
        }

        public string getEndDate()
        {
            var currentDay = DateTime.Today;

            var day = currentDay.Day.ToString();
            var month = currentDay.Month.ToString();
            var year = currentDay.Year.ToString();

            return format.toDateTimeForQuery(year, month, day, "T23:59:59.999");
        }

        public string convertToFormatDate(string date)
        {
            string day = "", month = "";

            if (formatDate == "dd/MM/yyyy")
            {
                day = date.Substring(0, 2);
                month = date.Substring(3, 2);
            }
            else
            {
                month = date.Substring(0, 2);
                day = date.Substring(3, 2);
            }
            string year = date.Substring(6, 4);
            string hour = "T00:00:00.000";

            return format.toDateTimeForQuery(year, month, day, hour);
        }

        public string convertToFormatDateFinal(string date)
        {
            string day = "", month = "";

            if (formatDate == "dd/MM/yyyy")
            {
                day = date.Substring(0, 2);
                month = date.Substring(3, 2);
            }
            else
            {
                month = date.Substring(0, 2);
                day = date.Substring(3, 2);
            }
            string year = date.Substring(6, 4);
            string hour = "T23:59:59.999";

            return format.toDateTimeForQuery(year, month, day, hour);
        }

    }
}
