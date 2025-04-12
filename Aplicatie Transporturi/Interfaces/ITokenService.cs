using Aplicatie_Transporturi.Entities;
namespace Aplicatie_Transporturi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}