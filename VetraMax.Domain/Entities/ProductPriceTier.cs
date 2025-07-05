using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities
{
	public class ProductPriceTier:Base
	{
		[Required]
		public float Price { get; set; }
		public float PriceOnOffer { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		[ForeignKey("TraderPricing")]
		public int TraderPricingId { get; set; }
		public virtual TraderPricing TraderPricing {  get; set; } 
	}
}
