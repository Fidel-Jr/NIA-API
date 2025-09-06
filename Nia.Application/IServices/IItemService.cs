using Nia.Application.Common;
using Nia.Application.Dtos;
using Nia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Application.IServices
{
    public interface IItemService
    {
        Task<List<QRItemDto?>> GetAllItems();
        Task<Item> AddItem(ItemRequestDto itemDto);
        Task<ResultNonGeneric> DeleteItem(Guid itemDto);
        Task<QRCode> GetItemQr(Guid id);
        Task<Result<BorrowTransactionDto>> Borrow(BorrowRequestDto request, int borrowerId);
        Task<Result<QRItemDto>> Validate(ItemValidationRequestDto request, Guid guid);
    }
}
