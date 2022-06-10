using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WineDocumentation.Core.Domain;

namespace WineDocumentation.Core.Repositoies
{
    public interface IWineRepository : IRepository
    {
        Task<Wine> GetAsync(Guid wineId);
        Task<Wine> GetAsync(Wine wine);
        Task<IEnumerable<Wine>> GetAllAsync();
        Task AddAsync(Wine wine);
        Task UpdateAsync(Wine wine);
        Task DeleteAsync(Wine wine);
        Task DeleteAsync(Guid wineId);
        Task AddScore(Score score);
    }
}