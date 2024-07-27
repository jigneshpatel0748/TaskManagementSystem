using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Data.Entity;
using TaskManagementSystem.Dto.Response;
using TaskManagementSystem.Dto.Role;
using TaskManagementSystem.Services.Interface;
using TaskManagementSystem.Validators;

namespace TaskManagementSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IRoleService _roleService;

        public RoleController(ILogger<RoleController> logger, IRoleService roleService)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] RoleRequestDto roleDto)
        {
            this._logger.LogInformation($"{nameof(Create)}: called successfully");
            RoleRequestValidator validator = new RoleRequestValidator();
            var validationResult = validator.Validate(roleDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await _roleService.Create(roleDto).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] RoleRequestDto roleDto)
        {
            this._logger.LogInformation($"{nameof(Update)}: called successfully");
            RoleRequestValidator validator = new RoleRequestValidator();
            var validationResult = validator.Validate(roleDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await _roleService.Update(id, roleDto).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            this._logger.LogInformation($"{nameof(Delete)}: called successfully");
            var response = await _roleService.Delete(id).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse<List<RoleDto>>>> GetAll()
        {
            this._logger.LogInformation($"{nameof(Delete)}: called successfully");
            var response = await _roleService.GetAll().ConfigureAwait(false);
            return Ok(response);
        }
    }
}
