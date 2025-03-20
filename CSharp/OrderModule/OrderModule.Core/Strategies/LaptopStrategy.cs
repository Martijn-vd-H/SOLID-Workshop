using OrderModule.Core.Interfaces;

namespace OrderModule.Core
{
    public class LaptopPriceStrategy : IPriceStrategy
    {
        public decimal CalculatePrice(int number)
        {
            return 1200 * number;
        }
    }
}