using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;

namespace VetraMax.Domain.Interfaces
{
    public interface ICategoryRepository
    {
		Task<IEnumerable<Category>> GetAllAsync();
		Task<Category?> GetByIdAsync(int catId);
		Task<Category?> GetByNameAsync(string catName);
		Task InsertCategory(Category category);
		Task<bool> SaveChanges();


	}
}
