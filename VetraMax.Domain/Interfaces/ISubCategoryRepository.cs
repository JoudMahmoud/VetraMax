using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;

namespace VetraMax.Domain.Interfaces
{
	public interface ISubCategoryRepository
	{
		Task<IEnumerable<SubCategory>> GetAllAsync();
		Task<SubCategory?> GetByIdAsync(int subCatId);
		Task<SubCategory?> GetByNameAsync(string subCatName);
		Task InsertSubCategory(SubCategory subCategory);
		Task<bool> SaveChanges();
	}
}
