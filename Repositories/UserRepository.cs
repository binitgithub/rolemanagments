using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rolemanagments.Data;
using rolemanagments.Models;

namespace rolemanagments.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RoleDbContext roleDbContext;

        public UserRepository(RoleDbContext roleDbContext)
        {
            this.roleDbContext = roleDbContext;
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
           return await roleDbContext.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task RegisterUserAsync(User user, string password)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            roleDbContext.Users.Add(user);
            await roleDbContext.SaveChangesAsync();
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await GetUserByUsernameAsync(username);
            return user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }
    }
}