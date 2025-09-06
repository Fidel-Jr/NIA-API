using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string FullName { get; set; }
        [Required]
        [StringLength(100)]
        public required string Username { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z()\s-]+[0-9]*$")]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(Admin|User$)", ErrorMessage = "Role must be 'Admin' or 'User'.")]
        public string Role { get; set; } = "Admin";
        public string ImagePath { get; set; } = "default.png";
        [Required]
        public int LocationId { get; set; }
        public Location? Location { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "timestamp")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
