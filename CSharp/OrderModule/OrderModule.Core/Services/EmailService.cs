using OrderModule.Core.Interfaces;
using OrderModule.Core.Services;

public class EmailService : IEmailService
{
    private readonly ILogger _logger;

    public EmailService(ILogger logger)
    {
        _logger = logger;
    }

    public void SendEmail(Email email)
    {
        try
        {
            // Implementation for sending email.
            Console.WriteLine("Email sent:");
            Console.WriteLine($"To: {email.To}");
            Console.WriteLine($"From: {email.From}");
            Console.WriteLine($"Header: {email.Header}");
            Console.WriteLine($"Body: {email.Body}");
            _logger.LogInfo($"Email successfully sent to {email.To}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to send email to {email.To}: {ex.Message}");
            throw;
        }
    }
}