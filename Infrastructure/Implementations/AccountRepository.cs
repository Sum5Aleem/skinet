using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly StoreContext _context;
        public AccountRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<User> CheckEmailExists(string email)
        {
            return await _context.Users.Where(p => p.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetCurrentUser(string email)
        {
            return await _context.Users.Include(x => x.Address).Where(p => p.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> Login(string email, string password)
        {
            return await _context.Users.Where(p => p.Email == email && p.Password == password).FirstOrDefaultAsync();
        }
        public async Task<User> Register(User user)
        {
            _context.Users.Add(user);
            var created = await _context.SaveChangesAsync();

            if (created == 0) return null;
            return user;
        }

        public async Task<User> UpdateUserAddress(User user)
        {
            _context.Users.Update(user);
            var created = await _context.SaveChangesAsync();

            if (created == 0) return null;
            return user;
        }
    }
}
