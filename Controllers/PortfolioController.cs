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
        private readonly IPortfoiloRepository _portfoiloRepo;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo,IPortfoiloRepository portfoiloRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfoiloRepo = portfoiloRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfoilo()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfoilo = await _portfoiloRepo.GetUserPortfoilo(appUser);

            return Ok(userPortfoilo);
        }

    }
}
