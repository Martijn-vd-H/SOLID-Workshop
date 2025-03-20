using OrderModule.Core.Interfaces;

public interface IPriceCalculator
{
    decimal CalculatePrice(int number);
}

public class LaptopPriceCalculator : IPriceCalculator
{
    public decimal CalculatePrice(int number) => 1200 * number;
}

public class MonitorPriceCalculator : IPriceCalculator
{
    public decimal CalculatePrice(int number) => 250 * number;
}

public class DeskPriceCalculator : IPriceCalculator
{
    public decimal CalculatePrice(int number) => 550 * number;
}

// Factory to get the right calculator dynamically
public interface IPriceCalculatorFactory
{
    IPriceCalculator GetCalculator(HardwareType type);
}

public class PriceCalculatorFactory : IPriceCalculatorFactory
{
    private readonly Dictionary<HardwareType, IPriceCalculator> _calculators;

    public PriceCalculatorFactory()
    {
        _calculators = new Dictionary<HardwareType, IPriceCalculator>
        {
            { HardwareType.Laptop, new LaptopPriceCalculator() },
            { HardwareType.Monitor, new MonitorPriceCalculator() },
            { HardwareType.Desk, new DeskPriceCalculator() }
        };
    }

    public IPriceCalculator GetCalculator(HardwareType type)
    {
        if (!_calculators.TryGetValue(type, out var calculator))
        {
            throw new ArgumentOutOfRangeException(nameof(type), $"No price calculator found for {type}");
        }
        return calculator;
    }
}

// Usage
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