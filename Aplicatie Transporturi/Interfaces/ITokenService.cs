using Aplicatie_Transporturi.Entities;

public interface ITokenService
{
    string CreateToken(AppUser user);
}