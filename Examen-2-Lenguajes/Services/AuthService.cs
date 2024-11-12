using Examen_2_Lenguajes.Database.Context;
using Examen_2_Lenguajes.Dto.Auth;
using Examen_2_Lenguajes.Dto.Common;
using Examen_2_Lenguajes.Entity;
using Examen_2_Lenguajes.Services.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Examen_2_Lenguajes.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<UserEntity> signInManager;
        private readonly UserManager<UserEntity> userManager;
        private readonly IConfiguration configuration;
        private readonly ILogger<AuthService> logger;
        private readonly PartidasDbContext context;

        public AuthService
           (
           SignInManager<UserEntity> signInManager,
           UserManager<UserEntity> userManager,
           IConfiguration configuration,
           ILogger<AuthService> logger,
           PartidasDbContext context
           )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
            this.logger = logger;
            this.context = context;
        }

        public ClaimsPrincipal GetTokenPrincipal (string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Secret").Value));

            var validation = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidateLifetime = false,
                ValidateActor = false,
                ValidateIssuer = false,
                ValidateAudience = false,
            };
            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }

        public async Task<ResponseDto<LoginResponseDto>> LoginAsync (LoginDto dto)
        {
            var result = await signInManager.PasswordSignInAsync(dto.Email, dto.Password, isPersistent: false, lockoutOnFailure:false);

            if (result.Succeeded) 
            {
                var userEntity = await userManager.FindByEmailAsync(dto.Email);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("userId", userEntity.Id)
                };

                var userRoles = await userManager.GetRolesAsync(userEntity);
                foreach(var role in userRoles) 
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                var jwtToken = (authClaims);
                return new ResponseDto<LoginResponseDto> 
                {
                   
                }
            }
    }
}
