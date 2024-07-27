using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Entity;
using TaskManagementSystem.Dto.Response;
using TaskManagementSystem.Dto.Role;

namespace TaskManagementSystem.Services.Interface
{
    public interface IRoleService
    {
        Task<ApiResponse<bool>> Create(RoleRequestDto roleItem);
        Task<ApiResponse<bool>> Update(int id, RoleRequestDto user);
        Task<ApiResponse<bool>> Delete(int id);
        Task<ApiResponse<List<RoleDto>>> GetAll();
    }
}
