using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineDocumentation.Infrastructure.Service;
using WineDocumentation.Infrastructure.DTO;
using WineDocumentation.Infrastructure.Commands.Users;
using System.Collections.Generic;
using WineDocumentation.Infrastructure.Commands.Wines;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WineDocumentation.Api.Controllers 
{
    [Route("[controller]")]
    public class WinesController : Controller 
    {
        
        private readonly IWineService _wineService;

        public WinesController(IWineService wineService, IHttpContextAccessor httpContextAccessor)
        {
            _wineService = wineService;
        }

        public IActionResult Wines()
        {
            if (CheckUser())
            {
                return View("All");
            }

            return RedirectToAction("Index", "Home");
        }

        [Route("name/{winename}")]
        [HttpGet]
         public async Task<IEnumerable<WineDto>> Get(string winename)
            => await _wineService.GetByNameAsync(winename);


        [Route("id/{wineid}")]
        [HttpGet]
         public async Task<ActionResult> GetById(string wineid)
         {
            if (CheckUser())
            {
                var wine = await _wineService.GetAsync(Guid.Parse(wineid));
                ViewBag.Message = wine;
                return View("Wine");    
            }

            return RedirectToAction("Index", "Home");
         }

        [Route("idt/{wineid}")]
        [HttpGet]
        public async Task<WineDto> GetByIdTest(string wineid)
        {
            var wine = await _wineService.GetAsync(Guid.Parse(wineid));
            if (wine != null)
            {
                return wine;
            }
            return null;
        }

        [Route("all")]
        [HttpGet]
        
        public async Task<ActionResult> All()
        {
            if (CheckUser())
            {
                var wines = await _wineService.GetAllAsync();
                ViewBag.Message = wines;
                return View();
            }

            return RedirectToAction("Index", "Home");

           
        }

        [HttpPost("delete")]
        public async Task Delete(DeleteWine request)
        {
            await _wineService.DeleteAsync(Guid.Parse(request.Id));
        }

        [HttpPost("Comment")]        
        public async Task<ActionResult> Comment(AddComment request)
        {
            await _wineService.NewComment(Guid.Parse(request.WineId) ,request.ScoreValue, request.Comment, request.Author);
            return RedirectToAction($"Id", "Wines", new {Id = request.WineId} );
        }


        [Route("Add")]
        [HttpGet]
        public ActionResult Add()
        {
            if (CheckUser())
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("addwine")]
        public async Task<IActionResult> Post(CreateWine request)
        {
            await _wineService.CreateAsync(
                Guid.NewGuid(), 
                request.Winename, 
                request.Brand, 
                new Core.Domain.Species(request.Speciename), 
                request.Description);

            return RedirectToAction("all", "Wines");
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> PostAddWine([FromBody]CreateWine request)
        {
            await _wineService.CreateAsync(
                Guid.NewGuid(),
                request.Winename,
                request.Brand,
                new Core.Domain.Species(request.Speciename),
                request.Description);

            return RedirectToAction("all", "Wines");
        }

        private bool CheckUser()
        {
            var user = (HttpContext.User.Identity as ClaimsIdentity);

            if (user.Name != null)
            {
                return true;
            }

            return false;
        }
    }
}