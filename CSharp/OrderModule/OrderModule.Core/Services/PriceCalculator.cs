using OrderModule.Core.Interfaces;

namespace OrderModule.Core.Services
{
    public class PriceCalculator : IPriceCalculator
    {
        private readonly IDictionary<HardwareType, IPriceStrategy> _strategies;

        public PriceCalculator(IDictionary<HardwareType, IPriceStrategy> strategies)
        {
            _strategies = strategies;
        }

        public decimal CalculatePrice(HardwareType type, int number)
        {
            if (!_strategies.TryGetValue(type, out var strategy))
            {
                throw new ArgumentOutOfRangeException(nameof(type), type, "No price strategy found for the given hardware type.");
            }
            return strategy.CalculatePrice(number);
        }
    }
}