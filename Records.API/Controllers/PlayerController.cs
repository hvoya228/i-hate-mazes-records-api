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
    public async Task<ActionResult<BestPlayerDto>> GetBestPlayer()
    {
        var response = await _service.GetBestPlayer();

        return response.StatusCode switch
        {
            Data.Enums.StatusCode.NotFound => NotFound(response),
            Data.Enums.StatusCode.InternalServerError => StatusCode(500, response),
            Data.Enums.StatusCode.BadRequest => BadRequest(response),
            _ => Ok(response.Data)
        };
    }
    
    [HttpGet("GetById")]
    public async Task<ActionResult<PlayerDto>> GetById(Guid id)
    {
        var response = await _service.GetById(id);

        return response.StatusCode switch
        {
            Data.Enums.StatusCode.NotFound => NotFound(response),
            Data.Enums.StatusCode.InternalServerError => StatusCode(500, response),
            Data.Enums.StatusCode.BadRequest => BadRequest(response),
            _ => Ok(response.Data)
        };
    }
    
    [HttpGet("Get")]
    public async Task<ActionResult<List<PlayerDto>>> Get()
    {
        var response = await _service.Get();

        return response.StatusCode switch
        {
            Data.Enums.StatusCode.NotFound => NotFound(response),
            Data.Enums.StatusCode.InternalServerError => StatusCode(500, response),
            Data.Enums.StatusCode.BadRequest => BadRequest(response),
            _ => Ok(response.Data)
        };
    }
    
    [HttpGet("Login")]
    public async Task<ActionResult<PlayerDto>> Login(string username, string password)
    {
        var response = await _service.Login(username, password);

        return response.StatusCode switch
        {
            Data.Enums.StatusCode.NotFound => NotFound(response),
            Data.Enums.StatusCode.InternalServerError => StatusCode(500, response),
            Data.Enums.StatusCode.BadRequest => BadRequest(response),
            _ => Ok(response.Data)
        };
    }
    
    [HttpPost("Register")]
    public async Task<ActionResult<PlayerDto>> Insert([FromBody] PlayerDto modelDto)
    {
        var response = await _service.Insert(modelDto);
        
        return response.StatusCode switch
        {
            Data.Enums.StatusCode.NotFound => NotFound(response),
            Data.Enums.StatusCode.InternalServerError => StatusCode(500, response),
            Data.Enums.StatusCode.BadRequest => BadRequest(response),
            _ => Ok(response.Data)
        };
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteById(Guid id)
    {
        return Ok(await _service.DeleteById(id));
    }
}