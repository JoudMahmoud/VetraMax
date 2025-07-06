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
	public class SubCategoryRepository:ISubCategoryRepository
	{
		#region Fields
		private readonly VetraMaxDbContext _dbContext;
		#endregion

		#region Constructor 
		public SubCategoryRepository(VetraMaxDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		#endregion

		#region CRUD Operations
		public async Task<IEnumerable<SubCategory>> GetAllAsync()
		{
			return await _dbContext.SubCategories.ToListAsync();
		}
		public async Task<SubCategory?> GetByIdAsync(int subCatId)
		{
			return await _dbContext.SubCategories.FindAsync(subCatId);
		}
		public async Task<SubCategory?> GetByNameAsync(string subCatName)
		{
			return await _dbContext.SubCategories.FirstOrDefaultAsync(c => c.Name == subCatName);
		}
		public async Task InsertSubCategory(SubCategory subCategory)
		{
			await _dbContext.SubCategories.AddAsync(subCategory);
		}
		public async Task<bool> SaveChanges()
		{
			return await _dbContext.SaveChangesAsync() > 0;
		}
		#endregion
	}
}
