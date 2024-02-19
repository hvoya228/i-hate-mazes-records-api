namespace Records.Data.Models;

public class Player : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public Guid BestRecordId { get; set; }
    public BestRecord BestRecord { get; set; } = null!;
}