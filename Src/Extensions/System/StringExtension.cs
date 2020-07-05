using System;
using System.Collections.Generic;
using System.Linq;

namespace GoodToCode.Extensions
{
    /// <summary>
    /// StringExtension
    /// </summary>
    public static class  StringExtension
    {

        /// <summary>
        /// Adds string/char if string begins with that string/char
        /// </summary>
        /// <param name="item">Item to Add part</param>
        /// <param name="toAdd">string to Add if match</param>
        /// <returns>Original item without the Addd substring</returns>
        public static string AddFirst(this string item, string toAdd)
        {
            var returnValue = item.Trim();

            if (item.IsFirst(toAdd) == false)
            {
                returnValue = (toAdd + item);
            }

            return returnValue;
        }

        /// <summary>
        /// Adds string/char if string ends with that string/char
        /// </summary>
        /// <param name="item">Item to Add part</param>
        /// <param name="toAdd">string to Add if match</param>
        /// <returns>Original item without the Addd substring</returns>
        public static string AddLast(this string item, string toAdd)
        {
            var returnValue = item.Trim();

            if (item.IsLast(toAdd) == false)
            {
                returnValue = (item + toAdd);
            }

            return returnValue;
        }

        /// <summary>
        /// Applies pascal casing to a string
        /// </summary>
        /// <param name="uncasedString">string to case</param>
        /// <param name="parseCharacter">Character that decides when to start a new capital letter</param>
        /// <param name="useExistingCase">Protects all upper characters, or previously cased characters from getting re-cased.</param>
        /// <returns>Cased string</returns>
        private static string FormatCasePascal(string uncasedString, string parseCharacter, bool useExistingCase = true)
        {
            var returnValue = string.Empty;
            string[] words = uncasedString.Split(parseCharacter.ToCharArray());
            int count;
            for (count = 0; count <= words.Length - 1; count++)
            {
                string word;
                // Upper-case abbreviations (P.O., B.S.A.)
                if ((words[count].Replace(".", string.Empty).Length == (words[count].Length / 2)))
                {
                    word = words[count].ToUpperInvariant();
                }
                else
                {
                    if (useExistingCase == false)
                    {
                        word = words[count].ToLowerInvariant();
                    }
                    else
                    {
                        word = words[count];
                    }
                }
                if (word.Length > 0)
                {
                    char firstLetter = char.ToUpperInvariant(word[0]);
                    returnValue = returnValue + firstLetter + word.Substring(1) + parseCharacter;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Formats exceptions for Pascal Case 
        ///  (i.e. Mr. Smith II should not be pascal cased to Ii)
        /// </summary>
        /// <param name="uncasedString">string to search for exceptions to special-case</param>
        /// <param name="parseCharacter">Character that decides when to start a new capital letter</param>
        /// <returns>Cased item based on the exception casing rules</returns>
        private static string FormatCaseException(string uncasedString, string parseCharacter)
        {
            var returnValue = string.Empty;
            string[] words = uncasedString.Split(parseCharacter.ToCharArray());
            int count;
            for (count = 0; count <= words.Length - 1; count++)
            {
                string word = words[count];
                if (word.Length > 0)
                {
                    switch (word.ToLowerInvariant())
                    {
                        case "jr.":
                            word = "Jr.";
                            break;
                        case "ii":
                            word = "II";
                            break;
                        case "iii":
                            word = "III";
                            break;
                        case "iv":
                            word = "IV";
                            break;
                    }
                    returnValue = returnValue + word + parseCharacter;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Is this item all upper case?
        /// </summary>
        /// <param name="item">Item to validate</param>
        /// <returns>True if this is all upper case.</returns>
        public static bool IsCaseUpper(this string item)
        {
            var returnValue = false;

            if (item == item.ToUpperInvariant())
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Is this item all lower case?
        /// </summary>
        /// <param name="item">Item to validate</param>
        /// <returns>True if this is all lower case.</returns>
        public static bool IsCaseLower(this string item)
        {
            var returnValue = false;

            if (item == item.ToLowerInvariant())
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Is this item mixed case?
        /// </summary>
        /// <param name="item">Item to validate</param>
        /// <returns>True if this has mixed case.</returns>
        public static bool IsCaseMixed(this string item)
        {
            return !item.IsCaseLower() & !item.IsCaseUpper();
        }

        /// <summary>
        /// Is this item an email address?
        /// </summary>
        /// <param name="item">Item to validate</param>
        /// <param name="emptyStringOK">Flags an empty string as valid, even though it is not an email address</param>
        /// <returns>True if this is an email address (or if empty.)</returns>
        public static bool IsEmail(this string item, bool emptyStringOK = true)
        {
            var returnValue = false;

            item = item.Trim();
            if ((emptyStringOK == true & item.Length == 0))
            {
                returnValue = true;
            } else
            {
                if ((item.Contains("@") & item.Contains("."))
                    && (item.IndexOf(".", item.IndexOf("@")) > item.IndexOf("@"))
                    && (item.Contains(" ") == false)
                    && (item.SubstringSafe(item.IndexOf("@") + 1).Contains("@") == false))
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Is the first character(s) equal to the passed characters?
        /// </summary>
        /// <param name="item">Item to validate</param>
        /// <param name="firstCharacters">Character to look for</param>
        /// <returns>True/False if found characters in position</returns>
        public static bool IsFirst(this string item, string firstCharacters)
        {
            var returnValue = false;

            if (item.Length >= firstCharacters.Length)
            {
                if (item.SubstringSafe(0, firstCharacters.Length) == firstCharacters)
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Is this item an integer?
        /// </summary>
        /// <param name="item">Item to validate</param>
        /// <returns>True if this is an integer.</returns>
        public static bool IsInteger(this string item)
        {
            var returnValue = false;

            if (item.TryParseInt64() != -1)
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Is the last character(s) equal to the passed characters?
        /// </summary>
        /// <param name="item">Item to validate</param>
        /// <param name="lastCharacters">Character to look for</param>
        /// <returns>True/False if found characters in position</returns>
        public static bool IsLast(this string item, string lastCharacters)
        {
            var returnValue = false;

            if (item.Length >= lastCharacters.Length)
            {
                if (item.SubstringRight(lastCharacters.Length) == lastCharacters)
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }        

        /// <summary>
        /// Removes string/char if string begins with that string/char
        /// </summary>
        /// <param name="item">Item to remove part</param>
        /// <param name="toRemove">string to remove if match</param>
        /// <returns>Original item without the removed substring</returns>
        public static string RemoveFirst(this string item, string toRemove)
        {
            var returnValue = item.Trim();

            if (item.IsFirst(toRemove))
            {
                returnValue = item.SubstringRight(item.Length - toRemove.Length);
            }

            return returnValue;
        }

        /// <summary>
        /// Removes string/char if string ends with that string/char
        /// </summary>
        /// <param name="item">Item to remove part</param>
        /// <param name="toRemove">string to remove if match</param>
        /// <returns>Original item without the removed substring</returns>
        public static string RemoveLast(this string item, string toRemove)
        {
            var returnValue = item.Trim();

            if (item.IsLast(toRemove))
            {
                returnValue = item.SubstringLeft(item.Length - toRemove.Length);
            }

            return returnValue;
        }

        /// <summary>
        /// Converts a string to Boolean
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <param name="notFoundValue">Value if not found</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static bool TryParseBoolean(this string item, bool notFoundValue = false)
        {
            var returnValue = notFoundValue;
            var convertValue = false;

            if (String.IsNullOrEmpty(item) == false)
            {                
                if (item.TryParseInt16() != -1) // Catch integers, as TryParse only evaluates "true" and "false", not "0".
                {
                    returnValue = item.TryParseInt16() == 0 ? false : true;
                }
                else if (Boolean.TryParse(item, out convertValue))
                {
                    returnValue = convertValue;
                }
            }

            return returnValue;
        }
        
        /// <summary>
        /// Converts a string to Int16
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <param name="notFoundValue">Value if not found</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static short TryParseInt16(this string item, short notFoundValue = -1)
        {
            var returnValue = notFoundValue;

            // Try to parse it out
            if (String.IsNullOrEmpty(item) == false)
            {
                if (Int16.TryParse(item, out short convertValue))
                {
                    returnValue = convertValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts a string to int
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <param name="notFoundValue">Value if not found</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static int TryParseInt32(this string item, int notFoundValue = -1)
        {
            var returnValue = notFoundValue;
            if (String.IsNullOrEmpty(item) == false)
            {
                if (int.TryParse(item, out int convertValue))
                {
                    returnValue = convertValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts a string to Int64
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <param name="notFoundValue">Value if not found</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static long TryParseInt64(this string item, long notFoundValue = -1)
        {
            var returnValue = notFoundValue;
            if (String.IsNullOrEmpty(item) == false)
            {
                if (Int64.TryParse(item, out long convertValue))
                {
                    returnValue = convertValue;
                }
            }

            return returnValue;
        }
        
        /// <summary>
        /// Converts string to Guid
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <param name="notFoundValue">Value if not found</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static Guid TryParseGuid(this string item, Guid notFoundValue = default(Guid))
        {
            var returnValue = notFoundValue;

            if (String.IsNullOrEmpty(item) == false)
            {
                try
                {
                    returnValue = new Guid(item);
                }
                catch
                {
                    returnValue = notFoundValue;
                }
            }

            return returnValue;
        }
        
        /// <summary>
        /// Converts string to decimal
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <param name="notFoundValue">Value if not found</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static decimal TryParseDecimal(this string item, decimal notFoundValue = 0m)
        {
            var returnValue = 0m;
            if (String.IsNullOrEmpty(item) == false)
            {
                if (Decimal.TryParse(item, out decimal convertValue))
                {
                    returnValue = convertValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts string to double
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <param name="notFoundValue">Value if not found</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static double TryParseDouble(this string item, double notFoundValue = 0.0)
        {
            var returnValue = 0.0;
            var convertValue = 0.0;

            if (String.IsNullOrEmpty(item) == false)
            {
                if (Double.TryParse(item, out convertValue))
                {
                    returnValue = convertValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts string to double
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <param name="notFoundValue">Value if not found</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static TEnum TryParseEnum<TEnum>(this string item, TEnum notFoundValue)
        {
            var returnValue = notFoundValue;

            try
            {
                if (String.IsNullOrEmpty(item) == false)
                {
                    returnValue = (TEnum)Enum.Parse(typeof(TEnum), item, true);
                }
            }
            catch (ArgumentException)
            {
                returnValue = notFoundValue;
            }
            catch (OverflowException)
            {
                returnValue = notFoundValue;
            }

            return returnValue;
        }        
        
        /// <summary>
        /// Converts string to DateTime
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <returns>Converted or not found value (01/01/1900) of the source item</returns>
        public static DateTime TryParseDateTime(this string item)
        {
            var notFoundValue = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);
            var returnValue = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);
            var convertDate = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);

            item = item.Trim();
            if (item.IsInteger() == true & item.Length == 8)
            {
                item = item.Substring(0, 2) + "-" + item.Substring(2, 2) + "-" + item.Substring(4, 4);
            }
            if ((!(String.IsNullOrEmpty(item))) & (item.Trim().Length >= 8))
            {
                if (DateTime.TryParse(item, out convertDate))
                {
                    if (convertDate.IsSavable())
                    {
                        returnValue = convertDate;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts a string to time (of date 01/01/1900)
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <returns>Converted or not found value of the source item</returns>
        public static DateTime TryParseTime(this string item)
        {
            var notFoundValue = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);
            var returnValue = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);
            var convertDate = new DateTime(1900, 01, 01, 00, 00, 00, 000, DateTimeKind.Utc);

            item = item.Trim();
            if ((String.IsNullOrEmpty(item) == false))
            {
                if (DateTime.TryParse("01/01/1900 " + item, out convertDate))
                {
                    returnValue = convertDate;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts string to Uri
        /// </summary>
        /// <param name="item">Source item to convert</param>
        /// <returns>Converted value if success. 
        /// Failure returns http://localhost:80, value of Default.Uri</returns>
        public static Uri TryParseUri(this string item)
        {
            var returnValue = new Uri("http://localhost:80", UriKind.RelativeOrAbsolute);

            if (String.IsNullOrEmpty(item) == false)
            {
                try
                {
                    returnValue = new Uri(item);
                }
                catch
                {
                    returnValue = new Uri("http://localhost:80", UriKind.RelativeOrAbsolute);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Pulls right characters from a String
        /// </summary>
        /// <param name="item">Item to extract right characters</param>
        /// <param name="rightCharacters">Number of characters, starting from the right</param>
        /// <returns>Characters or original string, starting from the right</returns>
        public static string SubstringRight(this string item, int rightCharacters)
        {
            return item.SubstringSafe(item.Length - rightCharacters);
        }

        /// <summary>
        /// Pulls left characters from a String
        /// </summary>
        /// <param name="item">Item to extract left characters</param>
        /// <param name="leftCharacters">Number of characters, starting from the left</param>
        /// <returns>Characters or original string, starting from the left</returns>
        public static string SubstringLeft(this string item, int leftCharacters)
        {
            return item.SubstringSafe(0, leftCharacters);
        }

       /// <summary>
       /// Extracts substring exception-safe
       /// </summary>
       /// <param name="item">Item to extract the substring</param>
       /// <param name="starting">Starting position</param>
       /// <param name="length">Number of characters to try to extract</param>
       /// <returns>Extracted characters, or original string if cant substring.</returns>
        public static string SubstringSafe(this string item, int starting, int length = -1)
        {
            var returnValue = string.Empty;
            var itemLength = item.Length;

            if (length == -1) length = itemLength - starting;
            if (itemLength > length - (starting + 1))
            {
                returnValue = length > -1 ? item.Substring(starting, length) : item.Substring(starting);
            }
            else
            {
                returnValue = itemLength == length - (starting + 1) ? item.Substring(starting, length - 1) : item;
            }

            return returnValue;
        }

        /// <summary>
        /// Splits a CSV into a list
        /// </summary>
        /// <param name="item">CSV string to convert to a list</param>
        /// <param name="separator">Character that separates the elements. Default is comma ','</param>
        /// <returns></returns>
        public static List<string> Split(this string item, char separator = ',')
        {
            var returnValue = new List<string>();
            if (!string.IsNullOrWhiteSpace(item))
            {
                returnValue = item
                    .TrimEnd(separator)
                    .Split(separator)
                    .AsEnumerable<string>()
                    .Select(s => s.Trim())
                    .ToList();
            }
            return returnValue;
        }

        /// <summary>
        /// Applies pascal casing to a string
        /// </summary>
        /// <param name="uncasedString">string to case</param>
        /// <returns>Cased string</returns>
        public static string ToPascalCase(this string uncasedString)
        {
            var returnValue = string.Empty;
            var partiallyCased = string.Empty;

            // Do nothing if nothing to work with
            if (string.IsNullOrEmpty(uncasedString) == false & ((uncasedString.ToLowerInvariant() == uncasedString) | (uncasedString.ToUpperInvariant() == uncasedString) & uncasedString.Contains(" ")))
            {
                uncasedString = uncasedString.Trim();
                partiallyCased = StringExtension.FormatCasePascal(uncasedString, " ", false);
                partiallyCased = StringExtension.FormatCasePascal(partiallyCased, "-");
                partiallyCased = StringExtension.FormatCasePascal(partiallyCased, "'");
                partiallyCased = StringExtension.FormatCaseException(partiallyCased, " ");
                returnValue = partiallyCased;
                returnValue = returnValue.Trim();
                if (returnValue.Length > uncasedString.Length)
                {
                    returnValue = returnValue.Substring(0, uncasedString.Length);
                }
            }
            else
            {
                returnValue = uncasedString;
            }

            return returnValue.Trim();
        }
    }
}
