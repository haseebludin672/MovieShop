using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        private IConfiguration _configuration;
        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }


        [HttpPost]
        [Route("register")] 
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = await _accountService.CreateUser(model);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (user == null) return BadRequest();
            return Ok(user);

        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //check the email/pw


            var user = await _accountService.ValidateUser(model.Email, model.Password);
            if (user == null)
            {
                return Unauthorized(new { error = "please verify email/password" });
            }

            // we need to create the JWT using the claims info and secret key

            var token = GenerateToken(user);
            return Ok(new { token = token });
        }

        private string GenerateToken(LogedInResponseModel user)
        {
            // create the claims
            var claims = new List<Claim>
            {
                new Claim( ClaimTypes.NameIdentifier, user.Id.ToString() ),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ),
                new Claim(JwtRegisteredClaimNames.Birthdate, user.DateOfBirth.ToShortDateString() ),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ),
                new("Language", "en")
            };
            // add any roles to the above claims 

            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

  
            
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            // create the token with a secret signature
            // expiration time
            //to read the key we use the line code below 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var expirationTime = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("ExpirationHours"));

            var tokenHandler = new JwtSecurityTokenHandler();

            // decribe the contents of the token

            var token = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = expirationTime,
                SigningCredentials = credentials,
                Issuer = _configuration["Issuer"],
                Audience = _configuration["Audience"]
            };

            var encodedJWT = tokenHandler.CreateToken(token);
            return tokenHandler.WriteToken(encodedJWT);
        }
    }
}

