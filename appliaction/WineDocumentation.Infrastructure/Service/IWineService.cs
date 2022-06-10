using System;
using System.Collections.Generic;
using WineDocumentation.Infrastructure.Repository;
using WineDocumentation.Core.Domain;
using WineDocumentation.Infrastructure.DTO;
using System.Threading.Tasks;

namespace WineDocumentation.Infrastructure.Service
{
    public interface IWineService : IService
    {
        Task<WineDto> GetAsync(Guid wineId);
        Task<IEnumerable<WineDto>> GetAllAsync();
        Task CreateAsync(Guid wineId, string winename, string brand, Species species, string description = "");
        //Task SetWineAsync(Guid wineId, string winename, string brand, Species species, string description = "");
        Task DeleteAsync(Guid wineId);
        Task<IEnumerable<WineDto>> GetByNameAsync(string name);

        Task NewComment(Guid wineId, string scoreValue, string comment, string author);

    }
}