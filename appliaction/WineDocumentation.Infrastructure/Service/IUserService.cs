using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WineDocumentation.Infrastructure.DTO;

namespace WineDocumentation.Infrastructure.Service
{
    public interface IUserService : IService
    {
        Task<UserDto> GetAsync(string email);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task RegisterAsync(Guid userId, string email, string username, string password, string role);
        Task UpdateAsync(Guid userId, string email, string username, string password, string role);
        Task<UserDto> LoginAsync(string email, string password);
        Task DeleteAsync(Guid userId);

    }
}