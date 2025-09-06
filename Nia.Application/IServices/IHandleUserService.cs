using Nia.Application.Dtos;
using Nia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nia.Application.IServices
{
    public interface IHandleUserService
    {
        Task<List<User?>> GetAllUsers();
        Task<List<User?>> GetAdmins();
        Task<AdminDetailsDto?> GetAdmin(int id);
        Task<List<User>> GetUsers();
        Task<User?> GetUser(int id);
        Task<User?> EditUser(int id, EditUserDto request);
        Task<User?> RemoveUser(int id);
    }
}
