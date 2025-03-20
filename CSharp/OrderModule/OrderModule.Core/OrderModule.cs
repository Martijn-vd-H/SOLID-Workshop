using OrderModule.Core.Services;

namespace OrderModule.Core;

public class OrderModule(EmailService emailService)
{
    private readonly EmailService _emailService = emailService;

    public void Order(HardwareType type, int number)
    {
        ValidateOrder(type, number);

        SubmitOrder(type, number);

        var price = CalculatePrice(type, number);

        var email = ComposeEmail(type, number, price);
        _emailService.SendEmail(email);
        
        Console.WriteLine("Order processed");
    }

    private static Email ComposeEmail(HardwareType type, int number, int price)
    {
        // Compose and send email
        var address = "itbusiness@example.com";
        var orderDetails = $"{number} of {type}";
        var invoiceDetails = $"Customer email: {address}\nDetails: {orderDetails}\nPrice: {price}";
        var email = new Email()
        {
            To = address,
            From = "Ordermodule@example.com",
            Header = $"Invoice {type}",
            Body = invoiceDetails,
        };
        return email;
    }

    public static int CalculatePrice(HardwareType type, int number)
    {
        // Calculate price
        var price = 0;
        switch (type)
        {
            case HardwareType.Laptop:
                price = 1200 * number;
                break;
            case HardwareType.Monitor:
                price = 250 * number;
                break;
            case HardwareType.Desk:
                price = 550 * number;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        return price;
    }

    private static void SubmitOrder(HardwareType type, int number)
    {
        // Order hardware with api
        var apiCaller = new OrderClient();
        var result = apiCaller.PlaceOrder(type, number);
        if (!result)
        {
            throw new Exception("Order failed");
        }
    }

    private static void ValidateOrder(HardwareType type, int number)
    {
        // Validation
        if (number < 1 || number > 30)
        {
            throw new ArgumentException($"Order {number} of type {type} seems incorrect");
        }
    }
}
