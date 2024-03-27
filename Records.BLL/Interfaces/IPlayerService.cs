using Records.Data.DataTransferObjects;
using Records.Data.Interfaces;

namespace Records.BLL.Interfaces;

public interface IPlayerService
{
    Task<IBaseResponse<BestPlayerDto>> GetBestPlayer();
    Task<IBaseResponse<PlayerDto>> GetById(Guid id);
    Task<IBaseResponse<List<PlayerDto>>> Get();
    Task<IBaseResponse<PlayerDto>> Login(string username, string password);
    Task<IBaseResponse<PlayerDto>> Insert(PlayerDto? modelDto);
    Task<IBaseResponse<string>> DeleteById(Guid id);
}