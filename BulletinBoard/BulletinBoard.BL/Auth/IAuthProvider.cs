using BulletinBoard.BL.Auth.Entities;

namespace BulletinBoard.BL.Auth;

public interface IAuthProvider
{
    Task<TokensResponse> AuthorizeUser(string email, string password);
    Task<TokensResponse> RegisterUser(string email, string password);
}