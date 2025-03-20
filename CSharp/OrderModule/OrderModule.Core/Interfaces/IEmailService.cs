using OrderModule.Core.Services;

namespace OrderModule.Core.Interfaces;

public interface IEmailService
{
    void SendEmail(Email email);
}