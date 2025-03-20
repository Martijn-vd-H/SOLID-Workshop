using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderModule.Core.Services.Calculator
{
    public interface IPriceCalculator
    {
        double Calculate(HardwareType type, int amount);
    }
}
