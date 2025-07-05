using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities
{
	public class Day:Base
	{
		public string DayOfWeek { get; set; }
		public virtual ICollection<Line>? Lines { get; set; }
	}
}
