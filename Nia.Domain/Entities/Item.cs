using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Unit is required.")]
        [StringLength(255)]
        public required string Unit { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(255)]
        public required string Description { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitValue { get; set; }

        [Required]
        public required int Quantity { get; set; }

        [Required(ErrorMessage = "Pac is required.")]
        [StringLength(255)]
        public required string Pac { get; set; }
        
        public DateOnly DateAcquired { get; set; }

        [Required(ErrorMessage = "Po Number is required.")]
        [StringLength(255)]
        public required string PoNumber { get; set; }

        [StringLength(255)]
        public string ImagePath { get; set; } = "wwwroot/images/default.png";
        [Required]
        public int? LocationId { get; set; }
        public Location? Location { get; set; }
        [Required]
        public int ConditionId { get; set; }
        public Condition? Condition { get; set; }
        [Required]
        public int ConditionNumberId { get; set; }
        public ConditionNumber? ConditionNumber { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime UpdatedAt { get; set; }
        public bool IsArchived { get; set; } = false;
    }
}
