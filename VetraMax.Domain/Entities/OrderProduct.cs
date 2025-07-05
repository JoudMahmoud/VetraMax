using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities
{
	public class OrderProduct:Base
	{
		public int Quantity { get; set; }
		public decimal Price { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }

		[ForeignKey("Order")]
		public int OrderId { get; set; }
		public virtual Order Order { get; set; }
	}
}
