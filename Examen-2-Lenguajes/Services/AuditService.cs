using Examen_2_Lenguajes.Services.Intefaces;

namespace Examen_2_Lenguajes.Services
{
    public class AuditService : IAuditServices
    {
        private readonly IHttpContextAccessor httpContextAccessor1;

        public AuditService(IHttpContextAccessor httpContextAccessor1)
        {
            this.httpContextAccessor1 = httpContextAccessor1;
        }

        public string GetUserId()
        {
            var idClaim = httpContextAccessor1.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault();

            return idClaim.Value;
        }
    }
}
