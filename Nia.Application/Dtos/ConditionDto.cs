using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Application.Dtos
{
    public class ConditionDto
    {
        [Required]
        public required string ConditionName { get; set; }
    }
}
