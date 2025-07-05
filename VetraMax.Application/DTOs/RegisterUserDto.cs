using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetraMax.Application.DTOs
{
	public class RegisterUserDto
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		[Required]
		public AddressDto Address { get; set; }
		public string? ImageUrl { get; set; }
		public  TraderVerificationInfoDto? TraderVerificationInfo { get; set; }
		[Required]
		public int TraderId { get; set; }
	}
}
