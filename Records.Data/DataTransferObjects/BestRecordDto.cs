namespace Records.Data.DataTransferObjects;

public class BestRecordDto
{
    public Guid Id { get; set; }
    public int TotalScore { get; set; }
    public int PinkScore { get; set; }
    public int GreenScore { get; set; }
    public Guid PlayerId { get; set; }
}