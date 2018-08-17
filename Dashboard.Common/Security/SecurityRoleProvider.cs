using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Dashboard.Common.Configuration;

namespace Dashboard.Common.Security
{
    public interface ISecurityRoleProvider
    {
        IEnumerable<string> GetAuthorizedRoles(string className, string methodName);
    }

    internal class SecurityRoleProvider : ISecurityRoleProvider
    {
        public IEnumerable<string> GetAuthorizedRoles(string className, string methodName)
        {
            return string.IsNullOrEmpty(methodName) ? 
                GlobalConfigurationSettings.GetSettings(string.Format(CultureInfo.InvariantCulture, "SEC:{0}", className)).Split(';').ToList() : 
                GlobalConfigurationSettings.GetSettings(string.Format(CultureInfo.InvariantCulture, "SEC:{0}.{1}", className, methodName)).Split(';').ToList();
        }
    }
}