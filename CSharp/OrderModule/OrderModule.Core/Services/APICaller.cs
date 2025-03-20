using System;
using OrderModule.Core.Interfaces;
using OrderModule.Core.Services;

namespace OrderModule.Core.Services
{
    public class APICaller : IAPICaller
    {
        private readonly IPriceCalculator _priceCalculator;

        public APICaller(IPriceCalculator priceCalculator)
        {
            _priceCalculator = priceCalculator;
        }

        public ApiResult PlaceOrder(HardwareType type, int number)
        {
            Console.WriteLine($"Ordering {number} of type {type}...");
            decimal price = _priceCalculator.CalculatePrice(type, number);
            return new ApiResult { HasSucceeded = true, Price = (int)price };
        }
    }
}