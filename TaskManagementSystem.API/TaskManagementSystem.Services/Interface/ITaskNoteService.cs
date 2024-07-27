using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Entity;
using TaskManagementSystem.Dto.Task;
using TaskManagementSystem.Dto.Response;

namespace TaskManagementSystem.Services.Interface
{
    public interface ITaskNoteService
    {
        Task<ApiResponse<bool>> Create(TaskNoteRequestDto taskNoteItem);
        Task<ApiResponse<bool>> Update(int id, TaskNoteRequestDto user);
        Task<ApiResponse<bool>> Delete(int id);
        Task<ApiResponse<List<TaskNoteDto>>> GetAll();
    }
}
