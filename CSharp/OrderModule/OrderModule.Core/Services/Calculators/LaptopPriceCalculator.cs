using OrderModule.Core.Interfaces;

namespace OrderModule.Core.Services.Calculators;

public class LaptopPriceCalculator : IPriceCalculator
{
    public decimal CalculatePrice(int number) => 1200 * number;
}