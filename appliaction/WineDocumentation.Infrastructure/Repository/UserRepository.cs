using System;
using WineDocumentation.Core.Domain;
using WineDocumentation.Core.Repositoies;
using System.Collections.Generic;
using System.Threading.Tasks;
using WineDocumentation.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace WineDocumentation.Infrastructure.Repository
{

    public class UserRepository : IUserRepository
    {
        private readonly WineDocumentationContex _context;


        public UserRepository(WineDocumentationContex contex)
        {
            _context = contex;
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<User> GetAsync(string email)
        {
            try 
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                return user;            
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var user = await GetAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }        
    }
}