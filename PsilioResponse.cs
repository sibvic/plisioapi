namespace Sibvic.plisioapi;

public class PsilioResponse<T>
{
    public string Status { get; set; }
    public T Data { get; set; }
}
