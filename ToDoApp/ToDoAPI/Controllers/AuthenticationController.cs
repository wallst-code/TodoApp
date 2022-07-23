using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }

        public record AuthenticationData(string? UserName, string? Password);
        public record UserData(int Id, string FirstName, string LastName, string UserName);

        [HttpPost("token")]
        [AllowAnonymous] // allows this to run regardless of being logged in first.
        public ActionResult<string> Authenticate([FromBody] AuthenticationData data)
        {
            var user = ValidateCredentials(data);

            if (user is null)
            {
                return Unauthorized();
            }

            var token = GenerateToken(user);

            return Ok(token);
        }

        private string GenerateToken(UserData user)
        {
            var secretKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_config.GetValue<string>("Authentication:SecretKey")));

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new(0);
            claims.Add(new(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName));
            claims.Add(new(JwtRegisteredClaimNames.GivenName, user.FirstName));
            claims.Add(new(JwtRegisteredClaimNames.FamilyName, user.LastName));

            var audience = _config.GetValue<string>("Authentication:Audience");
            var token = new JwtSecurityToken(
                issuer:_config.GetValue<string>("Authentication:Issuer"),
                audience:_config.GetValue<string>("Authentication:Audience"),
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(1),
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private UserData? ValidateCredentials(AuthenticationData data)
        {
            // This is not production code - it needs to be an actual OAuth system.
            if (CompareValues(data.UserName, "tcorey") &&
                CompareValues(data.Password, "Test123"))
            {
                return new UserData(1, "Tim", "Corey", data.UserName!); // "!" at the end means that it is not null.
            }
            if (CompareValues(data.UserName, "sstorm") &&
               CompareValues(data.Password, "Test123"))
            {
                return new UserData(2, "Sue", "Storm", data.UserName!); // "!" at the end means that it is not null.
            }

            return null;
        }

        private bool CompareValues(string? actual, string? expected)
        {
            if (actual is not null)
            {
                if (actual.Equals(expected))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
