using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Intranet2.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToMoneyString(this string str)
        {
            return string.Format("{0:#.00}", Convert.ToDecimal(str) / 100);
        }
        public static string ToCaptilizedWords(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            char[] arr = str.ToCharArray();
            for (int index = 0; index < str.Count(); index++)
            {
                arr[index] = char.ToLower(arr[index]);
            }
            // capitalize first character
            arr[0] = char.ToUpper(arr[0]);
            return new string(arr);
        }

        public static string ToPhoneNumberForDisplay(this string phoneNumber)
        {
            if (phoneNumber.IsNullOrEmpty())
            {
                return string.Empty;
            }

            if (phoneNumber.Count() != 10)
                return phoneNumber;

            return Regex.Replace(phoneNumber, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");
        }

        public static string Titleize(this string text)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text).ToSentenceCase();
        }

        public static string ToSentenceCase(this string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static object DBString(this string Text)
        {
            return Text.IsNullOrEmpty() ? (object)DBNull.Value : Text;
        }

        public static string RemoveHtml(this string text)
        {
            string temp = Regex.Replace(text, "<.*?>", String.Empty);
            temp = temp.Replace("&nbsp;", " ");
            temp = temp.Replace("&(.*?);", " ");
            temp = temp.NormalizeWhiteSpaceForLoop();
            temp = temp.Replace("\n", string.Empty);
            temp = temp.Replace("\t", string.Empty);
            temp = temp.Replace("\r", string.Empty);
            temp = HttpUtility.HtmlDecode(temp);
            return temp;
        }

        public static string NormalizeWhiteSpaceForLoop(this string input)
        {
            int len = input.Length,
                index = 0,
                i = 0;
            var src = input.ToCharArray();
            bool skip = false;
            char ch;
            for (; i < len; i++)
            {
                ch = src[i];
                switch (ch)
                {
                    case '\u0020':
                    case '\u00A0':
                    case '\u1680':
                    case '\u2000':
                    case '\u2001':
                    case '\u2002':
                    case '\u2003':
                    case '\u2004':
                    case '\u2005':
                    case '\u2006':
                    case '\u2007':
                    case '\u2008':
                    case '\u2009':
                    case '\u200A':
                    case '\u202F':
                    case '\u205F':
                    case '\u3000':
                    case '\u2028':
                    case '\u2029':
                    case '\u0009':
                    case '\u000A':
                    case '\u000B':
                    case '\u000C':
                    case '\u000D':
                    case '\u0085':
                        if (skip) continue;
                        src[index++] = ch;
                        skip = true;
                        continue;
                    default:
                        skip = false;
                        src[index++] = ch;
                        continue;
                }
            }

            return new string(src, 0, index);
        }

        #region Core settings

        public static readonly string CoreTrueValue = "Y";

        public static string ToCoreString(this bool b)
        {
            return b ? CoreTrueValue : string.Empty;
        }

        public static bool CoreStringToBool(this string str)
        {
            return !str.IsNullOrEmpty() && str == CoreTrueValue;
        }

        public static string JavascriptBooleanToCoreString(this string str)
        {
            return !str.IsNullOrEmpty() && str == "true" ? CoreTrueValue : string.Empty;
        }

        public static string CheckBoxInputToCoreString(this string str)
        {
            return !str.IsNullOrEmpty() && str == "on" ? CoreTrueValue : string.Empty;
        }

        #endregion
    }
}
