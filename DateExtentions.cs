using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet2.Extensions
{
    public static class DateExtentions
    {
        public static Dictionary<int, string> IntMonthDictionary = new Dictionary<int, string>()
        {
            {1, "Jan" },
            {2, "Feb" },
            {3, "Mar" },
            {4, "Apr" },
            {5, "May" },
            {6, "June" },
            {7, "July" },
            {8, "Aug" },
            {9, "Sept" },
            {10, "Oct" },
            {11, "Nov" },
            {12, "Dec" }
        };

        public static string ToAbbreviatedMonthString(this int i)
        {
            if (i < 1 || i > 12)
            {
                return string.Empty;
            }
            return IntMonthDictionary[i];
        }
    }
}