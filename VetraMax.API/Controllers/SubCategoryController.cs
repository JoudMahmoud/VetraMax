using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetraMax.Application.DTOs;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Interfaces;

namespace VetraMax.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubCategoryController : ControllerBase
	{
		#region Fields
		private readonly ISubCategoryRepository _subCatRepo;
		private readonly ICategoryRepository _catRepo;
		private readonly IMapper _mapper;
		#endregion

		#region Constructor
		public SubCategoryController(ISubCategoryRepository subCatRepo,ICategoryRepository catRepo, IMapper mapper)
		{
			_subCatRepo = subCatRepo;
			_catRepo = catRepo;
			_mapper = mapper;

		}
		#endregion

		#region CURD Operations
		[HttpPost]
		public async Task<ActionResult> InsertSubCategory(InsertSubCategoryDto subCategoryDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var existingCategory = await _catRepo.GetByIdAsync(subCategoryDto.CategoryId);
			if (existingCategory==null) { return BadRequest($"Category with ID {subCategoryDto.CategoryId} doesn't exist."); }
			var subCategory = _mapper.Map<SubCategory>(subCategoryDto);
			await _subCatRepo.InsertSubCategory(subCategory);
			var success = await _subCatRepo.SaveChanges();
			if (success)
			{
				return Ok(new { message = "SubCategory added successful", data = subCategoryDto });
			}
			return StatusCode(500, "Failed to save the subCategory.");

		}

		//[HttpGet]
		//public async Task<ActionResult<IEnumerable<SubCategoryd>>> GetAllCategories()
		//{
		//	var categories = await _catRepo.GetAllAsync();
		//	if (categories.Count() == 0)
		//	{
		//		return NotFound();
		//	}
		//	var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
		//	return Ok(categoriesDto);
		//}

		//[HttpGet("{id}")]
		//public async Task<ActionResult<CategoryDto?>> GetCategoryById([FromRoute] int id)
		//{
		//	var category = await _catRepo.GetByIdAsync(id);
		//	if (category == null) { return NotFound(); }
		//	var CategoryDto = _mapper.Map<CategoryDto>(category);
		//	return Ok(CategoryDto);
		//}

		//[HttpGet("ByName")]
		//public async Task<ActionResult<CategoryDto?>> GetCategoryById([FromQuery] string categoryName)
		//{
		//	var category = await _catRepo.GetByNameAsync(categoryName);
		//	if (category == null) { return NotFound(); }
		//	var CategoryDto = _mapper.Map<CategoryDto>(category);
		//	return Ok(CategoryDto);
		//}
		#endregion
	}
}
