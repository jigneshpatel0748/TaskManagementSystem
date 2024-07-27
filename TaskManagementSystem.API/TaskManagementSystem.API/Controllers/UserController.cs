using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Data.Entity;
using TaskManagementSystem.Dto.Response;
using TaskManagementSystem.Dto.User;
using TaskManagementSystem.Services.Interface;
using TaskManagementSystem.Validators;

namespace TaskManagementSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login([FromBody] LoginRequestDto loginDto)
        {
            this._logger.LogInformation($"{nameof(Login)}: called successfully");
            LoginRequestValidator validator = new LoginRequestValidator();
            var validationResult = validator.Validate(loginDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await _userService.Login(loginDto).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ApiResponse<bool>>> Create([FromBody] UserRequestDto userDto)
        {
            this._logger.LogInformation($"{nameof(Create)}: called successfully");
            UserRequestValidator validator = new UserRequestValidator();
            var validationResult = validator.Validate(userDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await _userService.Create(userDto).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] UserRequestDto userDto)
        {
            this._logger.LogInformation($"{nameof(Update)}: called successfully");
            UserRequestValidator validator = new UserRequestValidator();
            var validationResult = validator.Validate(userDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var response = await _userService.Update(id, userDto).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            this._logger.LogInformation($"{nameof(Delete)}: called successfully");
            var response = await _userService.Delete(id).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse<List<UserDto>>>> GetAll()
        {
            this._logger.LogInformation($"{nameof(GetAll)}: called successfully");
            var response = await _userService.GetAll().ConfigureAwait(false);
            return Ok(response);
        }
    }
}
