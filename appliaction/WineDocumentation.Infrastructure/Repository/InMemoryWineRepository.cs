using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineDocumentation.Core.Domain;
using WineDocumentation.Core.Repositoies;

namespace WineDocumentation.Infrastructure.Repository
{
    public class InMemoryWineRepository : IWineRepository
    {
        private static readonly ISet<Wine> _wines = new HashSet<Wine>()
        {
            new Wine(Guid.Parse("4fb8234b-f401-4f86-9744-ac5f1789a4c1"),
                "Leśny Dzban", 
                new Species("Tanie wino ", "Rudawy taki", "Dla koneserów najlepszego smaku"), 
                "Pan Zdzisek", 
                "Wino najlepszej jakości na jaką Cię stać :)" 
            ),
             new Wine(Guid.NewGuid(), 
                "Jabuszko", 
                new Species("Tanie wino", "Złotawy (Mętny Złoty)", "Dla koneserów i dbających o portfel"), 
                "Sandomierskie smaki", 
                "Wino z piwniczki Ojca Mateusza" 
            ),
             new Wine(Guid.NewGuid(), 
                "Kwiat Jabłoni", 
                new Species("Tanie wino", "Czerwonawy", "Dla lubiących dobrą muzykę"), 
                "Katarzyna i Jacek Sienkiewicz", 
                "Rozśpiewany smak" 
            ),
             new Wine(Guid.NewGuid(), 
                "WSW", 
                new Species("Tanie wino", "Biały", "Dla szukających mocnych wrażeń"), 
                "Warka", 
                "Trzeciego nikt nie dopił" 
            ),
            new Wine(Guid.NewGuid(), 
                "Barefoot", 
                new Species("Pół wytrawne wino różowe", "Różowe", ""), 
                "Barefoot", 
                "O świeżym aromacie truskawek i ananasów idealne do sałatek owocowych i warzywnych, delikatnych makaronów owoców morza i sushi. Wspaniale rześkie." 
            )                             
        };
        public async Task AddAsync(Wine wine)
        {
            _wines.Add(wine);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Wine wine)
        {
            _wines.Remove(wine);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid wineId)
        {
            var tempWine = await GetAsync(wineId);
            await DeleteAsync(tempWine);
        }

        public async Task<Wine> GetAsync(Guid wineId)
            => await Task.FromResult(_wines.FirstOrDefault(w => w.Id == wineId));

        public async Task<Wine> GetAsync(Wine wine)
            => await Task.FromResult(_wines.FirstOrDefault(w => w.Id == wine.Id));

        public async Task<IEnumerable<Wine>> GetAllAsync()
            => await Task.FromResult(_wines);
        

        public async Task UpdateAsync(Wine wine)
            => await Task.CompletedTask;

        public Task AddScore(Score score)
        {
            throw new NotImplementedException();
        }
    }
}