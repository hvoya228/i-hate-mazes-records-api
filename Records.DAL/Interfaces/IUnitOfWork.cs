namespace Records.DAL.Interfaces;

public interface IUnitOfWork
{
    IPlayerRepository PlayerRepository { get; }
    IBestRecordRepository BestRecordRepository { get; }
    Task SaveChangesAsync();
}