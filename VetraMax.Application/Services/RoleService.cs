using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;

namespace VetraMax.Application.Services
{
	public class RoleService
	{
		#region Member Data
		private readonly UserManager<User> _userManager;
		#endregion

		#region Constructor
		public RoleService(UserManager<User> userManager) { _userManager = userManager; }

		#endregion

		public async Task AddUserToRoleAsync(User user, string roleName)
		{
			if(!await _userManager.IsInRoleAsync(user, roleName))
			{
				await _userManager.AddToRoleAsync(user, roleName);
			}
		}

		public async Task RemoveUserFromRoleAsync(User user, string roleName)
		{
			if(await _userManager.IsInRoleAsync(user, roleName))
			{
				await _userManager.RemoveFromRoleAsync(user, roleName);
			}
		}
	}
}
