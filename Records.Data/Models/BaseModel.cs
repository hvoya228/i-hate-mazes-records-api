namespace Records.Data.Models;

public class BaseModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
}