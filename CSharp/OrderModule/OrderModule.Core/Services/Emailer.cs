namespace OrderModule.Core.Services;

public class Emailer
{
    public static void SendEmail(Email email)
    {
        Console.WriteLine(email.Body);
    }
}