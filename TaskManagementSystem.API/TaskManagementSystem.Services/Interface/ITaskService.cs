using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Entity;
using TaskManagementSystem.Data.Enums;
using TaskManagementSystem.Dto.Task;
using TaskManagementSystem.Dto.Response;

namespace TaskManagementSystem.Services.Interface
{
    public interface ITaskService
    {
        Task<ApiResponse<bool>> Create(TaskRequestDto taskItem);
        Task<ApiResponse<bool>> Update(int id, TaskRequestDto user);
        Task<ApiResponse<bool>> Delete(int id);
        Task<ApiResponse<List<TaskDto>>> GetUserTask();
        Task<ApiResponse<List<TaskDto>>> GetAll();
        Task<ApiResponse<bool>> TaskAction(int id, Status status);
        Task<ApiResponse<List<TaskDto>>> GetTaskReport(TaskReportRequestDto request);
    }
}
