using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Application.Dtos
{
    public class BorrowRequestDto
    {
        public Guid QrCodeGuid { get; set; }
        public int Quantity { get; set; }
    }
}
