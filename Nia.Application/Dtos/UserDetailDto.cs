using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Application.Dtos
{
    public class UserDetailDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string? Username { get; set; }
       
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? ImagePath { get; set; }

        public int LocationId { get; set; }
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string PasswordConfirmation { get; set; } = string.Empty;

    }
}
