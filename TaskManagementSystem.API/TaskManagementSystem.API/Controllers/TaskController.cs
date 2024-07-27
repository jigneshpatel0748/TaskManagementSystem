using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Data.Entity;
using TaskManagementSystem.Data.Enums;
using TaskManagementSystem.Dto.Response;
using TaskManagementSystem.Dto.Task;
using TaskManagementSystem.Services.Interface;
using TaskManagementSystem.Validators;

namespace TaskManagementSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] TaskRequestDto taskDto)
        {
            this._logger.LogInformation($"{nameof(Create)}: called successfully");
            TaskRequestValidator validator = new TaskRequestValidator();
            var validationResult = validator.Validate(taskDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await _taskService.Create(taskDto).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] TaskRequestDto taskDto)
        {
            this._logger.LogInformation($"{nameof(Update)}: called successfully");
            TaskRequestValidator validator = new TaskRequestValidator();
            var validationResult = validator.Validate(taskDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await _taskService.Update(id, taskDto).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            this._logger.LogInformation($"{nameof(Delete)}: called successfully");
            var response = await _taskService.Delete(id).ConfigureAwait(false);
            return Ok(response);
        }
        
        [HttpGet("GetUserTask")]
        public async Task<ActionResult<ApiResponse<List<TaskDto>>>> GetUserTask()
        {
            this._logger.LogInformation($"{nameof(GetUserTask)}: called successfully");
            var response = await _taskService.GetUserTask().ConfigureAwait(false);
            return Ok(response);
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse<List<TaskDto>>>> GetAll()
        {
            this._logger.LogInformation($"{nameof(Delete)}: called successfully");
            var response = await _taskService.GetAll().ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPost("TaskAction/{id}/Status/{status}")]
        public async Task<ActionResult<ApiResponse<bool>>> TaskAction(int id, Status status)
        {
            this._logger.LogInformation($"{nameof(Create)}: called successfully");
            var response = await _taskService.TaskAction(id, status).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPost("GetTaskReport")]
        public async Task<ActionResult<ApiResponse<List<TaskDto>>>> GetTaskReport(TaskReportRequestDto request)
        {
            this._logger.LogInformation($"{nameof(GetTaskReport)}: called successfully");
            TaskReportRequestValidator validator = new TaskReportRequestValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await _taskService.GetTaskReport(request).ConfigureAwait(false);
            return Ok(response);
        }
    }
}
