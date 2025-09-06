using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Nia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Application.Dtos
{
    public class UserDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        [StringLength(100)]
        public string Username { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
        public IBrowserFile? ImagePath { get; set; }
        [Required]
        public int LocationId { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string PasswordConfirmation { get; set; } = string.Empty;
    }
}
