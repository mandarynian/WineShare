using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WineDocumentation.Core.Domain;
using WineDocumentation.Core.Repositoies;
using WineDocumentation.Infrastructure.EF;

namespace WineDocumentation.Infrastructure.Repository
{
    public class WineRepository : IWineRepository
    {
        private readonly WineDocumentationContex _context;

        public WineRepository(WineDocumentationContex contex)
        {
            _context = contex;
        }

        public async Task AddAsync(Wine wine)
        {
            await _context.Wines.AddAsync(wine);
            await _context.SaveChangesAsync();
        }

       
        public async Task DeleteAsync(Wine wine)
        {
            var wineToDelete = await GetAsync(wine);
            _context.Wines.Remove(wineToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid wineId)
        {
            var wineToDelete = await GetAsync(wineId);
            _context.Wines.Remove(wineToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Wine> GetAsync(Guid wineId)
        {
            var wine = await _context.Wines.SingleOrDefaultAsync(x => x.Id == wineId);
            if (wine != null)
            {
                wine.Species = _context.Species.FirstOrDefault(s => s.Id == wine.SpeciesId);
                 var scores = _context.Scores
                    .Select(s => s)
                    .Where(scor => scor.WineId == wineId)
                    .ToList();
                        
                if (scores.Count > 0)
                {
                    wine.AddScores(scores);
                }
            }

            return wine;
        }

        public async Task<Wine> GetAsync(Wine wine)
        {
            var wineFromDb = await _context.Wines.SingleOrDefaultAsync(w => w.Id == wine.Id);
            if (wineFromDb != null)
            {
                wineFromDb.Species = _context.Species.FirstOrDefault(s => s.Id == wine.SpeciesId);
                var scores = _context.Scores
                    .Select(s => s)
                    .Where(scor => scor.WineId == wineFromDb.Id)
                    .ToList();
                        
                if (scores.Count > 0)
                {
                    wineFromDb.AddScores(scores);
                }
            }

            return wineFromDb;
        }
            

        public async Task<IEnumerable<Wine>> GetAllAsync()
        {
            var wines = await _context.Wines.ToListAsync();
        
            if (wines != null)
            {
                wines.ForEach(w => {
                    w.Species = _context.Species.FirstOrDefault(s => s.Id == w.SpeciesId);
                    var scores = _context.Scores
                        .Select(s => s)
                        .Where(scor => scor.WineId == w.Id)
                        .ToList();

                    if (scores.Count > 0)
                    {
                        w.AddScores(scores);
                    }
                });
            }
                
            return wines;
        }

        public async Task AddScore(Score score)
        {
            await _context.Scores.AddAsync(score);
             await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Wine wine)
        {
            _context.Wines.Update(wine);
            await _context.SaveChangesAsync();
        }
    }
}