using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrderModule.Core.Services.Calculator
{
    public class PriceCalculator : IPriceCalculator
    {
        public double Calculate(HardwareType hardwareType, int amount)
        {
            switch (hardwareType)
            {
                case HardwareType.Laptop:
                    return 1200 * amount;
                case HardwareType.Monitor:
                    return 250 * amount;
                case HardwareType.Desk:
                    return 550 * amount;
                default:
                    throw new ArgumentOutOfRangeException(nameof(hardwareType), hardwareType, null);
            }
        }
    }
}
