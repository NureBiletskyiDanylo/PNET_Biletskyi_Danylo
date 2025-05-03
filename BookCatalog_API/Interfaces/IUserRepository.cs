using BookCatalog_API.DTOs;
using BookCatalog_API.Entities;

namespace BookCatalog_API.Interfaces;

public interface IUserRepository
{
    Task<bool> AddUserAsync(User user);
    Task<bool> RemoveUserAsync(User user);
    Task<User> GetUserByIdAsync(int id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> UserExistsAsync(string email);
    Task<List<MemberDto>> GetAllUsersAsync();
    Task<bool> RemoveUserByIdAsync(int id);
    Task<bool> UpdateUserRoleAsync(int id, string role);

}
