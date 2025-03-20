namespace OrderModule.Core.Services;

public class OrderClient
{
    public bool PlaceOrder(HardwareType type, int number)
    {
        Console.WriteLine($"Ordering {number} of type {type}...");
        return true;
    }
}