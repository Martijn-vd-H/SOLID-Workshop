using OrderModule.Core.Interfaces;

namespace OrderModule.Core.Services.Calculators;

public class MonitorPriceCalculator : IPriceCalculator
{
    public decimal CalculatePrice(int number) => 250 * number;
}