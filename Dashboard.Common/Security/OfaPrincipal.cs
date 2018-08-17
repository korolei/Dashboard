using System;
using System.Security.Principal;
using System.Threading;

namespace Dashboard.Common.Security
{
	[Serializable]
	public sealed class OfaPrincipal : IPrincipal
	{
		#region Constructors
		
		private OfaPrincipal(IIdentity identity)
		{
			Identity = identity;
		}

		#endregion

		#region IPrincipal Implementation

		public IIdentity Identity { get; private set; }

		public bool IsInRole(string role)
		{
			bool result = false;

			OfaIdentity identity = (OfaIdentity)this.Identity;
			result = identity.IsInRole(role);

			return result;
		}

		#endregion

		#region Public Methods

		public static void LogOn(IIdentity identity)
		{
            OfaIdentity customIdentity = OfaIdentity.GetIdentity(identity);
			OfaPrincipal customPrincipal = new OfaPrincipal(customIdentity);

			SetPrincipal(customPrincipal);
		}

        public static OfaPrincipal GetPrincipal(string networkName)
        {
            OfaIdentity customIdentity = OfaIdentity.GetIdentityAsync(networkName).Result;
            return new OfaPrincipal(customIdentity);
        }

        public static void LogOnFromEnvironment()
        {
            using (WindowsIdentity identity = new WindowsIdentity(Environment.UserName))
            {
                LogOn(identity);
            }
        }

		public static void SetPrincipal(OfaPrincipal principal)
		{
			if (principal != null && principal.Identity.IsAuthenticated)
			{
				//if (HttpContext.Current != null)
				//{
				//	HttpContext.Current.User = principal;
				//}

				Thread.CurrentPrincipal = principal;
			}
		}

		#endregion
	}
}