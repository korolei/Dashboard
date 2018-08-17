using System;

namespace Dashboard.Common.Utilities
{
    public static class PrincipalHelper
    {
        internal const string DOMAIN_OFINA = "OFINA";
        internal const string DOMAIN_FULL = "ofina.on.ca";
        
        public static string TrimEmailDomain(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                return string.Empty;

            int indexOfAtSign = emailAddress.IndexOf('@');

            if (indexOfAtSign == -1)
                return emailAddress;
            return emailAddress.Substring(0, indexOfAtSign);
        }

        public static string AppendEmailDomain(string emailAddress)
        {
            const string EMAIL_DOMAIN = "@" + DOMAIN_FULL;

            if (string.IsNullOrEmpty(emailAddress))
                throw new ArgumentNullException("emailAddress");

            int indexOfAtSign = emailAddress.IndexOf('@');

            if (indexOfAtSign == -1)
                return emailAddress + EMAIL_DOMAIN;
            else if (indexOfAtSign == emailAddress.Length - 1)
                return emailAddress + DOMAIN_FULL;
            else
                return emailAddress;
        }

        public static string PrependDomain(string networkName)
        {
            if (string.IsNullOrEmpty(networkName))
                return string.Empty;

            string identity = networkName ?? string.Empty;

            if (identity.IndexOf('\\') == -1)
                return DOMAIN_OFINA + '\\' + identity;
            return identity;
        }

        public static string TrimDomain(string identityName)
        {
            if (string.IsNullOrEmpty(identityName))
                return string.Empty;

            string identity = identityName ?? string.Empty;

            int idx = identity.IndexOf('\\');
            if (idx == -1)
                return identity;
            return identity.Substring(idx + 1);
        }

        public static bool SameName(string identityNameA, string identityNameB)
        {
            if (identityNameA == null ^ identityNameB == null)
                return false;

            return string.Equals(TrimDomain(identityNameA), TrimDomain(identityNameB), StringComparison.OrdinalIgnoreCase);
        }
    }
}
