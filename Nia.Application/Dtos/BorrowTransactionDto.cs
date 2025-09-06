using Nia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Application.Dtos
{
    public class BorrowTransactionDto
    {
        public ItemDto Item { get; set; }
        public int Quantity { get; set; }
        public User? Borrower { get; set; }
        public string Status { get; set; } = "borrowed";
        
    }
}
