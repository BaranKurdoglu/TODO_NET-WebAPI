using dotnetDeneme.Dtos.Account;
using dotnetDeneme.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnetDeneme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                // IdentityUser kütüphanesi Succeeded döndüğü için bir değişkene atamalıyız.

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User"); // Her eklenen kullanıcıyı User rolüne tanımlar.

                    if (roleResult.Succeeded)
                    {
                        return Ok("User created.");
                    }
                    else
                    {
                        return BadRequest(roleResult.Errors);
                    }
                }

                else
                {
                    return BadRequest(createdUser.Errors);
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
