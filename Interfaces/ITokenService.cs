using dotnetDeneme.Models;

namespace dotnetDeneme.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
