namespace OrderModule.Core.Interfaces;

public interface IPriceCalculator
{
    decimal CalculatePrice(int number);
}