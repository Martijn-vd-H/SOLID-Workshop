namespace OrderModule.Core.Services.EmailService;

public class Emailer : IEmailer
{
    private IEmailComposer _composer;
    public Emailer(IEmailComposer composer)
    {
        _composer = composer;
    }

    public void SendEmail(Email email)
    {
        Console.WriteLine(email.Body);
    }
}