using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dashboard.Common.Validation
{
    public static class ValidationHelper
    {
        public static void AssertCollectionNotEmpty<T>(IEnumerable<T> files, string memberName, string message)
        {
            if (files == null || !files.Any())
            {
                ValidationResult result = new ValidationResult(message, new string[] { memberName });
                throw new ValidationException(result, new RequiredAttribute(), files);
            }
        }

        public static bool IsValidEmail(string emailAddress)
        {
            bool invalid = false;

            if (String.IsNullOrEmpty(emailAddress))
                return false;

            MatchEvaluator DomainMapper = (match) =>
            {
                string domainName = match.Groups[2].Value;
                IdnMapping idn = new IdnMapping();

                try
                {
                    domainName = idn.GetAscii(domainName);
                }
                catch (ArgumentException)
                {
                    invalid = false;
                }

                return match.Groups[1].Value + domainName;
            };

            // Use IdnMapping class to convert Unicode domain names. 
            try
            {
                emailAddress = Regex.Replace(emailAddress, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            try
            {
                return Regex.IsMatch(emailAddress,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
