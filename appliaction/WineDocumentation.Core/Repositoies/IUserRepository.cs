using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WineDocumentation.Core.Domain;

namespace WineDocumentation.Core.Repositoies
{
    public interface IUserRepository : IRepository
    {
        Task<User> GetAsync(Guid id); 
        Task<User> GetAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task RemoveAsync(Guid id);
    }
}