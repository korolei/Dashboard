using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace WebApiWrapper
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();
            config.MapHttpAttributeRoutes();

            //Web API routes
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                "ActionApi",
                "api/{controller}/{action}"
            );

            config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
        }

        public static void Configure(HttpConfiguration config)
        {
            //Formatters
            var jqueryFormatter = config.Formatters.FirstOrDefault(
                x => x.GetType() ==
                     typeof(JQueryMvcFormUrlEncodedFormatter));
            config.Formatters.Remove(
                config.Formatters.FormUrlEncodedFormatter);
            config.Formatters.Remove(jqueryFormatter);
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new StringEnumConverter() },
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };

            foreach (var formatter in config.Formatters)
            {
                formatter.RequiredMemberSelector =
                    new SuppressedRequiredMemberSelector();

            }
            //Default Services
            config.Services.Replace(
                typeof(IContentNegotiator),
                new DefaultContentNegotiator(true));
            config.Services.RemoveAll(
                typeof(ModelValidatorProvider),
                validator => !(validator
                    is DataAnnotationsModelValidatorProvider));
        }

        public class SuppressedRequiredMemberSelector
            : IRequiredMemberSelector
        {
            public bool IsRequiredMember(MemberInfo member)
            {
                return false;
            }
        }
    }
}
