using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderModule.Core.Services
{
    public interface IAPICaller
    {
        ApiResult PlaceOrder(HardwareType type, int amount);
    }
}
