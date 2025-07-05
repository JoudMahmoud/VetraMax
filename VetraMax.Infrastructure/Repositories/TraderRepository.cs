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
	public class TraderRepository:ITraderRepository
	{
		#region Member Data
		private readonly VetraMaxDbContext _dbContext;
		#endregion
		#region Contsructor
		public TraderRepository(VetraMaxDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		#endregion

		#region Methods
		public async Task<TraderType?> GetTraderTypeByName(string trderTypeName)
		{
			return await _dbContext.TraderTypes.FirstOrDefaultAsync(t => t.Name == trderTypeName);
		}
		public async Task<TraderType?> GetTraderTypeById(int traderTypeId)
		{
			return await _dbContext.TraderTypes.FirstOrDefaultAsync(t => t.Id == traderTypeId);
		}
		#endregion

	}
}
