using OrderModule.Core.Interfaces;

namespace OrderModule.Core.Services;

public class ApiResult
{
    public bool HasSucceeded { get; set; }
    public int Price { get; set; }
}