using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Domain.Entities
{
    public class QRCode
    {
        public Guid Guid { get; set; }
        public int? ItemId { get; set; }
        public Item? Item { get; set; }
        public required string QrCode { get; set; }
        public bool IsActive { get; set; } = true;
        public int Version { get; set; } = 1;

        [Column(TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "timestamp")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
    }
}
