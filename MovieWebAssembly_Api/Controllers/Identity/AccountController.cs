using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Identity;

namespace MovieWebAssembly_Api.Controllers.Identity;

[ApiController]
[Route("[controller]/[action]")]
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
                IsSuccessful = false
            });
        }

        var roleResult = await _userManager.AddToRoleAsync(user, "user");

        if (!roleResult.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new RegistrationResponseDTO
            {
                Errors = errors,
                IsSuccessful = false
            });
        }

        return StatusCode(201);
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout([FromBody] AuthenticationDTO authenticationDTO)
    {
        await _signInManager.SignOutAsync();
        return Ok(new { Message = "You have successfully logged out." });
    }
    

    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] AuthenticationDTO authenticationDTO)
    { 
        var result =
            await _signInManager.PasswordSignInAsync(authenticationDTO.UserName, authenticationDTO.Password, false,
                false);
        var message = "Sign In Unsuccessful";   
        
        if(result.Succeeded)
        {
            message =  "Sign In Successful";
        }

        return Ok(message);
    }
}