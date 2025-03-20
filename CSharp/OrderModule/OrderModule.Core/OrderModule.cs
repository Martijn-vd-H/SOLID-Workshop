using OrderModule.Core.Services;
using OrderModule.Core.Services.Calculator;
using OrderModule.Core.Services.EmailService;

namespace OrderModule.Core;

public class OrderModule
{
    private IEmailer _emailer;
    private IEmailComposer _emailComposer;
    private IValidator _validator;
    private IAPICaller _apiCaller;

    public OrderModule(IEmailer emailer, IEmailComposer emailComposer, IValidator validator, IAPICaller apiCaller)
    {
        _emailer = emailer;
        _emailComposer = emailComposer;
        _validator = validator;
        _apiCaller = apiCaller;
    }

    public void Order(HardwareType type, int number)
    {
        // Validation
        _validator.ValidateAmountToOrder(number, type);

        // Order hardware with api
        var result = _apiCaller.PlaceOrder(type, number);
        if (!result.HasSucceeded)
        {
            throw new Exception("Order failed");
        }

        // Calculate price
        var price = result.Price;
        
        // Compose and send email
        var address = "itbusiness@example.com";
        var orderDetails = $"{number} of {type}";
        var invoiceDetails = $"Customer email: {address}\nDetails: {orderDetails}\nPrice: {price}";
        
        var email = _emailComposer.ComposeEmail(address, "Invoice", invoiceDetails);

        _emailer.SendEmail(email);
        
        Console.WriteLine("Order processed");
    }
}
