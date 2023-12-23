using System.Net;
using BulletinBoard.BL.Auth.Entities;
using BulletinBoard.DataAccess;
using BulletinBoard.DataAccess.Entities;
using FluentAssertions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;

namespace BulletinBoard.Service.UnitTests.Users.Authorization;

public class LoginUserTests : BulletinBoardServiceTestsBaseClass
{
    [Test]
    public async Task SuccessFullResult()
    {
        // prepare: create new user (login, password) => execute (try to login) => assert (Success : access token, refresh token)
        //prepare
        var user = new UserEntity()
        {
            Email = "test@test",
            UserName = "test@test",
            FirstName = "test",
            SecondName = "test",
            Patronymic = "test",
        };
        var password = "Password1@";

        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
        var result = await userManager.CreateAsync(user, password);

        var query = $"?email={user.UserName}&password={password}";
        var requestUri =
            BulletinBoardApiEndpoints.AuthorizeUserEndpoint + query;
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var client = TestHttpClient;
        var response = await client.SendAsync(request);

        var responseContentJson = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<TokensResponse>(responseContentJson);

        content.Should().NotBeNull();
        content.AccessToken.Should().NotBeNull();
        content.RefreshToken.Should().NotBeNull();

        var requestToGetAllUsers =
            new HttpRequestMessage(HttpMethod.Get, BulletinBoardApiEndpoints.GetAllUsersEndpoint);

        var clientWithToken = TestHttpClient;
        client.SetBearerToken(content.AccessToken);
        var getAllUsersResponse = await client.SendAsync(requestToGetAllUsers);

        getAllUsersResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    public async Task BadRequestUserNotFoundResultTest()
    {
        var login = "not_existing@mail.ru";
        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<UserEntity>>();
        var user = userRepository.GetAll().FirstOrDefault(x => x.UserName.ToLower() == login.ToLower());
        if (user != null)
        {
            userRepository.Delete(user);
        }

        var password = "password";

        var query = $"?email={login}&password={password}";
        var requestUri = BulletinBoardApiEndpoints.AuthorizeUserEndpoint + query;
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var response = await TestHttpClient.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task PasswordIsIncorrectResultTest()
    {
        var user = new UserEntity()
        {
            Email = "test@test",
            UserName = "test@test",
        };
        var password = "password";

        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var userManager = scope.ServiceProvider.GetService<UserManager<UserEntity>>();
        await userManager.CreateAsync(user, password);

        var incorrect_password = "qwaszx2502";

        var query = $"?email={user.UserName}&password={incorrect_password}";
        var requestUri =
            BulletinBoardApiEndpoints.AuthorizeUserEndpoint + query; // /auth/login?login=test@test&password=kvhdbkvhbk
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var client = TestHttpClient;
        var response = await client.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest); // with some message
    }

    [Test]
    [TestCase("", "")]
    [TestCase("qwe", "")]
    [TestCase("test@test", "")]
    [TestCase("", "password")]
    public async Task LoginOrPasswordAreInvalidResultTest(string login, string password)
    {
        var query = $"?login={login}&password={password}";
        var requestUri =
            BulletinBoardApiEndpoints.AuthorizeUserEndpoint + query; // /auth/login?login=test@test&password=kvhdbkvhbk
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var client = TestHttpClient;
        var response = await client.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest); // with some message
    }