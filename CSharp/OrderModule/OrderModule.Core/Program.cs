// Create the necessary dependencies

using OrderModule.Core.Interfaces;
using OrderModule.Core.Services;

ILogger logger = new ConsoleLogger();
IOrderValidator orderValidator = new OrderValidator();
IAPICaller apiCaller = new APICaller();
IPriceCalculator priceCalculator = new PriceCalculator();
IEmailService emailService = new EmailService(); 

// Instantiate the OrderModule with its dependencies
var orderModule = new OrderModule.Core.OrderModule(
    orderValidator,
    apiCaller,
    priceCalculator,
    emailService,
    logger);

// Process orders
orderModule.Order(HardwareType.Laptop, 3);
orderModule.Order(HardwareType.Monitor, 6);
orderModule.Order(HardwareType.Desk, 2);