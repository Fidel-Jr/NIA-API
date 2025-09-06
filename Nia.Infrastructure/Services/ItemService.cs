using Microsoft.EntityFrameworkCore;
using Nia.Application.Common;
using Nia.Application.Dtos;
using Nia.Application.IServices;
using Nia.Domain.Entities;
using Nia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCode = QRCoder.QRCode;
using QRCodeModel = Nia.Domain.Entities.QRCode;

namespace Nia.Infrastructure.Services
{
    public class ItemService(NiaDbContext context, QrCodeService qrCodeService) : IItemService
    {
        public async Task<Item> AddItem(ItemRequestDto itemDto)
        {
            if (itemDto == null)
                throw new ArgumentNullException(nameof(itemDto), "Item request data is null.");

            // Map DTO to entity
            var item = new Item
            {
                Unit = itemDto.Unit,
                Description = itemDto.Description,
                CategoryId = itemDto.CategoryId,
                UnitValue = itemDto.UnitValue,
                Quantity = itemDto.Quantity,
                Pac = itemDto.Pac,
                DateAcquired = itemDto.DateAcquired,
                PoNumber = itemDto.PoNumber,
                ImagePath = itemDto.ImagePath,
                LocationId = itemDto.LocationId,
                ConditionId = itemDto.ConditionId,
                ConditionNumberId = itemDto.ConditionNumberId
            };

            Guid newGuid = Guid.NewGuid();
            string qrImage = qrCodeService.GenerateQrCodeWithLogoBase64(newGuid.ToString(), "wwwroot/images/logo.png");

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                context.Items.Add(item);
                await context.SaveChangesAsync();

                var QrCodeModel = new QRCodeModel()
                {
                    Guid = newGuid,
                    ItemId = item.Id,
                    QrCode = qrImage
                };

                context.QRCodes.Add(QrCodeModel);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            
            return item;
        }

        public async Task<List<QRItemDto?>> GetAllItems()
        {
            var items = await context.QRCodes.Include(q => q.Item).ThenInclude(q => q!.Category)
                                            .Include(q => q.Item).ThenInclude(q => q!.Location)
                                            .Include(q => q.Item).ThenInclude(q => q!.Condition)
                                            .Include(q => q.Item).ThenInclude(q => q!.ConditionNumber)
                                            .Select(q => new QRItemDto
                                            {
                                                Guid = q.Guid,
                                                QrCode = q.QrCode,
                                                Item = new ItemDto
                                                {
                                                    Unit = q.Item.Unit,
                                                    Description = q.Item.Description,
                                                    Category = new CategoryDto
                                                    {
                                                        CategoryName = q.Item.Category.CategoryName
                                                    },
                                                    UnitValue = q.Item.UnitValue,
                                                    Quantity = q.Item.Quantity,
                                                    Pac = q.Item.Pac,
                                                    DateAcquired = q.Item.DateAcquired,
                                                    PoNumber = q.Item.PoNumber,
                                                    ImagePath = q.Item.ImagePath,
                                                    Location = new LocationDto
                                                    {
                                                        LocationName = q.Item.Location.LocationName
                                                    },
                                                    Condition = new ConditionDto
                                                    {
                                                        ConditionName = q.Item.Condition.ConditionName
                                                    },
                                                    ConditionNumber = new ConditionNumberDto
                                                    {
                                                        ConditionNumberType = q.Item.ConditionNumber.ConditionNumberType
                                                    }
                                                }
                                            }).ToListAsync();
            return items!;
        }

        public async Task<QRCodeModel> GetItemQr(Guid id)
        {
            var item = await context.QRCodes.Include(i => i.Item).FirstOrDefaultAsync(q => q.Guid == id);
            if (item == null || item.QrCode == null)
                throw new ArgumentNullException(nameof(item), "Item request data is null.");

            return item;
        }

        public async Task<Result<BorrowTransactionDto>> Borrow(BorrowRequestDto request, int borrowerId)
        {
            var item = await context.QRCodes.Include(i=>i.Item).FirstOrDefaultAsync(q => q.Guid == request.QrCodeGuid);

            var borrower = await context.Users.FindAsync(borrowerId);

            if(borrower is null)
            {
                return Result<BorrowTransactionDto>.Failure("Borrower not found.");
            }

            if (item is null)
                return Result<BorrowTransactionDto>.Failure("QR code not found.");

            if (item.Item == null)
                return Result<BorrowTransactionDto>.Failure("Item not linked to QR code.");

            if (item.Item.Quantity < request.Quantity)
                return Result<BorrowTransactionDto>.Failure("Insufficient item quantity.");

            item.Item.Quantity -= request.Quantity;

            var transaction = new BorrowTransaction
            {
                ItemId = item.Item.Id,
                Item = item.Item,
                Quantity = request.Quantity,
                BorrowerId = borrower.Id,
                Borrower = borrower,
                Status = "borrowed"
            };

            context.BorrowTransactions.Add(transaction);
            await context.SaveChangesAsync();

            if (item == null || item.Item == null)
            {
                return Result<BorrowTransactionDto>.Failure("Item or item details not found.");
            }

            return Result<BorrowTransactionDto>.Success(new BorrowTransactionDto
            {
                Item = new ItemDto
                {
                    Unit = item.Item.Unit,
                    Description = item.Item.Description,
                    Category = item.Item.Category == null ? null : new CategoryDto
                    {
                        CategoryName = item.Item.Category.CategoryName
                    },
                    UnitValue = item.Item.UnitValue,
                    Quantity = item.Item.Quantity,
                    Pac = item.Item.Pac,
                    DateAcquired = item.Item.DateAcquired,
                    PoNumber = item.Item.PoNumber,
                    ImagePath = item.Item.ImagePath,
                    Location = item.Item.Location == null ? null : new LocationDto
                    {
                        LocationName = item.Item.Location.LocationName
                    },
                    Condition = item.Item.Condition == null ? null : new ConditionDto
                    {
                        ConditionName = item.Item.Condition.ConditionName
                    },
                    ConditionNumber = item.Item.ConditionNumber == null ? null : new ConditionNumberDto
                    {
                        ConditionNumberType = item.Item.ConditionNumber.ConditionNumberType
                    }
                },
                Quantity = request.Quantity,
                Borrower = borrower,
                Status = "borrowed"
            });

        }

        public async Task<ResultNonGeneric> DeleteItem(Guid guid)
        {
            var itemId = await context.QRCodes
            .Where(q => q.Guid == guid)
            .Select(q => q.ItemId)
            .FirstOrDefaultAsync();

            if (itemId == 0) // or itemId == null if it's nullable
            {
                // handle not found
                return ResultNonGeneric.Failure("QR Code not found or not linked to an item.");
            }

            var item = await context.Items
                    .FirstOrDefaultAsync(i => i.Id == itemId);

            if (item == null)
            {
                return ResultNonGeneric.Failure("Item not found.");
            }

            context.Items.Remove(item); 
            await context.SaveChangesAsync();

            return ResultNonGeneric.Success();


        }
        
        public async Task<Result<QRItemDto>> Validate(ItemValidationRequestDto request, Guid guid)
        {
            // Find the QR code by its GUID
            var qrCode = await context.QRCodes.FindAsync(guid);

            if (qrCode == null)
            {
                return Result<QRItemDto>.Failure("QR code or item not found.");
            }
            // Find the item associated with the QR code
            var item = await context.Items.FirstOrDefaultAsync(i => i.Id == qrCode!.ItemId);

            // Check if the QR code and item exist
            
            if (qrCode.Guid != guid)
            {
                return Result<QRItemDto>.Failure("Invalid QR Code.");
            }

            // Generate a new QR code with the updated item information after validation
            Guid newGuid = Guid.NewGuid();
            string qrImage = qrCodeService.GenerateQrCodeWithLogoBase64(newGuid.ToString(), "wwwroot/images/logo.png");

            // Full control over when to commit changes
            using var transaction = await context.Database.BeginTransactionAsync();

            // Add, Update and Save changes in the database 
            try
            {
                // Update the QR code
                qrCode.IsActive = false;
                qrCode.UpdatedAt = DateTime.UtcNow;
                await context.SaveChangesAsync();

                var conditionHistory = new ItemConditionHistory
                {
                    ItemId = item.Id,
                    QrCodeId = qrCode.Guid,
                    ConditionId = item.ConditionId,
                    ConditionNumberId = item.ConditionNumberId
                };

                // Update the item condition
                item.ConditionId = request.ConditionId;
                item.ConditionNumberId = request.ConditionNumberId;
                item.UpdatedAt = DateTime.UtcNow;
                await context.SaveChangesAsync();

                context.ItemConditionHistories.Add(conditionHistory);
                await context.SaveChangesAsync();

                var QrCodeModel = new QRCodeModel()
                {
                    Guid = newGuid,
                    ItemId = item.Id,
                    QrCode = qrImage,
                    Version = qrCode.Version + 1,
                    IsActive = true,
                };
                context.QRCodes.Add(QrCodeModel);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync(); 
                throw;
            }

            // Fetch the updated item with all related data
            item = await context.Items
                    .Include(i => i.Category)
                    .Include(i => i.Location)
                    .Include(i => i.Condition)
                    .Include(i => i.ConditionNumber)
                    .FirstOrDefaultAsync(i => i.Id == qrCode!.ItemId); // or your ID

            return Result<QRItemDto>.Success( new QRItemDto
            {
                Guid = newGuid,
                QrCode = qrImage,
                Item = new ItemDto
                {
                    Unit = item.Unit,
                    Description = item.Description,
                    Category = new CategoryDto
                    {
                        CategoryName = item.Category?.CategoryName
                    },
                    UnitValue = item.UnitValue,
                    Quantity = item.Quantity,
                    Pac = item.Pac,
                    DateAcquired = item.DateAcquired,
                    PoNumber = item.PoNumber,
                    ImagePath = item.ImagePath,
                    Location = new LocationDto
                    {
                        LocationName = item.Location?.LocationName
                    },
                    Condition = new ConditionDto
                    {
                        ConditionName = item.Condition?.ConditionName
                    },
                    ConditionNumber = new ConditionNumberDto
                    {
                        ConditionNumberType = item.ConditionNumber?.ConditionNumberType
                    }
                }
            });


        }
    }
}
