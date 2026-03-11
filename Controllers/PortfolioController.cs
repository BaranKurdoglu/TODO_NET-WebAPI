using dotnetDeneme.Extensions;
using dotnetDeneme.Interfaces;
using dotnetDeneme.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnetDeneme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolioAsync(appUser);

            return Ok(userPortfolio);
        }



        [HttpPost]
        [Authorize]

        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.AddBySymbolAsync(symbol);

            if (stock is null)
                return BadRequest("Stock not found.");


            var userPortfolio = await _portfolioRepo.GetUserPortfolioAsync(appUser);

            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
                return BadRequest("Cannot add same stock to portfolio !");


            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };

            await _portfolioRepo.CreatePortfolioAsync(portfolioModel);


            if (portfolioModel is null)
                return BadRequest("Could not create.");

            else
            {
                return Created();
            }
        }
    }
}

//UserManager; Microsoft.AspNetCore.Identity kütüphanesinden bir Class'tır.