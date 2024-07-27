using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Data.Entity;
using TaskManagementSystem.Dto.Response;
using TaskManagementSystem.Dto.Project;
using TaskManagementSystem.Services.Interface;
using TaskManagementSystem.Validators;

namespace TaskManagementSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectService _projectService;

        public ProjectController(ILogger<ProjectController> logger, IProjectService projectService)
        {
            _projectService = projectService;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] ProjectRequestDto projectDto)
        {
            this._logger.LogInformation($"{nameof(Create)}: called successfully");
            ProjectRequestValidator validator = new ProjectRequestValidator();
            var validationResult = validator.Validate(projectDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await _projectService.Create(projectDto).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] ProjectRequestDto projectDto)
        {
            this._logger.LogInformation($"{nameof(Update)}: called successfully");
            ProjectRequestValidator validator = new ProjectRequestValidator();
            var validationResult = validator.Validate(projectDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await _projectService.Update(id, projectDto).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            this._logger.LogInformation($"{nameof(Delete)}: called successfully");
            var response = await _projectService.Delete(id).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse<List<ProjectDto>>>> GetAll()
        {
            this._logger.LogInformation($"{nameof(Delete)}: called successfully");
            var response = await _projectService.GetAll().ConfigureAwait(false);
            return Ok(response);
        }
    }
}
