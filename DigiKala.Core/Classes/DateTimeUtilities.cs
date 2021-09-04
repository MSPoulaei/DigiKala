using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DigiKala.Core.Classes
{
    public static class DateTimeUtilities
    {
        public static string ToShamsi(this DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(date) + "/" +
                   persianCalendar.GetMonth(date) + "/" +
                   persianCalendar.GetDayOfMonth(date);
        }
    }
}
