using ContactManagementV2.Services.CategoryService;
using ContactManagementV2.Services.CategoryService.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementV2.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var result = await _service.GetAll();
            if (!result.IsSuccessfull) 
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{ID}")]

        public async Task<IActionResult> Get(int ID) 
        {
            var result = await _service.GetbyId(ID);
            if (!result.IsSuccessfull) 
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryDto categorydto) 
        {
            var result = await _service.Add(categorydto);

            if (!result.IsSuccessfull) 
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("{ID}")]
        public async Task<IActionResult> Update(int ID, [FromBody] UpdateCategorydto updateCategorydto) 
        {
            var result = await _service.Update(ID, updateCategorydto);
            if (!result.IsSuccessfull) 
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{ID}")]

        public async Task<IActionResult> Delete(int ID) 
        {
            var result = await _service.Delete(ID);
            if (!result.IsSuccessfull) 
            {
                return NotFound();
            }
            return Ok(result);
        }

        
    }
}
