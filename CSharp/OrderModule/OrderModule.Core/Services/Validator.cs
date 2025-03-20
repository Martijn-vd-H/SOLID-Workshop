using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrderModule.Core.Services
{
    public class Validator : IValidator
    {
        public bool ValidateAmountToOrder(int amount, HardwareType type)
        {
            if (amount < 1 || amount > 30)
            {
                throw new ArgumentException($"Order {amount} of type {type} seems incorrect");
            }
            return true;
        }

        public bool ValidateApiResult(ApiResult result)
        {
            if (!result.HasSucceeded)
            {
                throw new Exception("Order failed");
            }

            return true;
        }
    }
}