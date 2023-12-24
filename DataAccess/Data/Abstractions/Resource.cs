namespace DataAccess.Data.Abstractions;

public class Resource<T> : ResourceBase where T : ResourceBase
{
    public T Data { get; set; }
}