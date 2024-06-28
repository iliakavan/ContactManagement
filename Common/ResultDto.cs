namespace ContactManagementV2.Common;

public class ResultDto<T> where T : class
{
    public T? Value { get; set; }
    public bool IsSuccessfull { get; set; }
    public string? Message { get; set; }
}
public class ResultDto
{
    public bool IsSuccessfull { get; set; }
    public string? Message { get; set; }
}