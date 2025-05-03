using AutoMapper;
using BookCatalog_API.DTOs;
using BookCatalog_API.Entities;
using BookCatalog_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace BookCatalog_API.Data.Repositories;

public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
    public async Task<bool> AddUserAsync(User user)
    {
        context.Users.Add(user);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<List<MemberDto>> GetAllUsersAsync()
    {
        var users = await context.Users.ToListAsync();
        List<MemberDto> members = mapper.Map<List<MemberDto>>(users);
        return members ?? new List<MemberDto>();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        User? user = await context.Users
            .FirstOrDefaultAsync(a => a.Email == email);
        return user;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        User? user = await context.Users.FindAsync(id);
        if (user == null) throw new ArgumentNullException("User was not found");
        return user;
    }

    public async Task<bool> RemoveUserAsync(User user)
    {
        context.Users.Remove(user);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveUserByIdAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user != null)
        {
            context.Users.Remove(user);
            return await context.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        context.Users.Update(user);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateUserRoleAsync(int id, string role)
    {
        var user = await context.Users.FindAsync(id);

        switch (role.ToLower())
        {
            case "user":
                user.Role = Enums.Roles.User;
                break;
            case "admin":
                user.Role = Enums.Roles.Admin;
                break;
            default:
                break;
        }

        return await context.SaveChangesAsync() > 0;
    }

    //public async Task<User> UserExistsAsync(string email)
    //{
    //    var user = await context.Users.FirstOrDefaultAsync(a => a.Email == email);
    //    return user;
    //}

    public async Task<bool> UserExistsAsync(string email)
    {
        var user = await context.Users.FirstOrDefaultAsync(a => a.Email == email);
        return user == null;
    }
}
