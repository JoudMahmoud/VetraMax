using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities
{
	public class SubCategory:Base
	{
		[Required]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 50 characters")]
		public string Name { get; set; }	
		public string? ImageUrl { get; set; }
		public virtual ICollection<Product>? Products { get; set; }
		[Required]
		[ForeignKey("Category")]
		public int CatId { get; set; }
		public virtual Category Category { get; set; }
	}
}
