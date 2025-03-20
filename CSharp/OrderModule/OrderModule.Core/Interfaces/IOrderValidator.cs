namespace OrderModule.Core.Interfaces;

public interface IOrderValidator
{
    void Validate(HardwareType type, int number);
}
