using Records.Data.Enums;

namespace Records.Data.Interfaces;

public interface IBaseResponse<T>
{
    public string Description { get; set; }
    public StatusCode StatusCode { get; set; }
    public int ResultsCount { get; set; }
    T Data { get; set; }
}