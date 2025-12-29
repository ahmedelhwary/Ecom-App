using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.interfaces;
using Ecom.infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
                if (categories is null)
                    return BadRequest(new ResponseAPI(400));

                return Ok(categories);  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-id/{id:int}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category is null)
                    return BadRequest(new ResponseAPI(400, $"Not Found Category Id = {id}"));
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-category")]
        public async Task<IActionResult> addCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                await _unitOfWork.CategoryRepository.AddAsync(category);
                return Ok(new ResponseAPI (200, "Item has been added"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-category")]
        public async Task<IActionResult> updateCategory(UpdateCategoryDTO categoryDTO)
        {
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                await _unitOfWork.CategoryRepository.UpdateAsync(category);
                return Ok(new ResponseAPI(200, "Item has been updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete-category/{id:int}")]
        public async Task<IActionResult> deleteCategory(int id)
        {
            try
            {
                await _unitOfWork.CategoryRepository.DeleteAsync(id);
                return Ok(new ResponseAPI(200, "Item has been deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
