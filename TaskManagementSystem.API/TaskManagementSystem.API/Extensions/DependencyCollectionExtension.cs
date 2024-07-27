using FluentValidation;
using TaskManagementSystem.Dto.Project;
using TaskManagementSystem.Dto.Role;
using TaskManagementSystem.Dto.Task;
using TaskManagementSystem.Dto.User;
using TaskManagementSystem.Services.Interface;
using TaskManagementSystem.Services.Services;
using TaskManagementSystem.Validators;

namespace TaskManagementSystem.API.Extensions
{
    public static class DependencyCollectionExtension
    {
        public static void InjectDependency(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskNoteService, TaskNoteService>();


            services.AddScoped<IValidator<RoleRequestDto>, RoleRequestValidator>();
            services.AddScoped<IValidator<UserRequestDto>, UserRequestValidator>();
            services.AddScoped<IValidator<LoginRequestDto>, LoginRequestValidator>();
            services.AddScoped<IValidator<ProjectRequestDto>, ProjectRequestValidator>();
            services.AddScoped<IValidator<TaskRequestDto>, TaskRequestValidator>();
            services.AddScoped<IValidator<TaskNoteRequestDto>, TaskNoteRequestValidation>();
            services.AddScoped<IValidator<TaskReportRequestDto>, TaskReportRequestValidator>();
        }
    }
}
