using Records.DAL.Interfaces;
using Records.Data.Models;

namespace Records.DAL.Repositories;

public class BestRecordRepository : GenericRepository<BestRecord>, IBestRecordRepository
{
    public BestRecordRepository(RecordsContext databaseContext) : base(databaseContext)
    {
    }
}