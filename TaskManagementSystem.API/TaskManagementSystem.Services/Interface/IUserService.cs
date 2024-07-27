using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Entity;
using TaskManagementSystem.Dto.Response;
using TaskManagementSystem.Dto.User;

namespace TaskManagementSystem.Services.Interface
{
    public interface IUserService
    {
        Task<ApiResponse<LoginResponseDto>> Login(LoginRequestDto userItem);
        Task<ApiResponse<bool>> Create(UserRequestDto loginDto);
        Task<ApiResponse<bool>> Update(int id, UserRequestDto user);
        Task<ApiResponse<bool>> Delete(int id);
        Task<ApiResponse<List<UserDto>>> GetAll();
        UserDto GetCurrentUser();
    }
}
