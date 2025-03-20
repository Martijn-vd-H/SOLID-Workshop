using OrderModule.Core.Interfaces;
using OrderModule.Core.Services;

namespace OrderModule.Core;

public class OrderModule
{
    private readonly IOrderValidator _orderValidator;
    private readonly IAPICaller _apiCaller;
    private readonly IPriceCalculatorFactory _priceCalculatorFactory;
    private readonly IEmailService _emailService;
    private readonly ILogger _logger;
    
    public decimal LastCalculatedPrice { get; private set; }


    public OrderModule(IOrderValidator orderValidator,
        IAPICaller apiCaller,
        IPriceCalculatorFactory priceCalculatorFactory,
        IEmailService emailService,
        ILogger logger)
    {
        _orderValidator = orderValidator;
        _apiCaller = apiCaller;
        _priceCalculatorFactory = priceCalculatorFactory;
        _emailService = emailService;
        _logger = logger;
    }

    public void Order(HardwareType type, int number)
    {
        try
        {
            _logger.LogInfo("Starting order process");

            // Validate the order
            _orderValidator.Validate(type, number);
            _logger.LogInfo("Order validated");

            // Place the order via API
            _logger.LogInfo("Placing order via API");
            var result = _apiCaller.PlaceOrder(type, number);
            if (!result)
            {
                _logger.LogError("API order placement failed");
                throw new Exception("Order failed");
            }
            _logger.LogInfo("API order placement succeeded");

            // Calculate price and record it
            LastCalculatedPrice = _priceCalculatorFactory.GetCalculator(type).CalculatePrice(number);
            _logger.LogInfo($"Price calculated: {LastCalculatedPrice}");

            // Compose and send email
            var address = "itbusiness@example.com";
            var orderDetails = $"{number} of {type}";
            var invoiceDetails = $"Customer email: {address}\nDetails: {orderDetails}\nPrice: {LastCalculatedPrice}";
            var email = new Email
            {
                To = address,
                From = "Ordermodule@example.com",
                Header = $"Invoice {type}",
                Body = invoiceDetails,
            };
            _emailService.SendEmail(email);
            _logger.LogInfo("Order email sent");

            Console.WriteLine("Order processed");
            _logger.LogInfo("Order process completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Order process encountered an error: {ex.Message}");
            throw;
        }
    }
}
