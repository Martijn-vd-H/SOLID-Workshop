using OrderModule.Core.Interfaces;

namespace OrderModule.Core.Services;

public class OrderValidator : IOrderValidator
{
    public void Validate(HardwareType type, int number)
    {
        if (number < 1 || number > 30)
        {
            throw new ArgumentException($"Order {number} of type {type} seems incorrect");
        }
    }
}