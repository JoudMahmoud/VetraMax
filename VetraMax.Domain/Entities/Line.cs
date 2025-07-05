using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities
{
	public class Line : Base
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string City { get; set; }

		public virtual ICollection<User>? Users { get; set; }
		public virtual ICollection<Day>? Days { get; set; }
	}
}
