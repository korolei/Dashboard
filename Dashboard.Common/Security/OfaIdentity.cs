using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Dashboard.Common.Utilities;
using Ofa.Common.Linq;

namespace Dashboard.Common.Security
{
    [Serializable]
    public sealed class OfaIdentity : IIdentity
    {
        #region Constructors

        private OfaIdentity(IIdentity identity)
        {
            Name = identity.Name;
            IsAuthenticated = identity.IsAuthenticated;
            AuthenticationType = identity.AuthenticationType;
        }

        #endregion

        #region Properties

        public string GivenName { get; private set; }
        public string Surname { get; private set; }
        public string FullName { get; private set; }
        public string Title { get; private set; }
        public string Department { get; private set; }
        public string PhoneNumber { get; private set; }
        public string EmailAddress { get; private set; }
        public string Manager { get; private set; }
        public bool IsManager { get; private set; }
        public Collection<string> DirectReports { get; private set; }

        private Collection<string> Roles { get; set; }

        #endregion

        #region IIdentity Implementation

        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }
        public string Name { get; }

        #endregion

        #region Internal Methods

        internal static async Task<OfaIdentity> GetIdentityAsync(string networkName)
        {
            var user = await ActiveDirectoryHelper.GetUserAsync(networkName);
            var directReports = await ActiveDirectoryHelper.GetDirectReportsAsync(networkName);

            var result = new OfaIdentity((IIdentity) user)
            {
                GivenName = user.GivenName,
                Surname = user.Surname,
                FullName = user.DisplayName,
                Title = user.Title,
                Department = user.Department,
                PhoneNumber = user.TelephoneNumber,
                EmailAddress = user.Email,
                Manager = PrincipalHelper.PrependDomain(user.ManagerNetworkName),
                DirectReports = directReports.Select(o => PrincipalHelper.PrependDomain(o.NetworkName)).ToCollection()
            };


            result.IsManager = result.DirectReports.Any();
            result.Roles = user.MemberOf.ToCollection();

            return result;
        }

        internal static OfaIdentity GetIdentity(IIdentity identity)
        {
            return GetIdentityAsync(identity.Name).Result;
        }

        internal bool IsInRole(string role)
        {
            return Roles.Any(o => role.Equals(o, StringComparison.InvariantCultureIgnoreCase));
        }

        #endregion
    }
}