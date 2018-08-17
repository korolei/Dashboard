using System.Net.Http;
using Dashboard.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Dashboard.Tests.Web
{
    public class TestServerFixture
    {
        public TestServer Server { get; private set; }
        public HttpClient Client { get; private set; }

        public TestServerFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup(typeof(TestServerStartup));

            Server = new TestServer(builder);

            Client = Server.CreateClient();
        }       
    }
}