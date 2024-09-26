using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Authenticates a user and provides a JWT token.
    /// </summary>
    /// <param name="loginRequest">The login credentials (username and password).</param>
    /// <returns>A JWT token if the credentials are correct.</returns>
    /// <response code="200">Returns the JWT token if login is successful.</response>
    /// <response code="400">Returns an error if the login credentials are invalid.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Authenticate user", Description = "Generates a JWT token if the credentials are valid.")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Username == "testuser" && request.Password == "password123")
        {
            var token = GenerateJwtToken();
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }

    private string GenerateJwtToken()
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings.GetValue<string>("SecretKey");
        var issuer = jwtSettings.GetValue<string>("Issuer");
        var audience = jwtSettings.GetValue<string>("Audience");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "testuser"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtSettings.GetValue<int>("ExpiryInMinutes")),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

/// <summary>
/// Request model for user login.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// The username for login.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// The password for login.
    /// </summary>
    public string Password { get; set; }
}