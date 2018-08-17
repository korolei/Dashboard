using Dashboard.Facade.BorrowingProfile;
using Dashboard.Models.BorrowingProfile.Settings;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace Dashboard.Facade
{
    public static class Configure
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBorrowingProfileFacade, BorrowingProfileFacade>();
            services.AddSingleton<IBorrowingProfileSettings, BorrowingProfileSettings>();
            services.AddSingleton<IBorrowingProfileClientSettings, BorrowingProfileClientSettings>();
            services.AddSingleton<IRestClient, RestClient>();
        }
    }
}