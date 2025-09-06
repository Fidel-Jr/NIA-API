using Nia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Application.Dtos
{
    public class ItemDto
    {
        public string Unit { get; set; } = default!;
        public string Description { get; set; } = default!;

        public CategoryDto? Category { get; set; } = default!;
        public decimal UnitValue { get; set; }
        public int Quantity { get; set; }
        public string Pac { get; set; } = default!;
        public DateOnly DateAcquired { get; set; }
        public string PoNumber { get; set; } = default!;
        public string ImagePath { get; set; } = default!;

        public LocationDto? Location { get; set; }
        public ConditionDto? Condition { get; set; }
        public ConditionNumberDto? ConditionNumber { get; set; }
    }
}
