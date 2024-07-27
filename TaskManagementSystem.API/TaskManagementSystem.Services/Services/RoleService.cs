using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Base;
using TaskManagementSystem.Data.Context;
using TaskManagementSystem.Data.Entity;
using TaskManagementSystem.Dto.Response;
using TaskManagementSystem.Dto.Role;
using TaskManagementSystem.Services.Interface;

namespace TaskManagementSystem.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AppSettings _appSetting;
        private readonly IMapper _mapper;

        public RoleService(DataContext context,
            IHttpContextAccessor httpContext,
            IOptions<AppSettings> options, IMapper mapper)
        {
            _context = context;
            _httpContext = httpContext;
            _appSetting = options.Value;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> Create(RoleRequestDto roleItem)
        {
            var isExist = await _context.Roles.AnyAsync(x => x.Name == roleItem.Name);
            if (isExist)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Role already exist"
                };
            }

            var role = _mapper.Map<Roles>(roleItem);
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "Role added successfully"
            };
        }

        public async Task<ApiResponse<bool>> Update(int id, RoleRequestDto role)
        {
            var dbItem = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItem == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Role not found"
                };
            }

            _mapper.Map(role, dbItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "Role updated successfully"
            };
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var dbItem = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItem == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Role not found"
                };
            }

            _context.Remove(dbItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "Role removed successfully"
            };
        }

        public async Task<ApiResponse<List<RoleDto>>> GetAll()
        {
            var roles = await _context.Roles.ToListAsync();
            var Data = _mapper.Map<List<RoleDto>>(roles);
            return new ApiResponse<List<RoleDto>>()
            {
                IsSuccess = true,
                Data = Data
            };
        }
    }
}
