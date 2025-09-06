using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Application.Dtos
{
    public class RefreshRequestTokenDto
    {
        public int UserId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
