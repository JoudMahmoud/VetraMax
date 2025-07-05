using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities.OwnedClasses;
using VetraMax.Domain.Enums;

namespace VetraMax.Domain.Entities
{
	public class Product:Base
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string ImageUrl { get; set; }
		public string? Description { get; set; }
		public int TotalQuintity { get; set; }
		public bool IsPricedInCoins { get; set; } =false;
		public bool IsOnOffer { get; set; }=false;
		[Required]
		public decimal weight { get; set; }
		public int ItemsPerContainer { get; set; }
		[Required]
		public WeightUnit WeightUnit { get; set; }
		[Required]
		public virtual ICollection<QuantityByExpiry> QuantitiesByExpiry { get; set; }
		[Required]
		public virtual ICollection<TraderPricing> Pricing { get; set; }
		
		[ForeignKey("SubCategory")]
		public int SubCatId { get; set; }
		public virtual SubCategory SubCategory { get; set; }
		public virtual ICollection<User> FavoritedByUsers { get; set; } = new List<User>();


	}
}
