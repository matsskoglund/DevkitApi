using System;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using DevkitApi;

namespace DevkitApi.IntTest
{
    public class IntTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public IntTest()
        {
            _server = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>());
            _client = _server.CreateClient();
        }
    
        [Fact]
        public void Test1()
        {

        }
    }
}
