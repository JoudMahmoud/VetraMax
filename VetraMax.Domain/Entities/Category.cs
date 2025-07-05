using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities
{
	public class Category:Base
	{
		[Required]
		public string Name { get; set; }
		public virtual ICollection<SubCategory>? SubCategories { get; set; }
	}
}
