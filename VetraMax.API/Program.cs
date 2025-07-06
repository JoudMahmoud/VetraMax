
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VetraMax.Application.Automapper;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Interfaces;
using VetraMax.Infrastructure.DataSeed;
using VetraMax.Infrastructure.Persistence;
using VetraMax.Infrastructure.Repositories;

namespace VetraMax.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);


			#region Service Registrations
			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


			//add autoMapper
			builder.Services.AddAutoMapper(cfg =>
			{
				cfg.AddProfile<MappingProfile>();
			});

			//add memory cach
			builder.Services.AddMemoryCache();

			//configure DbContext
			builder.Services.AddDbContext<VetraMaxDbContext>(
				options => options.UseLazyLoadingProxies()
				.UseSqlServer(
					builder.Configuration.GetConnectionString("DefaultConnection")));


			//Register Identity services
			builder.Services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<VetraMaxDbContext> ()
				.AddDefaultTokenProviders();

			// Configure Authentication
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
					ValidAudience = builder.Configuration["JWT:ValidAudience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
				};
			});

			//Register custom services
			builder.Services.AddScoped<RoleSeeder>();
			builder.Services.AddScoped<ITraderRepository,TraderRepository>();
			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();

			



			// Configure Authorization
			builder.Services.AddAuthorization();


			// Register custom services



			#endregion


			var app = builder.Build();

			#region Role and Data Seeding
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				try
				{
					var roleSeeder = services.GetRequiredService<RoleSeeder>();
					await roleSeeder.SeedRolesAsync();
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "❌ Error occurred during role seeding.");
				}
			}
			#endregion



			#region role and data seed 
			#endregion

			#region Configure Middleware
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();



			app.MapControllers();
			#endregion
			app.Run();
		}
	}
}
