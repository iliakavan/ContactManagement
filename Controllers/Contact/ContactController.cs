using ContactManagementV2.Services.ContactService;
using ContactManagementV2.Services.ContactService.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementV2.Controllers.Contact;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class ContactController : ControllerBase
{
    private readonly IContactService _Service;

    public ContactController(IContactService service)
    {
        _Service = service;
    }
    
    
    [HttpGet("{ID}")]
    public async Task<IActionResult> Get(int ID)
    {
        var result = await _Service.Get(ID);
        if(!result.IsSuccessfull)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _Service.GetAll();
        if(!result.IsSuccessfull)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Contactdto contact)
    {
        var result = await _Service.Create(contact);
        if(!result.IsSuccessfull)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPatch("{ID}")]

    public async Task<IActionResult>Update(int ID, [FromBody] UpdateContactdto contact)
    {
        var result = await _Service.Update(ID, contact);
        if(!result.IsSuccessfull)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpDelete("{ID}")]
    public async Task<IActionResult> Delete(int ID)
    {
        var result = await _Service.Delete(ID);
        if(!result.IsSuccessfull)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}