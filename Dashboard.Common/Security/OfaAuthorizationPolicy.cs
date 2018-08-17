using Microsoft.AspNetCore.Authorization;

namespace Dashboard.Common.Security
{
    public static class OfaAuthorizationPolicy
    {
        private static AuthorizationPolicy _requireWindowsProviderPolicy;

        public static AuthorizationPolicy GetRequireWindowsProviderPolicy()
        {
            if (_requireWindowsProviderPolicy != null) return _requireWindowsProviderPolicy;

            _requireWindowsProviderPolicy = new AuthorizationPolicyBuilder()
                .RequireClaim("http://schemas.microsoft.com/identity/claims/identityprovider", "Windows")
                .Build();

            return _requireWindowsProviderPolicy;
        }
    }
}