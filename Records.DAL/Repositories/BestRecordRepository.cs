using Records.DAL.Interfaces;
using Records.Data.Models;

namespace Records.DAL.Repositories;

public class BestRecordRepository : GenericRepository<BestRecord>, IBestRecordRepository
{
    protected BestRecordRepository(RecordsContext databaseContext) : base(databaseContext)
    {
    }
}