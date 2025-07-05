using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Enums;

namespace VetraMax.Domain.Entities
{
	public class WalletTransaction:Base
	{
		[Required]
		public float Amount { get; set; }
		[Required]
		public DateTime DateTime { get; set; }
		[Required]
		public ChargingMethod ChargingMethod { get; set; }
		[Required]
		public Status Status { get; set; }
		public TransactionType TransactionType { get; set; }
		[ForeignKey("Order")]
		public int OrderId { get; set; }
		public virtual Order Order { get; set; }
	
	}
}
