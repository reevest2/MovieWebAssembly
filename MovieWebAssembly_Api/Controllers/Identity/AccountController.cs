using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Identity;
using MovieWebAssembly_Api.Helper;

namespace MovieWebAssembly_Api.Controllers.Identity;

[ApiController]
[Route("[controller]/[action]")]
[Authorize]
public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApiSettings _apiSettings;

    public AccountController(SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<ApiSettings> options)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _apiSettings = options.Value;
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
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] AuthenticationDTO authenticationDTO)
    { 
        var result =
            await _signInManager.PasswordSignInAsync(authenticationDTO.UserName, authenticationDTO.Password, false,
                false);

        var errorMessage = new List<string> { "Invalid Authentication" };
        if (!result.Succeeded)
        {
            errorMessage.Add("Sign in failed");
            return Unauthorized(new AuthenticationResponseDTO
            {
                IsSuccessful = false,
                Errors = errorMessage
            });
        }

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(authenticationDTO.UserName);
            if (user == null)
            {
                errorMessage.Add("User not found");
                return Unauthorized(new AuthenticationResponseDTO
                {
                    IsSuccessful = false,
                    Errors = errorMessage
                });
            }

            var signinCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);

            var tokenOptions = new JwtSecurityToken(
                issuer: _apiSettings.ValidIssuer,
                audience: _apiSettings.ValidAudience,
                claims: claims,
                expires: DateTime.Now,
                signingCredentials: signinCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedToken = tokenHandler.WriteToken(tokenOptions);

            return Ok(new AuthenticationResponseDTO
            {
                IsSuccessful = true,
                Token = encodedToken,
                UserDTO = new UserDTO
                {
                    Name = user.UserName,
                    Id = user.Id,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                }
                
            });
        }

        errorMessage.Add("Token error");
        return Ok(new AuthenticationResponseDTO
        {
            
          IsSuccessful  = false,
          Errors = errorMessage
        });
    }


    private SigningCredentials GetSigningCredentials()
    {
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("Id", user.Id)
        };
        var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));

        claims.AddRange(roles.Select(role => (new Claim(ClaimTypes.Role, role))));

        return claims;
    }
}