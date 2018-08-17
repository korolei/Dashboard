using System;
using System.Security.Principal;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;

namespace Dashboard.Common.Security
{
    public interface IPrincipalHolder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IPrincipal GetPrincipal();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        OfaPrincipal GetCachedPrincipal();
        void CachePrincipal(IPrincipal principal);
    }

    public class ThreadPrincipalHolder : IPrincipalHolder
    {
        private readonly IMemoryCache _cache;
        private const string CACHE_KEY = "OfaSecurityPrincipalCache";

        public ThreadPrincipalHolder(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public IPrincipal GetPrincipal()
        {
            return Thread.CurrentPrincipal;
        }

        public OfaPrincipal GetCachedPrincipal()
        {
            var principalName = GetPrincipalName(GetPrincipal());
            OfaPrincipal principal = null;
            _cache.TryGetValue(CACHE_KEY, out string cacheValue);

            if (cacheValue.Contains(principalName))
                principal = (OfaPrincipal)_cache.Get(principalName);

            return principal;
        }

        private static string GetPrincipalName(IPrincipal principal)
        {
            return principal.Identity.Name;
        }

        public void CachePrincipal(IPrincipal principal)
        {
            var principalName = GetPrincipalName(principal);
            _cache.TryGetValue(CACHE_KEY, out string cacheValue);

            if (!cacheValue.Contains(principalName))
                _cache.Set(principalName, principal, DateTime.Now.AddMinutes(5));
        }
    }
}
