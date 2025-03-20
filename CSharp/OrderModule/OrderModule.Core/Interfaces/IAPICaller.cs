namespace OrderModule.Core.Interfaces;

public interface IAPICaller
{
    bool PlaceOrder(HardwareType type, int number);
}