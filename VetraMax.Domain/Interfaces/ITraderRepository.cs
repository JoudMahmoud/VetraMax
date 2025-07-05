using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;

namespace VetraMax.Domain.Interfaces
{
	public interface ITraderRepository
	{
		Task<TraderType?> GetTraderTypeByName(string trderTypeName);
		Task<TraderType?> GetTraderTypeById(int traderTypeId);
	}
}
