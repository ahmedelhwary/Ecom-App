using AutoMapper;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecom.API.Controllers
{

    public class BugController : BaseController
    {
        public BugController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        [HttpGet("not-found")]
        public async Task<IActionResult> GetNotFound()
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(100);
            if (category == null) return NotFound();
            return Ok(category);
        }
        [HttpGet("server-errror")]
        public async Task<IActionResult> GetserverError()
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(100);
            category.Name = "";
            return Ok(category);
        }
        [HttpGet("bad-request/{Id:int}")]
        public async Task<IActionResult> GetBadRequest(int id)
        {
            return Ok();
        }
        [HttpGet("bad-request/")]
        public async Task<IActionResult> GetBadRequest()
        {
            return BadRequest();
        }
    }
}
