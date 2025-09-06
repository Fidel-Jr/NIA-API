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
    public class CreateUserRequest
    {
        
        public string FullName { get; set; }
        
        public string Username { get; set; }
     
        public string? Email { get; set; }
        
        public string Password { get; set; }
        public IFormFile? ImagePath { get; set; }
        
        public int LocationId { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string PasswordConfirmation { get; set; } = string.Empty;
    }
}
