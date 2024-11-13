using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Examen_2_Lenguajes.Entity
{
    public class UserEntity : IdentityUser
    {
        [Key]
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        [Column("refresh_token")]
        [StringLength(450)]
        public string RefreshToken { get; set; }

        [Column("refresh_token_expire")]
        public DateTime RefreshTokenExpired
        {
            get; set;
        }

    }
}
