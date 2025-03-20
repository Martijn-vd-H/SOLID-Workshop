using OrderModule.Core.Services;

namespace OrderModule.Core.Interfaces
{
    public interface IAPICaller
    {
        ApiResult PlaceOrder(HardwareType type, int number);
    }
}