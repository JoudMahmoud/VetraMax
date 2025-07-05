using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities.OwnedClasses
{
	[Owned]
	public class QuantityByExpiry
	{
		public int Quantity { get; set; }
		public DateOnly ExpiryDate { get; set; }
		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}
