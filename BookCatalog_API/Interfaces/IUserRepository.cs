using BookCatalog_API.Entities;

namespace BookCatalog_API.Interfaces;

public interface IUserRepository
{
    Task<bool> AddUserAsync(User user);
    Task<bool> RemoveUserAsync(User user);
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserByEmailAsync(string email);
    Task<bool> UpdateUser(User user);
}
