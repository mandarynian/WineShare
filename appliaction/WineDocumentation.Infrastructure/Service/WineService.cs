using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using WineDocumentation.Core.Domain;
using WineDocumentation.Core.Repositoies;
using WineDocumentation.Infrastructure.DTO;

namespace WineDocumentation.Infrastructure.Service
{
    public class WineService : IWineService
    {
        private IValidationService _validationService;
        private readonly IWineRepository _wineRepository;
        private readonly IMapper _mapper;   

        public WineService(
            IWineRepository wineRepository, 
            IMapper mapper
        )
        {
            _validationService = new ValidationService();
            _wineRepository = wineRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(
            Guid wineId, 
            string winename, 
            string brand, 
            Species species, 
            string description = ""
        )
        {
            try 
            {
                var wine = await  _wineRepository.GetAsync(wineId);
                if (wine != null)
                {
                    throw new Exception("Wine with this ID is aleready exists.");
                }

                var nameVaidation = await  _validationService.NameVlidate(winename);

                if (!nameVaidation)
                {
                    throw new Exception("Name is not valid!");
                }

                wine = new Wine(wineId, winename, species, brand, description);

                await _wineRepository.AddAsync(wine);
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
            
        }

        public async Task<IEnumerable<WineDto>> GetByNameAsync(string winename)
        {
            try
            {
                var wines = await _wineRepository.GetAllAsync();

                var selectedWines = wines.Select(w => w)
                    .Where(w => w.Winename == winename);

                return _mapper.Map<IEnumerable<Wine>, IEnumerable<WineDto>>(selectedWines);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task DeleteAsync(Guid wineId)
        {
            var wine = await _wineRepository.GetAsync(wineId);
            await _wineRepository.DeleteAsync(wine);
        }

        public async Task<IEnumerable<WineDto>> GetAllAsync()
        {
            var wines = await _wineRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Wine>, IEnumerable<WineDto>>(wines);
        }

        public async Task<WineDto> GetAsync(Guid wineId)
        {
            var wine = await _wineRepository.GetAsync(wineId);
            return _mapper.Map<Wine, WineDto>(wine);
        }

        public async Task NewComment(Guid wineId, string scoreValue, string comment, string author)
        {
            var wine  = await _wineRepository.GetAsync(wineId);

            if (wine == null)
            {
                throw new Exception("Wine is not find!");
            }

            _wineRepository.AddScore(
                new Score(Convert.ToUInt32(scoreValue), comment, author, wine.Id)
            ).Wait();
        }

        // public Task SetWineAsync(Guid wineId, string winename, string brand, Species species, string description = "")
        // {
        //     throw new NotImplementedException();
        // }
    }
}