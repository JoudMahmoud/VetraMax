using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VetraMax.Application.DTOs;
using VetraMax.Application.Services;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Entities.OwnedClasses;
using VetraMax.Domain.Interfaces;

namespace VetraMax.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		#region Member Data
		private readonly UserManager<User> _userManager;
		private readonly IMemoryCache _memoryCache;
		private readonly IMapper _mapper;
		private readonly ITraderRepository _traderRepository;
		private readonly RoleService _roleService;
		private readonly IConfiguration _configuration;
		#endregion


		#region Constructor
		public AccountController(UserManager<User> userManager, IMemoryCache memoryCache, IMapper mapper, ITraderRepository traderRepository, IConfiguration configuration) {
			_userManager = userManager;
			_memoryCache = memoryCache;
			_mapper = mapper;
			_traderRepository = traderRepository;
			_configuration = configuration;
		}

		#endregion

		#region Send OTP
		[HttpPost("send-otp")]
		public async Task<IActionResult> SendOTP([FromQuery] string phoneNumber)
		{
			var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
			string otp = "1234";
			_memoryCache.Set(phoneNumber, otp, TimeSpan.FromMinutes(5));
			if (existingUser != null)
			{
				return Ok(new { message = "OTP sent for login.", otp = otp });
			}
			return Ok(new { message = "OTP sent for registration.", otp = otp });
		}
		#endregion

		#region Register 
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterUserDto RegisterUser)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var existUser = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == RegisterUser.PhoneNumber);
			if (existUser != null)
			{
				return BadRequest(new { error = "Phone number already exists" });
			}

			if (ModelState.IsValid)
			{
				var address = _mapper.Map<Address>(RegisterUser.Address);
				var user = _mapper.Map<User>(RegisterUser);
				user.Address = address;
				if (RegisterUser.TraderVerificationInfo != null)
				{
					var traderVerification = _mapper.Map<TraderVerificationInfo>(RegisterUser.TraderVerificationInfo);
					user.TraderVerificationInfo = traderVerification;
				}

				var TraderType = await _traderRepository.GetTraderTypeById(RegisterUser.TraderId);
				if (TraderType == null)
				{
					return BadRequest(new { error = "Invalid TraderType" });
				}

				user.TraderType = TraderType;
				//create the user
				IdentityResult result = await _userManager.CreateAsync(user);
				if (result.Succeeded)
				{
					await _userManager.UpdateAsync(user);
					var wallet = new Wallet { Balance = 0, UserId = user.Id };
					user.Wallet = wallet;
					await _userManager.UpdateAsync(user);
					await _roleService.AddUserToRoleAsync(user, "User");
					var roles = await _userManager.GetRolesAsync(user);

					var token = GenerateJwtToken(user, roles, out var expiration);

					return Ok(new { token, expiration });
				}
				else { return BadRequest(result.Errors); }

			}
			return BadRequest(ModelState);

		}
			#endregion



			#region Generate JWT Token
			private string GenerateJwtToken(User user, IList<string> roles, out DateTime expiration)
			{
				var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim("userId", user.Id),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

				foreach (var itemRole in roles)
				{
					claims.Add(new Claim(ClaimTypes.Role, itemRole));
				}

				SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
				SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
				expiration = DateTime.UtcNow.AddHours(24);
				//create Token 
				JwtSecurityToken myToken = new JwtSecurityToken(
					issuer: _configuration["JWT:ValidIssuer"],
					audience: _configuration["JWT:ValidAudience"],
					claims: claims,
					expires: expiration,
					signingCredentials: signingCred
					);
				return new JwtSecurityTokenHandler().WriteToken(myToken);
			}
			#endregion


		} 
}

