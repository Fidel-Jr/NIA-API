using Nia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Application.Dtos
{
    public class QRItemDto
    {
        public required Guid Guid { get; set; }
        public required ItemDto Item {  get; set; }
        public required string QrCode { get; set; }

    }
}
