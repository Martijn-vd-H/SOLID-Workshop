using OrderModule.Core.Interfaces;

namespace OrderModule.Core
{
    public class MonitorPriceStrategy : IPriceStrategy
    {
        public decimal CalculatePrice(int number)
        {
            return 250 * number;
        }
    }
}