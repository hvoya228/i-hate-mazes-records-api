namespace Records.Data.Models;

public class BestRecord : BaseModel
{
    public int TotalScore { get; set; }
    public int PinkScore { get; set; }
    public int GreenScore { get; set; } 
    
    public Guid PlayerId { get; set; }
    public Player Player { get; set; } = null!;
}