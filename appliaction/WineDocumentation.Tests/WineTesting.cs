using System.Net.Http;
using Newtonsoft.Json;
using System;
using Xunit;
using WineDocumentation.Infrastructure.DTO;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using FluentAssertions;
using WineDocumentation.Api;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WineDocumentation.Infrastructure.Commands.Wines;

namespace WineDocumentation.Tests
{
    public class WineTesting
    {
        private TestServer _server;
        private HttpClient _client;

        public WineTesting()
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>();

            _server = new TestServer(host); 
            _client = _server.CreateClient();   
            _server.Host.Start();
        }

        [Fact]
        public async Task get_valid_wine_by_id() 
        {
            var idwine = "4fb8234b-f401-4f86-9744-ac5f1789a4c1";
            var respond = await _client.GetAsync($"Wines/idt/{idwine}");
            Assert.NotNull(respond);
            var respoStr = await respond.Content.ReadAsStringAsync();
            var wines = JsonConvert.DeserializeObject<WineDto>(respoStr);
            wines.Id.ShouldBeEquivalentTo(Guid.Parse(idwine));
        }

        [Fact]
        public async Task get_invalid_wine_by_name()
        {
            var name = "Jabolek";
            var respond = await _client.GetAsync($"Wines/{name}");
            respond.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task get_all_windes()
        {
            var respond = await _client.GetAsync($"Wines/all");
            respond.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Redirect);
        }

        [Fact]
        public async Task add_wine()
        {

            var wine = new CreateWine()
            {
                Brand = "WINE FOLLY",
                Description = "Kolejne wino z serii białych win Zazano",
                Speciename = "Riesling",
                Winename = "Zazando"
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(wine), Encoding.UTF8, "application/json");
            var respond = await _client.PostAsync($"Wines/add", stringContent);
            respond.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Redirect);
        }

    }
}
