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
using TaskManagementSystem.Dto.Project;
using TaskManagementSystem.Services.Interface;

namespace TaskManagementSystem.Services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AppSettings _appSetting;
        private readonly IMapper _mapper;

        public ProjectService(DataContext context,
            IHttpContextAccessor httpContext,
            IOptions<AppSettings> options, IMapper mapper)
        {
            _context = context;
            _httpContext = httpContext;
            _appSetting = options.Value;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> Create(ProjectRequestDto projectItem)
        {
            var isExist = await _context.Projects.AnyAsync(x => x.Name == projectItem.Name);
            if (isExist)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Project already exist"
                };
            }

            var project = _mapper.Map<Projects>(projectItem);
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "Project added successfully"
            };
        }

        public async Task<ApiResponse<bool>> Update(int id, ProjectRequestDto project)
        {
            var dbItem = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItem == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Project not found"
                };
            }

            _mapper.Map(project, dbItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "Project updated successfully"
            };
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var dbItem = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItem == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Project not found"
                };
            }

            _context.Remove(dbItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "Project removed successfully"
            };
        }

        public async Task<ApiResponse<List<ProjectDto>>> GetAll()
        {
            var projects = await _context.Projects.ToListAsync();
            var Data = _mapper.Map<List<ProjectDto>>(projects);
            return new ApiResponse<List<ProjectDto>>()
            {
                IsSuccess = true,
                Data = Data
            };
        }
    }
}
