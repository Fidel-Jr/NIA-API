using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Application.Dtos
{
    public class EditUserDto
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Username { get; set; }
        public string? Email { get; set; }
        public IFormFile? ImagePath { get; set; }
        [Required]
        public int LocationId { get; set; }
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? PasswordConfirmation { get; set; }

    }
}
