using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderModule.Core.Services
{
    public interface IValidator
    {
        bool ValidateAmountToOrder(int amount, HardwareType type);
        bool ValidateApiResult(ApiResult result);
    }
}
