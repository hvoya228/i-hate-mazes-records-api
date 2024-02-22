using Records.DAL.Interfaces;

namespace Records.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly RecordsContext _databaseContext;
    public IPlayerRepository PlayerRepository { get; }
    public IBestRecordRepository BestRecordRepository { get; }
    
    public UnitOfWork(
        RecordsContext databaseContext, 
        IPlayerRepository playerRepository, 
        IBestRecordRepository bestRecordRepository)
    {
        _databaseContext = databaseContext;
        PlayerRepository = playerRepository;
        BestRecordRepository = bestRecordRepository;
    }
    
    public async Task SaveChangesAsync()
    {
        await _databaseContext.SaveChangesAsync();
    }
}