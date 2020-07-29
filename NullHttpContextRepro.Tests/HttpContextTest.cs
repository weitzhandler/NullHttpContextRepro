using FluentAssertions;
using Microsoft.AspNetCore.Http;
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
    }
}