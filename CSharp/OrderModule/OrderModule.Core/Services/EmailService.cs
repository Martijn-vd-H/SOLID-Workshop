namespace OrderModule.Core.Services;

public class EmailService
{
    public void SendEmail(Email email)
    {
        Console.WriteLine(email.Body);
    }
}