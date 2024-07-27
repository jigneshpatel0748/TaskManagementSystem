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
using TaskManagementSystem.Dto.Task;
using TaskManagementSystem.Dto.Response;
using TaskManagementSystem.Services.Interface;

namespace TaskManagementSystem.Services.Services
{
    public class TaskNoteService  : ITaskNoteService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AppSettings _appSetting;
        private readonly IMapper _mapper;

        public TaskNoteService(DataContext context,
            IHttpContextAccessor httpContext,
            IOptions<AppSettings> options, IMapper mapper)
        {
            _context = context;
            _httpContext = httpContext;
            _appSetting = options.Value;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> Create(TaskNoteRequestDto taskNoteItem)
        {
            var taskNote = _mapper.Map<TaskNotes>(taskNoteItem);
            await _context.TaskNotes.AddAsync(taskNote);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "Note added successfully"
            };
        }

        public async Task<ApiResponse<bool>> Update(int id, TaskNoteRequestDto taskNote)
        {
            var dbItem = await _context.TaskNotes.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItem == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Note not found"
                };
            }

            _mapper.Map(taskNote, dbItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "Note updated successfully"
            };
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var dbItem = await _context.TaskNotes.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItem == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Note not found"
                };
            }

            _context.Remove(dbItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "Note removed successfully"
            };
        }

        public async Task<ApiResponse<List<TaskNoteDto>>> GetAll()
        {
            var notes = await _context.TaskNotes.ToListAsync();
            var Data = _mapper.Map<List<TaskNoteDto>>(notes);
            return new ApiResponse<List<TaskNoteDto>>()
            {
                IsSuccess = true,
                Data = Data
            };
        }
    }
}
