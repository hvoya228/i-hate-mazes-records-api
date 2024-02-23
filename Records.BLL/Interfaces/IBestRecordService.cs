using Records.Data.DataTransferObjects;
using Records.Data.Interfaces;

namespace Records.BLL.Interfaces;

public interface IBestRecordService
{
    Task<IBaseResponse<BestRecordDto>> GetById(Guid id);
    Task<IBaseResponse<IEnumerable<BestRecordDto>>> Get();
    Task<IBaseResponse<string>> Insert(BestRecordDto? modelDto);
    Task<IBaseResponse<string>> UpdateById(Guid id, BestRecordDto modelDto);
    Task<IBaseResponse<string>> DeleteById(Guid id);
}