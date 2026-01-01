using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> get(int? CategoryId, string sort = "a")
        {
            try
            {
                var products = await _unitOfWork.ProductRepository.GetAllAsync(sort, CategoryId);

                return Ok(products);
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
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(id, x => x.Category, x => x.Photos);
                if (product is null)
                    return BadRequest(new ResponseAPI(400, $"Not Found Product Id = {id}"));
                var result = mapper.Map<ProductDTO>(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add-Product")]
        public async Task<IActionResult> add([FromForm] AddProductDTO productDTO)
        {
            try
            {

                await _unitOfWork.ProductRepository.AddAsync(productDTO);
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400,ex.Message));
            }
        }
        [HttpPut("Update-Product")]
        public async Task<IActionResult> update([FromForm] UpdateProductDTO updateProductDTO)
        {
            try
            {
                await _unitOfWork.ProductRepository.UpdateAsync(updateProductDTO);
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
        [HttpDelete ("Delete-Product/{id:int}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(id, x => x.Photos, x => x.Category);
                await _unitOfWork.ProductRepository.DeleteAsync(product);
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
    }
}
