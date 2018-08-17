using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Dashboard.Facade
{
    public class FacadeBase
    {
        protected FacadeBase()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        protected static async Task<T> ExecuteAsync<T>(IRestClient client,RestRequest request)
        {
            
            var response = await client.ExecuteTaskAsync<T>(request);
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var ex = new ApplicationException(message, response.ErrorException);
                throw ex;
            }
            return response.Data;
        }
    }
}