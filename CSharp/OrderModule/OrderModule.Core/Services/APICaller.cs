using OrderModule.Core.Interfaces;

namespace OrderModule.Core.Services;

public class APICaller : IAPICaller
{
    public bool PlaceOrder(HardwareType type, int number)
    {
        Console.WriteLine($"Ordering {number} of type {type}...");
        return true;
    }
}