using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Common.Configuration;
using Dashboard.Common.Connected_Services.GroupIdentificationService;
using Dashboard.Common.Connected_Services.UserProfileService;
using Dashboard.Common.Services;

namespace Dashboard.Common.Utilities
{
    public enum QueryBehaviour
    {
        Any,
        All
    }

    public static class ActiveDirectoryHelper
    {
        #region Public Methods

        public static IEnumerable<string> GetUsers(string groupName)
        {
            var members = new Collection<string>();

            using (var ctx = new PrincipalContext(ContextType.Domain, PrincipalHelper.DOMAIN_OFINA))
            {
                GroupPrincipal grp = null;

                try
                {
                    grp = GroupPrincipal.FindByIdentity(ctx, IdentityType.Name, groupName);

                    if (grp == null)
                        throw new InvalidOperationException(
                            "We did not find that group in that domain, perhaps the group resides in a different domain?");
                    else
                        foreach (var p in grp.GetMembers(true))
                            members.Add(p.SamAccountName);
                }
                finally
                {
                    grp?.Dispose();
                }
            }

            return members;
        }

        public static async Task<IUser[]> GetDirectReportsAsync(string userName)
        {
            // validate input parameters
            ParameterValidator.AssertIsNotNullOrWhiteSpace("userName", userName);

            var client = GetUsersClient();

            return (IUser[]) await client.Execute(o => o.GetDirectReportsAsync(userName));
        }

        public static async Task<IUser[]> GetManagersAsync(string userName)
        {
            // validate input parameters
            ParameterValidator.AssertIsNotNullOrWhiteSpace("userName", userName);

            var client = GetUsersClient();

            return (IUser[]) await client.Execute(o => o.GetManagersAsync(userName, int.MaxValue));
        }

        public static async Task<IUser> GetUserAsync(string userName)
        {
            // validate input parameters
            ParameterValidator.AssertIsNotNullOrWhiteSpace("userName", userName);

            // return a list of users within the specified group
            var client = GetUsersClient();

            return (IUser) await client.Execute(o => o.GetUserAndRolesAsync(userName, SearchDepth.Nested));
        }

        public static async Task<IUser> GetContactAsync(string email)
        {
            // validate input parameters
            ParameterValidator.AssertIsNotNullOrWhiteSpace("email", email);

            // return a list of users within the specified group
            var client = GetUsersClient();

            return (IUser) await client.Execute(o => o.GetContactByEmailAsync(email));
        }

        public static string GetEmailAddressByDisplayName(string displayName)
        {
            ParameterValidator.AssertIsNotNullOrWhiteSpace("displayName", displayName);

            return GetActiveDirectoryPropertyValue("displayname", displayName, "mail");
        }

        public static string GetEmailAddressByNetworkName(string networkName)
        {
            ParameterValidator.AssertIsNotNullOrWhiteSpace("networkName", networkName);

            var filteredNetworkName = PrincipalHelper.TrimDomain(networkName);

            return GetActiveDirectoryPropertyValue("sAMAccountName", filteredNetworkName, "mail");
        }

        public static string GetDisplayNameByEmailAddress(string emailAddress)
        {
            ParameterValidator.AssertIsNotNullOrWhiteSpace("emailAddress", emailAddress);

            return GetActiveDirectoryPropertyValue("mail", emailAddress, "displayname");
        }

        public static string GetNetworkNameByEmailAddress(string emailAddress)
        {
            ParameterValidator.AssertIsNotNullOrWhiteSpace("emailAddress", emailAddress);

            return GetActiveDirectoryPropertyValue("mail", emailAddress, "sAMAccountName");
        }

        public static async Task<IUser[]> GetUsersInGroupsAsync(IEnumerable<string> groupNames,
            QueryBehaviour behaviour)
        {
            // validate input parameters
            ParameterValidator.AssertIsNotNull("groupNames", groupNames);

            var nonEmptyGroups = groupNames.Where(o => !string.IsNullOrWhiteSpace(o)).ToArray();

            ParameterValidator.AssertIsNotNullOrEmptyList("groupsNames", nonEmptyGroups);

            // return a list of users within the specified group
            var client = GetGroupsClient();
            IUser[] users;

            if (behaviour == QueryBehaviour.All)
                users = (IUser[]) await client.Execute(o => o.GetUsersInGroupsAsync(nonEmptyGroups));
            else
                users = (IUser[]) await client.Execute(o => o.GetUsersInAnyGroupsAsync(nonEmptyGroups));

            return users;
        }

        public static async Task<IUser[]> GetUsersInGroupsAsync(IEnumerable<string> groupNames)
        {
            return await GetUsersInGroupsAsync(groupNames, QueryBehaviour.All);
        }

        public static async Task<IUser[]> GetUsersInGroupAsync(string groupName)
        {
            // validate input parameters

            ParameterValidator.AssertIsNotNullOrWhiteSpace("groupName", groupName);

            return await GetUsersInGroupsAsync(new[] {groupName}, QueryBehaviour.All);
        }

        public static async Task<IEnumerable<string>> GetEmailAddressesFromActiveDirectoryGroupsAsync(string groupName)
        {
            return await GetEmailAddressesFromActiveDirectoryGroupsAsync(new[] {groupName});
        }

        public static async Task<IEnumerable<string>> GetEmailAddressesFromActiveDirectoryGroupsAsync(
            IEnumerable<string> groupNames)
        {
            // validate input parameters
            var parameterValue = groupNames as string[] ?? groupNames.ToArray();
            ParameterValidator.AssertIsNotNullOrEmptyList("groupNames", parameterValue);

            // return a list of non-null email addresses for the members of the specified groups
            var users = await GetUsersInGroupsAsync(parameterValue, QueryBehaviour.Any);

            return users.Select(o => o.Email.Trim()).Where(o => !string.IsNullOrEmpty(o));
        }

        #endregion

        #region Private Helper Methods

        private static IServiceProxy<IGroups> GetGroupsClient()
        {
            var uri = new Uri(GlobalConfigurationSettings.MossySettings[ApplicationSettings.OfaGroupServiceUrl]);
            var proxy = new GenericServiceProxy<IGroups, GroupsClient>(uri);

            return proxy;
        }


        private static IServiceProxy<IUsers> GetUsersClient()
        {
            var uri = new Uri(GlobalConfigurationSettings.MossySettings[ApplicationSettings.OfaProfileServiceUrl]);
            var proxy = new GenericServiceProxy<IUsers, UsersClient>(uri);

            return proxy;
        }

        private static string GetActiveDirectoryPropertyValue(string filterField, string filterValue,
            string propertyToRetrieve)
        {
            string result = null;

            using (var directoryEntry = new DirectoryEntry("LDAP://OFINA"))
            {
                using (var directorySearcher = new DirectorySearcher(directoryEntry))
                {
                    directorySearcher.Filter = string.Format(CultureInfo.InvariantCulture, "({0}={1})", filterField,
                        filterValue);
                    directorySearcher.PropertiesToLoad.Add(propertyToRetrieve);

                    var searchResult = directorySearcher.FindOne();

                    if (searchResult != null) result = searchResult.Properties[propertyToRetrieve][0].ToString();
                }
            }

            return result;
        }

        #endregion
    }
}