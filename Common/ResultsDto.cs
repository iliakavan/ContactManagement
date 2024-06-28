namespace ContactManagementV2.Common;


public class ResultsDto<T> where T : class
{
    public T? Values { get; set; }
    public bool IsSuccessfull { get; set; }
}