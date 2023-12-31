﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProteusMMX.Helpers
{
    public class RemoveHTML
    {
        public static string StripHTML(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return "";
            }
            text = text.Replace("&nbsp;", " ").Replace("<br>", "\n");
            var oRegEx = new System.Text.RegularExpressions.Regex("<.*?>| &.*?&nbsp;");
            return oRegEx.Replace(text, string.Empty);
        }
        public static string StripHtmlTags(string html)
        {
            return StripHTML(System.Net.WebUtility.HtmlDecode(html));
        }
        //public static string StripHtmlTags(string source)
        //{
        //    return Regex.Replace(source, "<.*?>| &.*?&nbsp;", string.Empty);
        //}

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.None);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }


    }
}
