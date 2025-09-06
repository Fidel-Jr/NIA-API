using Nia.Application.Common;
using Nia.Application.Dtos;
using Nia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Application.IService
{
    public interface IAuthService
    {
        Task<User?> RegisterAsAdminAsync(CreateUserRequest request);
        Task<User?> RegisterAsUserAsync(CreateUserRequest request);
        Task<Result<LoginResponseDto>> LoginAsync(UserRequestDto request);
        Task<TokenResponseDto> RefreshTokenAsync(RefreshRequestTokenDto request);

    }
}
