using HussieniSuperMarket.Models;


namespace HussieniSuperMarket.Services.Interface
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser,IEnumerable<string>roles);
    }
}
