using Microsoft.AspNetCore.Identity;

namespace Examen_2_Lenguajes.Entity
{
    public class UserEntity : IdentityUser
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual IEnumerable<PartidaEntity> Partida
        { get; set; }

    }
}
