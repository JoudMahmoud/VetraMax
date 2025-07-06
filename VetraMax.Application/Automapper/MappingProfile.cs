using AutoMapper;
using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Application.DTOs;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Entities.OwnedClasses;
//using AutoMapper.Execution.microsoft.dependencyinjection;

namespace VetraMax.Application.Automapper
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			//CreateMap<source, distination>
			CreateMap<RegisterUserDto, User>()
				.ForMember(dest => dest.TraderType, opt => opt.Ignore());

			CreateMap<AddressDto, Address>();

			CreateMap<TraderVerificationInfoDto, TraderVerificationInfo>();

			CreateMap<CategoryDto, Category>();
			CreateMap<Category, CategoryDto>();

			CreateMap<InsertSubCategoryDto, SubCategory>()
				.ForMember(dest => dest.CatId, opt => opt.MapFrom(src => src.CategoryId));
		}
	}

}
