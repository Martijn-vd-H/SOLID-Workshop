using OrderModule.Core.Interfaces;

namespace OrderModule.Core.Services;

public class EmailService : IEmailService
{
    public void SendEmail(Email email)
    {
        // SMTP Client magic
        Console.WriteLine("Email sent:");
        Console.WriteLine($"To: {email.To}");
        Console.WriteLine($"From: {email.From}");
        Console.WriteLine($"Header: {email.Header}");
        Console.WriteLine($"Body: {email.Body}");
    }
}