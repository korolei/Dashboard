using System.Security.Principal;

namespace Dashboard.Common.Connected_Services.UserProfileService
{
    public partial class User : IIdentity, IUser
    {
        public string AuthenticationType => "NTLM";

        public bool IsAuthenticated => true;

        public string Name => NetworkName;
    }
}

namespace Dashboard.Common.Connected_Services.GroupIdentificationService
{
    public partial class User : IUser
    {
    }
}
