using Microsoft.AspNetCore.Mvc;
using Records.BLL.Interfaces;
using Records.Data.DataTransferObjects;

namespace Records.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BestRecordController : ControllerBase
{
    private readonly IBestRecordService _service;

    public BestRecordController(IBestRecordService service)
    {
        _service = service;
    }
    
    [HttpGet("GetById")]
    public async Task<ActionResult<BestRecordDto>> GetById(Guid id)
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
    
    [HttpPut("Update")]
    public async Task<ActionResult> UpdateById([FromBody] BestRecordDto modelDto)
    {
        return Ok(await _service.Update(modelDto));
    }
}