using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Domain.Entities
{
	
	public class TraderVerificationInfo:Base
	{
		public string? IdImageFront { get; set; }
		public string? IdImageBack { get; set; }
		public string? NationalNum { get; set; }
		public string? TaxCard { get; set; }
		public string? CommercialRegister { get; set; }
		public string? CommercialRegisterImage { get; set; }
		public string? TaxCardImage { get; set; }


		[Required]
		[ForeignKey("User")]
		public string UserId { get; set; }
		public virtual User User { get; set; }
	}
}
