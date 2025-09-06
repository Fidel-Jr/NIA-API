using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nia.Application.Dtos;
using Nia.Application.IServices;
using Nia.Domain.Entities;
using Nia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Infrastructure.Services
{
    public class HandleUserService(NiaDbContext context) : IHandleUserService
    {
        

        public async Task<User?> EditUser(int id, EditUserDto request)
        {
            var user = await context.Users.FindAsync(id);
            //var pass = user?.PasswordHash;
            if(user is null)
            {
                return null;
            }
            user.FullName = request.FullName;
            user.Username = request.Username;
            user.LocationId = request.LocationId;
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
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, request.Password);
                user.PasswordHash = hashedPassword;
            }
            //user.PasswordHash = pass!;
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User?>> GetAllUsers()
        {
            var allUsers = await context.Users.ToListAsync();
            if (allUsers.Count < 0)
            {
                return null!;
            }
            return allUsers!;
        }

        public async Task<AdminDetailsDto?> GetAdmin(int id)
        {
            var user = await context.Users
        .Where(u => u.Id == id)
        .Select(u => new AdminDetailsDto
        {
            FullName = u.FullName,
            Username = u.Username,
            Email = u.Email,
            ImagePath = u.ImagePath,
            Role = u.Role,
            LocationId = u.LocationId
        })

        .SingleOrDefaultAsync();
            if (user is null)
            {
                return null;
            }
            return user;
        }

        public async Task<List<User?>> GetAdmins()
        {
            var admins = await context.Users.Where(u => u.Role == "Admin").ToListAsync();
            if(admins.Count < 0)
            {
                return null!;
            }
            return admins!;
        }

        public async Task<User?> GetUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user is null)
            {
                return null;
            }
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await context.Users.Where(u => u.Role == "User").ToListAsync();
            if (users.Count < 0)
            {
                return null!;
            }
            return users!;
        }


        public async Task<User?> RemoveUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user is null)
            {
                return null;
            }
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return user;
        }

        
    }
}
