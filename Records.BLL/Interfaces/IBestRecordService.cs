using Records.Data.DataTransferObjects;
using Records.Data.Interfaces;

namespace Records.BLL.Interfaces;

public interface IBestRecordService
{
    Task<IBaseResponse<BestRecordDto>> GetById(Guid id);
    Task<IBaseResponse<string>> Update(BestRecordDto modelDto);
}