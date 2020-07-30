using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace NullHttpContextRepro.Tests
{
    public class HttpContextTest
    {
        [Fact]
        public void TestHttpContext()
        {
            // arrange
            using var app = new WebApplicationFactory<Startup>();
            var httpContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();

            // act
            var httpContext = httpContextAccessor.HttpContext;

            // assert
            httpContext.Should().NotBeNull();
        }

        [Fact]
        public async Task TestHttpContextWithDummyController()
        {
            // arrange
            // need to integrate DummyController into the website
            using var app = new WebApplicationFactory<Startup>();
            HttpClient client = app.CreateClient();

            // act
            HttpResponseMessage response = await client.GetAsync("dummy");
            string content = await response.Content.ReadAsStringAsync();

            // assert
            response.IsSuccessStatusCode.Should().BeTrue();
            content.Should().Be(DummyController.SuccessMessage);
        }

    }

    [ApiController]
    [Route("[controller]")]
    public class DummyController : Controller
    {
        public const string SuccessMessage = nameof(SuccessMessage);
        [HttpGet]
        public string Get() => SuccessMessage;
    }
}