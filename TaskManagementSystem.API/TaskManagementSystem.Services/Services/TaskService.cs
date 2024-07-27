using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagementSystem.Data.Base;
using TaskManagementSystem.Data.Context;
using TaskManagementSystem.Data.Entity;
using TaskManagementSystem.Data.Enums;
using TaskManagementSystem.Dto.Task;
using TaskManagementSystem.Dto.Response;
using TaskManagementSystem.Services.Interface;
using TaskManagementSystem.Dto.User;

namespace TaskManagementSystem.Services.Services
{
    public class TaskService : ITaskService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AppSettings _appSetting;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        List<UserDto> users = new List<UserDto>();

        public TaskService(DataContext context,
            IHttpContextAccessor httpContext,
            IOptions<AppSettings> options, IMapper mapper, IUserService userService)
        {
            _context = context;
            _httpContext = httpContext;
            _appSetting = options.Value;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ApiResponse<bool>> Create(TaskRequestDto taskItem)
        {

            var task = _mapper.Map<Tasks>(taskItem);
            task.Status = Status.Idle;
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "Task added successfully"
            };
        }

        public async Task<ApiResponse<bool>> Update(int id, TaskRequestDto task)
        {
            var dbItem = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItem == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Task not found"
                };
            }

            _mapper.Map(task, dbItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "Task updated successfully"
            };
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var dbItem = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItem == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Task not found"
                };
            }

            _context.Remove(dbItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "Task removed successfully"
            };
        }

        public async Task<ApiResponse<List<TaskDto>>> GetUserTask()
        {
            var id = _userService.GetCurrentUser().Id;
            var userTask = new List<Tasks>();
            var users = new List<UserDto>();
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            var userDetail = _mapper.Map<UserDto>(user);
            users.Add(userDetail);

            if (user?.Role.Name == "Admin")
            {
                userTask = await _context.Tasks.Where(x => x.IsActive == true).ToListAsync();
            }
            else
            {
                users.AddRange(getSubUsers(user.Id));
                var usersId = users.Select(x => x.Id).ToList();
                if (usersId.Count > 0)
                {
                    userTask = await GetTask(usersId);
                }
            }

            var Data = userTask.Select(x => new TaskDto
            {
                Id = x.Id,
                Subject = x.Subject,
                ProjectId = x.ProjectId,
                ProjectName = x.Projects.Name,
                AssignTo = x.AssignTo,
                AssignUserName = x.AssignUsers.FullName,
                Type = x.Type,
                Priority = x.Priority,
                AssignDate = x.AssignDate,
                DueDate = x.DueDate,
                Description = x.Description,
                Attachment = x.Attachment,
                Status = x.Status,
                IsActive = x.IsActive,
            }).ToList();

            Data.ForEach(task =>
            {
                task.Notes = _context.TaskNotes.Where(x => x.TaskId == task.Id).Select(y => new TaskNoteDto
                {
                    Id = y.Id,
                    TaskId = y.TaskId,
                    Note = y.Note,
                    Attachment = y.Attachment,
                    CreatedBy = _context.Users.Where(z => z.Id == y.CreatedBy).Select(x => x.FullName).FirstOrDefault(),
                    CreatedTime = y.CreatedTime
                }).ToList();
            });

            return new ApiResponse<List<TaskDto>>()
            {
                IsSuccess = true,
                Data = Data
            };
        }

        public async Task<ApiResponse<List<TaskDto>>> GetAll()
        {
            var userTask = await _context.Tasks.Select(x => new TaskDto
            {
                Id = x.Id,
                Subject = x.Subject,
                ProjectId = x.ProjectId,
                ProjectName = x.Projects.Name,
                AssignTo = x.AssignTo,
                AssignUserName = x.AssignUsers.FullName,
                Type = x.Type,
                Priority = x.Priority,
                AssignDate = x.AssignDate,
                DueDate = x.DueDate,
                Description = x.Description,
                Attachment = x.Attachment,
                Status = x.Status,
                IsActive = x.IsActive
            }).ToListAsync();

            return new ApiResponse<List<TaskDto>>()
            {
                IsSuccess = true,
                Data = userTask
            };
        }

        public async Task<ApiResponse<bool>> TaskAction(int id, Status status)
        {
            var dbItem = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItem == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Task not found"
                };
            }

            dbItem.Status = status;

            if (status == Status.Running)
            {
                await _context.TaskTracker.AddAsync(new TaskTracker
                {
                    TaskId = id,
                    StartTime = DateTime.UtcNow,
                    EmdTime = null
                });
            }
            else
            {
                var dbTracker = await _context.TaskTracker.FirstOrDefaultAsync(x => x.TaskId == id && x.EmdTime == null);
                if (dbTracker != null)
                {
                    dbTracker.EmdTime = DateTime.UtcNow;
                }
            }

            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "Task updated successfully"
            };
        }

        public async Task<ApiResponse<List<TaskDto>>> GetTaskReport(TaskReportRequestDto request)
        {
            var id = _userService.GetCurrentUser().Id;
            var userTask = new List<Tasks>();
            var users = new List<UserDto>();
            var user = await _context.Users.Where(x => x.Id == id).FirstAsync();
            var userDetail = _mapper.Map<UserDto>(user);
            users.Add(userDetail);

            if (user?.Role.Name == "Admin")
            {
                userTask = await _context.Tasks.Where(x => x.IsActive == true
                        && (request.ProjectId == null || x.ProjectId == request.ProjectId)
                        && (request.AssignTo == null || x.AssignTo == request.AssignTo)
                        && (request.Type == null || x.Type == request.Type)
                        && (request.Priority == null || x.Priority == request.Priority)
                        && (request.Status == null || x.Status == request.Status)
                        && (x.AssignDate >= request.AssignFromDate.Date && x.AssignDate <= request.AssignToDate)
                        && ((request.DueTask == false && x.DueDate > DateTime.UtcNow) || (request.DueTask == true && x.DueDate < DateTime.UtcNow))).ToListAsync();
            }
            else
            {
                users.AddRange(getSubUsers(user.Id));
                var usersId = users.Select(x => x.Id).ToList();
                if (usersId.Count > 0)
                {
                    userTask = await _context.Tasks.Where(x =>
                        (usersId.Contains(Convert.ToInt32(x.AssignTo))
                        && x.IsActive == true)
                        && (request.ProjectId == null || x.ProjectId == request.ProjectId)
                        && (request.AssignTo == null || x.AssignTo == request.AssignTo)
                        && (request.Type == null || x.Type == request.Type)
                        && (request.Priority == null || x.Priority == request.Priority)
                        && (request.Status == null || x.Status == request.Status)
                        && (x.AssignDate >= request.AssignFromDate.Date && x.AssignDate <= request.AssignToDate)
                        && ((request.DueTask == false && x.DueDate > DateTime.UtcNow) || (request.DueTask == true && x.DueDate < DateTime.UtcNow))
                    ).ToListAsync();
                }
            }

            var Data = userTask.Select(x => new TaskDto
            {
                Id = x.Id,
                Subject = x.Subject,
                ProjectId = x.ProjectId,
                ProjectName = x.Projects.Name,
                AssignTo = x.AssignTo,
                AssignUserName = x.AssignUsers.FullName,
                Type = x.Type,
                Priority = x.Priority,
                AssignDate = x.AssignDate,
                DueDate = x.DueDate,
                Description = x.Description,
                Attachment = x.Attachment,
                Status = x.Status,
                IsActive = x.IsActive,
            }).ToList();


            return new ApiResponse<List<TaskDto>>()
            {
                IsSuccess = true,
                Data = Data
            };
        }

        private List<UserDto> getSubUsers(int Id)
        {
            var userList = _context.Users.Where(x => x.ManagerId == Id).ToList();
            userList.ForEach(user =>
            {
                var u = _mapper.Map<UserDto>(user);
                users.Add(u);
                getSubUsers(u.Id);
            });
            return users;
        }

        private async Task<List<Tasks>> GetTask(List<int> ids)
        {
            return await _context.Tasks.Where(x => ids.Contains(Convert.ToInt32(x.AssignTo)) && x.IsActive == true).ToListAsync();
        }
    }
}
