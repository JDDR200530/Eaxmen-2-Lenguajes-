using Examen_2_Lenguajes.Database.Context;
using Examen_2_Lenguajes.Dto.Auth;
using Examen_2_Lenguajes.Dto.Common;
using Examen_2_Lenguajes.Entity;
using Examen_2_Lenguajes.Services.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Examen_2_Lenguajes.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly PartidasDbContext _context;

        public AuthService(
            SignInManager<UserEntity> signInManager,
            UserManager<UserEntity> userManager,
            IConfiguration configuration,
            ILogger<AuthService> logger,
            PartidasDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        public ClaimsPrincipal GetTokenPrincipal(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidateLifetime = false,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateActor = false
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);
        }

        public async Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var userEntity = await _userManager.FindByEmailAsync(dto.Email);
                if (userEntity == null)
                {
                    return UnauthorizedResponse("Usuario no encontrado.");
                }

                var authClaims = await GetClaims(userEntity);
                var jwtToken = GenerateToken(authClaims);

                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Inicio de sesión satisfactorio",
                    Data = new LoginResponseDto
                    {
                        FullName = $"{userEntity.FirstName} {userEntity.LastName}",
                        Email = userEntity.Email,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        
                    }
                };
            }

            return UnauthorizedResponse("Error en el inicio de sesión");
        }

      

        public async Task<ResponseDto<LoginResponseDto>> RegisterAsync(RegisterDto dto)
        {
            var user = new UserEntity
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "USER");

                var authClaims = await GetClaims(user);
                var jwtToken = GenerateToken(authClaims);

                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Registro de usuario realizado satisfactoriamente",
                    Data = new LoginResponseDto
                    {
                        FullName = $"{user.FirstName} {user.LastName}",
                        Email = user.Email,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                       
                    }
                };
            }

            return new ResponseDto<LoginResponseDto>
            {
                StatusCode = 400,
                Status = false,
                Message = "Error al registrar el usuario"
            };
        }

        private JwtSecurityToken GenerateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JWT:Expires"] ?? "15")),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }

        private string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<List<Claim>> GetClaims(UserEntity userEntity)
        {
            var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, userEntity.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserId", userEntity.Id)
        };

            var userRoles = await _userManager.GetRolesAsync(userEntity);
            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            return authClaims;
        }

        private ResponseDto<LoginResponseDto> UnauthorizedResponse(string message)
        {
            return new ResponseDto<LoginResponseDto>
            {
                StatusCode = 401,
                Status = false,
                Message = message
            };
        }
    }

}
