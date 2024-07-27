using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Data.Entity;
using TaskManagementSystem.Dto.Response;
using TaskManagementSystem.Dto.Task;
using TaskManagementSystem.Services.Interface;
using TaskManagementSystem.Validators;

namespace TaskManagementSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskNoteController : ControllerBase
    {
        private readonly ILogger<TaskNoteController> _logger;
        private readonly ITaskNoteService _taskNoteService;

        public TaskNoteController(ILogger<TaskNoteController> logger, ITaskNoteService taskNoteService)
        {
            _taskNoteService = taskNoteService;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] TaskNoteRequestDto taskNoteDto)
        {
            this._logger.LogInformation($"{nameof(Create)}: called successfully");
            TaskNoteRequestValidation validator = new TaskNoteRequestValidation();
            var validationResult = validator.Validate(taskNoteDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await _taskNoteService.Create(taskNoteDto).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] TaskNoteRequestDto taskNoteDto)
        {
            this._logger.LogInformation($"{nameof(Update)}: called successfully");
            TaskNoteRequestValidation validator = new TaskNoteRequestValidation();
            var validationResult = validator.Validate(taskNoteDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await _taskNoteService.Update(id, taskNoteDto).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            this._logger.LogInformation($"{nameof(Delete)}: called successfully");
            var response = await _taskNoteService.Delete(id).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse<List<TaskNoteDto>>>> GetAll()
        {
            this._logger.LogInformation($"{nameof(Delete)}: called successfully");
            var response = await _taskNoteService.GetAll().ConfigureAwait(false);
            return Ok(response);
        }
    }
}
