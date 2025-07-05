using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Enums;

namespace VetraMax.Domain.Entities
{
	public class Order:Base
	{
		[Required]
		public decimal TotalPrice { get; set; }
		public decimal CleanPrice { get; set; }
		public decimal CashPrice { get; set; }

		[Required]
		public DateOnly DeliveryDate { get; set; }
		public DateTime OrderDate { get; set; }
		public string? CancellationReason { get; set; }

		[Required]
		public DeliveryState DeliveryState { get; set; }

		[Required]
		public PaymentMethod PaymentMethod { get; set; }

		[Required]
		public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
		public virtual WalletTransaction? Transaction { get; set; }

		[Required]
		[ForeignKey("User")]
		public string UserId { get; set; }
		public virtual User User { get; set; }

	}
}
