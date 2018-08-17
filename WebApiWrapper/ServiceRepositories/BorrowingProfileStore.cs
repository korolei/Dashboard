using System.ServiceModel;
using Ofa.Common.Services;
using Ofa.Common.Wcf.Client;
using WebApiWrapper.Connected_Services.BorrowingProfileProvider;

namespace WebApiWrapper.ServiceRepositories
{
    internal class BorrowingProfileStore : ServiceAgentBase<IBorrowingProfileProvider, BorrowingProfileProviderClient>
    {
        protected override BorrowingProfileProviderClient GetServiceClient()
        {
            var binding = WcfClientFactory.Create<IBorrowingProfileProvider, BorrowingProfileProviderClient>(Settings.Settings.BorrowingProfileProviderUrl, ServiceTrustLevel.Default);
            ((WSHttpBinding)binding.Endpoint.Binding).MaxReceivedMessageSize = 2147483647;
            return binding;
        }
    }
}