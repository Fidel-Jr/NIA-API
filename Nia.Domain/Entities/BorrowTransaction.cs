using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Domain.Entities
{
    public class BorrowTransaction
    {
        public int Id { get; set; }
        public required int ItemId { get; set; }
        public Item? Item { get; set; }
        public required int Quantity { get; set; }
        public required int BorrowerId { get; set; }
        public User? Borrower { get; set; }
        public string Status { get; set; } = "borrowed";
        [Column(TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "timestamp")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
