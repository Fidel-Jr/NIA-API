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
    public class AdminDetailsDto
    {
        public required string FullName { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string ImagePath { get; set; }
        public required int LocationId { get; set; }

    }
}
