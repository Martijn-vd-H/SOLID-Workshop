namespace OrderModule.Core.Interfaces;

public interface IPriceCalculatorFactory
{
    IPriceCalculator GetCalculator(HardwareType type);
}