using OrderModule.Core.Interfaces;

namespace OrderModule.Core
{
    public class DeskPriceStrategy : IPriceStrategy
    {
        public decimal CalculatePrice(int number)
        {
            return 550 * number;
        }
    }
}