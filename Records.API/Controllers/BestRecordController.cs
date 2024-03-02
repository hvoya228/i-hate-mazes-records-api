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
    
    [HttpPut]
    public async Task<ActionResult> UpdateById([FromBody] BestRecordDto modelDto)
    {
        return Ok(await _service.Update(modelDto));
    }
}