using BulletinBoard.DataAccess;
using BulletinBoard.DataAccess.Entities;
using BulletinBoard.Service.UnitTests.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Moq;
using NUnit.Framework;

namespace BulletinBoard.Service.UnitTests;

public class BulletinBoardServiceTestsBaseClass
{
    public BulletinBoardServiceTestsBaseClass()
    {
        var settings = TestSettingsHelper.GetSettings();

        _testServer = new TestWebApplicationFactory(services =>
        {
            services.Replace(ServiceDescriptor.Scoped(_ =>
            {
                var httpClientFactoryMock = new Mock<IHttpClientFactory>();
                httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
                    .Returns(TestHttpClient);
                return httpClientFactoryMock.Object;
            }));
            services.PostConfigureAll<JwtBearerOptions>(options =>
            {
                options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    $"{settings.IdentityServerUri}/.well-known/openid-configuration",
                    new OpenIdConnectConfigurationRetriever(),
                    new HttpDocumentRetriever(TestHttpClient) //important
                    {
                        RequireHttps = false,
                        SendAdditionalHeaderData = true
                    });
            });
        });
    }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var UserRepository = scope.ServiceProvider.GetRequiredService<IRepository<UserEntity>>();
        var User = UserRepository.Save(new UserEntity()
        {
            Title = "test"
        });
        TestUserId = User.Id;
    }

    public T? GetService<T>()
    {
        return _testServer.Services.GetRequiredService<T>();
    }

    private readonly WebApplicationFactory<Program> _testServer;
    protected int TestUserId;
    protected HttpClient TestHttpClient => _testServer.CreateClient();
}