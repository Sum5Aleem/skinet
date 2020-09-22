using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAccountRepository
    {
        Task<User> Login(string email, string password);
        Task<User> Register(User user);
        Task<User> GetCurrentUser(string email);
        Task<User> CheckEmailExists(string email);
        Task<User> UpdateUserAddress(User user);
    }
}
