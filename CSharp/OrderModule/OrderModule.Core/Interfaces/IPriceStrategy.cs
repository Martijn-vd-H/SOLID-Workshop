namespace OrderModule.Core.Interfaces
{
    public interface IPriceStrategy
    {
        decimal CalculatePrice(int number);
    }
}