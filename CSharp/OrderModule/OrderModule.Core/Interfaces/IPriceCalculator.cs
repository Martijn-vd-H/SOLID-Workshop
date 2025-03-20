namespace OrderModule.Core.Interfaces;

public interface IPriceCalculator
{
    decimal CalculatePrice(HardwareType type, int number);
}