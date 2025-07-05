using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities.OwnedClasses;

namespace VetraMax.Domain.Entities
{
    public class User : IdentityUser
	{
		public string? ImageUrl { get; set; }
		public bool IsActivate { get; set; } = true;

		[Required]
		public virtual Address Address { get; set; }
		public virtual TraderVerificationInfo? TraderVerificationInfo { get; set; }
		[Required]
		[ForeignKey("TraderType")]
		public int TraderTypeId { get; set; }
		public virtual TraderType? TraderType { get; set; }

		public virtual ICollection<Product> FavoriteProducts { get; set; } = new List<Product>();


		public virtual Line? Line { get; set; }
		public virtual Wallet Wallet { get; set; }
	}
}
