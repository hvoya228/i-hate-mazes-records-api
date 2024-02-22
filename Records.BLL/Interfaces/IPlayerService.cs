using Records.Data.DataTransferObjects;
using Records.Data.Interfaces;

namespace Records.BLL.Interfaces;

public interface IPlayerService
{
    Task<IBaseResponse<PlayerDto>> GetById(Guid id);
    Task<IBaseResponse<IEnumerable<PlayerDto>>> Get();
    Task<IBaseResponse<string>> Insert(PlayerDto? modelDto);
    Task<IBaseResponse<string>> DeleteById(Guid id);
}