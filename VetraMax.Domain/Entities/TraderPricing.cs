using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities
{
	public class TraderPricing:Base
	{
		public int MaxQuintity { get; set; }
		public int MinQuintity { get; set; }
		public int PriceInCoins { get; set; }
		[ForeignKey("TraderType")]
		public int TraderTypeId { get; set; }
		public virtual TraderType TraderType { get; set; }

		[ForeignKey("Product")]
		public int ProdcutId { get; set; }
		public virtual Product Product { get; set; }
		public virtual ICollection<ProductPriceTier> PriceTiers { get; set; }
		
	}
}
