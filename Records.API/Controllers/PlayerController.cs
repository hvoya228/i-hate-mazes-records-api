using Microsoft.AspNetCore.Mvc;
using Records.BLL.Interfaces;
using Records.Data.DataTransferObjects;

namespace Records.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _service;

    public PlayerController(IPlayerService service)
    {
        _service = service;
    }
    
    [HttpGet("GetBestPlayer")]
    public async Task<ActionResult<BestPlayerDto>> Get()
    {
        return Ok(await _service.GetBestPlayer());
    }
    
    [HttpGet("GetById")]
    public async Task<ActionResult<PlayerDto>> GetById(Guid id)
    {
        return Ok(await _service.GetById(id));
    }
    
    [HttpPost]
    public async Task<ActionResult<PlayerDto>> Insert([FromBody] PlayerDto modelDto)
    {
        return Ok(await _service.Insert(modelDto));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteById(Guid id)
    {
        return Ok(await _service.DeleteById(id));
    }
}