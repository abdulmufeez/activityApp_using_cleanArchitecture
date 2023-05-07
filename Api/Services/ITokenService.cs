using Domain;

namespace Api.Services
{
    public interface ITokenService
    {
        string CreateToken (AppUser user);        
    }
}