using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Domain.Entities
{
    public class ItemConditionHistory
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item? Item { get; set; }
        public Guid QrCodeId { get; set; }
        public QRCode? QrCode { get; set; }
        public int ConditionId { get; set; }
        public Condition? Condition { get; set; }
        public int ConditionNumberId { get; set; }
        public ConditionNumber? ConditionNumber { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "timestamp")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
