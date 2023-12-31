using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Identity;

namespace MovieWebAssembly_Api.Controllers.Identity;

[ApiController]
[Route("[controller]/[action]")]
[Authorize]
public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] UserRequestDTO userRequestDTO)
    {
        if (userRequestDTO == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var user = new IdentityUser
        {
            UserName = userRequestDTO.Email,
            Email = userRequestDTO.Email,
            PhoneNumber = userRequestDTO.PhoneNumber,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, userRequestDTO.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new RegistrationResponseDTO
            {
                Errors = errors, 
                IsRegistrationSuccessful = false
            });
        }

        var roleResult = await _userManager.AddToRoleAsync(user, "user");

        if (!roleResult.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new RegistrationResponseDTO
            {
                Errors = errors,
                IsRegistrationSuccessful = false
            });
        }

        return StatusCode(201);
    }
}