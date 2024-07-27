using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data.Base;
using TaskManagementSystem.Data.Context;
using TaskManagementSystem.Data.Entity;
using TaskManagementSystem.Dto.Response;
using TaskManagementSystem.Dto.User;
using TaskManagementSystem.Services.Interface;

namespace TaskManagementSystem.Services.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AppSettings _appSetting;
        private readonly IMapper _mapper;

        public UserService(DataContext context,
            IHttpContextAccessor httpContext,
            IOptions<AppSettings> options, IMapper mapper)
        {
            _context = context;
            _httpContext = httpContext;
            _appSetting = options.Value;
            _mapper = mapper;
        }

        public async Task<ApiResponse<LoginResponseDto>> Login(LoginRequestDto loginDto)
        {
            var userInfo = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
            if (userInfo == null)
            {
                return new ApiResponse<LoginResponseDto>
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }
            if (!userInfo.IsActive)
            {
                return new ApiResponse<LoginResponseDto>
                {
                    IsSuccess = false,
                    Message = "User is not active, please contact administrator"
                };
            }
            var isPasswordMatched = BCrypt.Net.BCrypt.Verify(loginDto.Password, userInfo.Password);
            if (!isPasswordMatched)
            {
                return new ApiResponse<LoginResponseDto>()
                {
                    IsSuccess = false,
                    Message = "Username or password not matched"
                };
            }
            var loginResponse = GetToken(userInfo);
            return new ApiResponse<LoginResponseDto>()
            {
                IsSuccess = true,
                Data = loginResponse
            };
        }

        private LoginResponseDto GetToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string appSecret = _appSetting.Secret;
            var key = Encoding.UTF8.GetBytes(appSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Role", user.Role.Name.ToString()),
                    new Claim("RoleId", user.RoleId.ToString()),
                    new Claim("UserID", user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("FullName", user.FullName),
                    new Claim("IsActive", user.IsActive.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userToken = tokenHandler.WriteToken(token);
            var loginResponse = _mapper.Map<LoginResponseDto>(user);
            loginResponse.Token = userToken;
            return loginResponse;
        }

        public async Task<ApiResponse<bool>> Create(UserRequestDto user)
        {
            var isExist = await _context.Users.AnyAsync(x => x.Email == user.Email);
            if (isExist)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "User already exist"
                };
            }

            var userItem = _mapper.Map<Users>(user);
            userItem.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, 10);
            await _context.Users.AddAsync(userItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "User added successfully"
            };
        }

        public async Task<ApiResponse<bool>> Update(int id, UserRequestDto user)
        {
            var dbItem = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItem == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }
            user.Password = dbItem.Password;
            _mapper.Map(user, dbItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "User updated successfully"
            };
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var dbItem = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItem == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }

            _context.Remove(dbItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "User removed successfully"
            };
        }

        public async Task<ApiResponse<List<UserDto>>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            List<UserDto> Data = users.Select(x => new UserDto
            {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FullName,
                PhoneNumber = x.PhoneNumber,
                RoleId = x.RoleId,
                RoleName = x.Role.Name,
                ManagerId = x.ManagerId,
                ManagerName = x.Manager?.FullName,
                IsActive = x.IsActive
            }).ToList();
            return new ApiResponse<List<UserDto>>()
            {
                IsSuccess = true,
                Data = Data
            };
        }

        private string GetClaimValueFromType(string claimType)
        {
            var identity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var claimData = identity.Claims.FirstOrDefault(x => x.Type.ToLower() == claimType.ToLower());
                if (claimData != null)
                {
                    return claimData.Value;
                }
            }
            return null;
        }
        public UserDto GetCurrentUser()
        {
            return new UserDto
            {
                Id = Int32.Parse(GetClaimValueFromType("UserID")),
                RoleId = Int32.Parse(GetClaimValueFromType("RoleId")),
                RoleName = GetClaimValueFromType("Role"),
                Email = GetClaimValueFromType("Email"),
                PhoneNumber = GetClaimValueFromType("MobilePhone"),
                FullName = GetClaimValueFromType("FullName")
            };
        }
    }
}
