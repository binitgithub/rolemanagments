using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rolemanagments.Models;

namespace rolemanagments.Repositories
{
    public interface IUserRepository
    {
    Task<User> GetUserByUsernameAsync(string username);
    Task RegisterUserAsync(User user, string password);
    Task<bool> ValidateUserAsync(string username, string password);
    }
}