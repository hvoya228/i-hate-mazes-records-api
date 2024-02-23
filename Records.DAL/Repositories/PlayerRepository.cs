using Records.DAL.Interfaces;
using Records.Data.Models;

namespace Records.DAL.Repositories;

public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
{
    public PlayerRepository(RecordsContext databaseContext) : base(databaseContext)
    {
    }
}