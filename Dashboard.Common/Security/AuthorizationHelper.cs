using System.Collections.Generic;
using System.Security.Principal;
using Dashboard.Common.Utilities;

namespace Dashboard.Common.Security
{
    public class AuthorizationHelper
    {
        public static bool CanExecute(string className, string methodName)
        {
            return CanExecute(className, methodName, GetDefaultProvider());
        }

        private static ISecurityRoleProvider GetDefaultProvider()
        {
            return new SecurityRoleProvider();
        }

        public static bool CanExecute(string className, string methodName, ISecurityRoleProvider provider)
        {
            ParameterValidator.AssertIsNotNullOrWhiteSpace(nameof(className), className);
            var flag = false;
            var currentIdentity = WindowsIdentity.GetCurrent();
            var myPrincipal = new WindowsPrincipal(currentIdentity);
            foreach (var authorizedRole in GetAuthorizedRoles(className, methodName, provider))
            {
                if (currentIdentity.Name == authorizedRole || myPrincipal.IsInRole(authorizedRole))
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }

        private static IEnumerable<string> GetAuthorizedRoles(string className, string methodName,
            ISecurityRoleProvider provider)
        {
            try
            {
                return provider.GetAuthorizedRoles(className, methodName);
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}