using OrderModule.Core.Services.Calculator;

namespace OrderModule.Core.Services;

public class APICaller : IAPICaller
{
    private IPriceCalculator _priceCalculator;
    public APICaller(IPriceCalculator priceCalculator)
    {
        _priceCalculator = priceCalculator;
    }

    public ApiResult PlaceOrder(HardwareType type, int amount)
    {
        Console.WriteLine($"Ordering {amount} of type {type}...");

        var price = _priceCalculator.Calculate(type, amount);

        var result = new ApiResult
        {
            HasSucceeded = true,
            Price = price
        };

        return result;
    }
}