using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VetraMax.Infrastructure.DataSeed
{
	public class RoleSeeder
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public RoleSeeder(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public async Task SeedRolesAsync()
		{
			try
			{
				var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "DataSeed", "Role.json");
				var jsonData = await File.ReadAllTextAsync(jsonPath);
				var roles = JsonSerializer.Deserialize<List<string>>(jsonData);

				if (roles != null)
				{
					foreach (var role in roles)
					{
						if (!await _roleManager.RoleExistsAsync(role.Trim()))
						{
							await _roleManager.CreateAsync(new IdentityRole(role.Trim()));
							Console.WriteLine($"✅ Role '{role.Trim()}' added.");
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"❌ Error seeding roles: {ex.Message}");
			}
		}
	}
}
