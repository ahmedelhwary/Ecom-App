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
        public async Task<IActionResult> get()
        {
            try
            {
                var products = await _unitOfWork.ProductRepository.GetAllAsync(x=>x.Category,x=>x.Photos);
                var result = mapper.Map<List<ProductDTO>>(products);
                if (products is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(result);
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
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400,ex.Message));
            }
        }
    }
}
