using OrderModule.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderModule.Core.Services
{

    public class LaptopPriceStrategy : IHardwarePriceStrategy
    {
        public decimal CalculatePrice(int number)
        {
            return 1200 * number;
        }
    }

    public class MonitorPriceStrategy : IHardwarePriceStrategy
    {
        public decimal CalculatePrice(int number)
        {
            return 250 * number;
        }
    }

    public class DeskPriceStrategy : IHardwarePriceStrategy
    {
        public decimal CalculatePrice(int number)
        {
            return 550 * number;
        }
    }
}