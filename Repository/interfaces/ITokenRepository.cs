using Microsoft.AspNetCore.Identity;

namespace ContactManagementV2.Repository.interfaces
{
    public interface ITokenRepository
    {
        string CreateTokens(IdentityUser user,List<string> Role);
    }
}
