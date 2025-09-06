using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Application.Dtos
{
    public class ConditionNumberDto
    {
        [Required]
        public required string ConditionNumberType { get; set; }
    }
}
