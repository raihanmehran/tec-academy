using backend.Entities;

namespace backend.Interfaces
{
    public interface ITokenRepository
    {
        Task<string> CreateToken(AppUser user);
    }
}