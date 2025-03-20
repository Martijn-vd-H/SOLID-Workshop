using OrderModule.Core.Interfaces;
using OrderModule.Core.Services;

public class PriceCalculator : IPriceCalculator
{
    private readonly Dictionary<HardwareType, IHardwarePriceStrategy> _priceStrategies;

    public PriceCalculator()
    {
        _priceStrategies = new Dictionary<HardwareType, IHardwarePriceStrategy>
        {
            { HardwareType.Laptop, new LaptopPriceStrategy() },
            { HardwareType.Monitor, new MonitorPriceStrategy() },
            { HardwareType.Desk, new DeskPriceStrategy() }
        };
    }

    public decimal CalculatePrice(HardwareType type, int number)
    {
        if (_priceStrategies.TryGetValue(type, out var strategy))
        {
            return strategy.CalculatePrice(number);
        }
        throw new ArgumentOutOfRangeException(nameof(type), type, null);
    }
}