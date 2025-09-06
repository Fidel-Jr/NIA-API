using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nia.Application.Common;
using Nia.Application.Dtos;
using Nia.Application.IService;
using Nia.Domain.Entities;
using Nia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Infrastructure.Services
{
    public class AuthService(NiaDbContext context, IConfiguration configuration) : IAuthService
    {
        public async Task<Result<LoginResponseDto>> LoginAsync(UserRequestDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user?.Username != request.Username)
            {
                return Result<LoginResponseDto>.Failure("Invalid Credentials");
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return Result<LoginResponseDto>.Failure("Invalid Credentials");
            }
            string token = CreateToken(user);
            return Result<LoginResponseDto>.Success(new LoginResponseDto
            {
                AccessToken = token,
                RefreshToken = await GenerateAndSaveRefreshToken(user),
                User = new UserInfo
                {
                    Id = user.Id,
                    Username  = user.Username,
                    Email = user.Email,
                    Role = user.Role,
                    ImagePath = user.ImagePath
                }
            });
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshToken(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await context.SaveChangesAsync();
            return refreshToken;
        }

        private async Task<User?> ValidateRefreshTokenAsync(int userId, string refreshToken)
        {
            var savedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (savedUser?.RefreshToken != refreshToken || savedUser?.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }
            return savedUser;
        }

        public async Task<User?> RegisterAsAdminAsync(CreateUserRequest request)
        {
            if (request == null)
            {
                return null;
            }
            if (await context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return null;
            }

            var user = new User
            {
                FullName = request.FullName,
                Username = request.Username,
                LocationId = request.LocationId,
                
                Role = "Admin"
            };

            if (request.ImagePath != null)
            {
                var fileName = Path.GetFileName(request.ImagePath.FileName);
                var savePath = Path.Combine("wwwroot/images", fileName);

                Directory.CreateDirectory("wwwroot/images");

                using var stream = new FileStream(savePath, FileMode.Create);
                await request.ImagePath.CopyToAsync(stream);
                user.ImagePath = fileName;
            }

            if (request.Email != null)
            {
                user.Email = request.Email;
            }
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> RegisterAsUserAsync(CreateUserRequest request)
        {
            if(await context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return null;
            }
            var user = new User
            {
                FullName = request.FullName,
                Username = request.Username,
                LocationId = request.LocationId,

                Role = "User"
            };

            if (request.ImagePath != null)
            {
                var fileName = Path.GetFileName(request.ImagePath.FileName);
                var savePath = Path.Combine("wwwroot/images", fileName);

                Directory.CreateDirectory("wwwroot/images");

                using var stream = new FileStream(savePath, FileMode.Create);
                await request.ImagePath.CopyToAsync(stream);
                user.ImagePath = fileName;
            }

            if (request.Email != null)
            {
                user.Email = request.Email;
            }
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<TokenResponseDto> RefreshTokenAsync(RefreshRequestTokenDto request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user is null)
                return null;
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshToken(user)
            };
        }

    }
}
