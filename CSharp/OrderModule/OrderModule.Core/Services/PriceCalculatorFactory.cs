using OrderModule.Core.Interfaces;
using OrderModule.Core.Services.Calculators;

namespace OrderModule.Core.Services;

public class PriceCalculatorFactory : IPriceCalculatorFactory
{
    private readonly Dictionary<HardwareType, IPriceCalculator> _calculators = new()
    {
        { HardwareType.Laptop, new LaptopPriceCalculator() },
        { HardwareType.Monitor, new MonitorPriceCalculator() },
        { HardwareType.Desk, new DeskPriceCalculator() }
    };

    public IPriceCalculator GetCalculator(HardwareType type)
    {
        if (!_calculators.TryGetValue(type, out var calculator))
        {
            throw new ArgumentOutOfRangeException(nameof(type), $"No price calculator found for {type}");
        }
        return calculator;
    }
}