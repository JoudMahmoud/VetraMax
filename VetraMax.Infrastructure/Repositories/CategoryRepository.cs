using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Interfaces;
using VetraMax.Infrastructure.Persistence;

namespace VetraMax.Infrastructure.Repositories
{
	public class CategoryRepository:ICategoryRepository
	{
		#region Fields
		private readonly VetraMaxDbContext _dbContext;
		#endregion

		#region Constructor 
		public CategoryRepository(VetraMaxDbContext dbContext) {
			_dbContext = dbContext;
		}
		#endregion

		#region CRUD Operations
		public async Task<IEnumerable<Category>> GetAllAsync()
		{
			return await _dbContext.Categories.ToListAsync();
		}
		public async Task<Category?> GetByIdAsync(int catId)
		{
			return await _dbContext.Categories.FindAsync(catId);
		}
		public async Task<Category?> GetByNameAsync(string catName)
		{
			return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == catName);
		}
		public async Task InsertCategory(Category category)
		{
			await _dbContext.Categories.AddAsync(category);
		}
		public async Task<bool> SaveChanges()
		{
			return await _dbContext.SaveChangesAsync() > 0;
		}
		#endregion

	}
}
