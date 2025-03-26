using OrderModule.Core.Interfaces;

namespace OrderModule.Core.Services.Calculators;

public class DeskPriceCalculator : IPriceCalculator
{
    public decimal CalculatePrice(int number) => 550 * number;
}