

// Factory to get the right calculator dynamically

// Usage
namespace OrderModule.Core.Services;

public class OrderService
{
    private readonly PriceCalculatorFactory _priceCalculatorFactory;

    public OrderService(PriceCalculatorFactory priceCalculatorFactory)
    {
        _priceCalculatorFactory = priceCalculatorFactory;
    }

    public decimal CalculateOrderPrice(HardwareType type, int quantity)
    {
        var calculator = _priceCalculatorFactory.GetCalculator(type);
        return calculator.CalculatePrice(quantity);
    }
}