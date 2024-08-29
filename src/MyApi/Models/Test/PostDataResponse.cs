using MyApi.Attributes;

namespace MyApi.Models.Test;

public class PostDataResponse
{
    public long Id { get; set; }
    [MyAttribute]
    public string? Name { get; set; }
}