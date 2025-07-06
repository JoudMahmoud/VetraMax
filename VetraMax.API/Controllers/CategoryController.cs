using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetraMax.Application.DTOs;
using VetraMax.Domain.Entities;
using VetraMax.Domain.Interfaces;
using VetraMax.Infrastructure.Repositories;

namespace VetraMax.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		#region Fields
		private readonly ICategoryRepository _catRepo;
		private readonly IMapper _mapper;
		#endregion

		#region Constructor
		public CategoryController(ICategoryRepository catRepo, IMapper mapper)
		{
			_catRepo = catRepo;
			_mapper = mapper;
		}
		#endregion

		#region CURD Operations
		[HttpPost]
		public async Task<ActionResult> InsertCategory(CategoryDto categoryDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var category = _mapper.Map<Category>(categoryDto);
			await _catRepo.InsertCategory(category);
			var success = await _catRepo.SaveChanges();
			if (success)
			{
				return Ok(new { message = "Category added successful", data = categoryDto });
			}
			return StatusCode(500, "Failed to save the category.");

		}
		
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
		{
			var categories = await _catRepo.GetAllAsync();
			if (categories.Count() == 0)
			{
				return NotFound();
			}
			var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
			return Ok(categoriesDto);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CategoryDto?>> GetCategoryById([FromRoute] int id)
		{
			var category = await _catRepo.GetByIdAsync(id);
			if (category == null) { return NotFound(); }
			var CategoryDto = _mapper.Map<CategoryDto>(category);
			return Ok(CategoryDto);
		}

		[HttpGet("ByName")]
		public async Task<ActionResult<CategoryDto?>> GetCategoryById([FromQuery] string categoryName)
		{
			var category = await _catRepo.GetByNameAsync(categoryName);
			if (category == null) { return NotFound(); }
			var CategoryDto = _mapper.Map<CategoryDto>(category);
			return Ok(CategoryDto);
		}
		#endregion
	}
}
