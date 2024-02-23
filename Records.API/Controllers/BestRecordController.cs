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
    
    [HttpGet("Get")]
    public async Task<ActionResult<IEnumerable<BestRecordDto>>> Get()
    {
        return Ok(await _service.Get());
    }
    
    [HttpPost]
    public async Task<ActionResult> Insert([FromBody] BestRecordDto modelDto)
    {
        return Ok(await _service.Insert(modelDto));
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateById(Guid id, [FromBody] BestRecordDto modelDto)
    {
        return Ok(await _service.UpdateById(id, modelDto));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteById(Guid id)
    {
        return Ok(await _service.DeleteById(id));
    }
}