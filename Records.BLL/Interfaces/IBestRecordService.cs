using Records.Data.DataTransferObjects;
using Records.Data.Interfaces;

namespace Records.BLL.Interfaces;

public interface IBestRecordService
{
    Task<IBaseResponse<BestRecordDto>> GetById(Guid id);
    Task<IBaseResponse<IEnumerable<BestRecordDto>>> Get();
    Task<IBaseResponse<string>> UpdateById(Guid id, BestRecordDto modelDto);
}