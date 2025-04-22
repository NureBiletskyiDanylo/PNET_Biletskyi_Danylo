using BookCatalog_API.Entities;

namespace BookCatalog_API.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}
