
using Dashboard.Common.Security;

namespace Dashboard.Common
{
    public interface IUser : IDisplayableUser
    {
        string CanonicalName { get; set; }
        string Company { get; set; }
        string Department { get; set; }
        string DistinguishedName { get; set; }
        string Email { get; set; }
        string FaxNumber { get; set; }
        string GivenName { get; set; }
        string ManagerNetworkName { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        string[] MemberOf { get; set; }
        string MobileNumber { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        string[] OwnedGroups { get; set; }
        string Surname { get; set; }
        string TelephoneNumber { get; set; }
        string Title { get; set; }
    }
}
