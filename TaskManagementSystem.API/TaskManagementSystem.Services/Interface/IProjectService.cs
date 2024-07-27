using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Entity;
using TaskManagementSystem.Dto.Response;
using TaskManagementSystem.Dto.Project;

namespace TaskManagementSystem.Services.Interface
{
    public interface IProjectService
    {
        Task<ApiResponse<bool>> Create(ProjectRequestDto projectItem);
        Task<ApiResponse<bool>> Update(int id, ProjectRequestDto user);
        Task<ApiResponse<bool>> Delete(int id);
        Task<ApiResponse<List<ProjectDto>>> GetAll();
    }
}
