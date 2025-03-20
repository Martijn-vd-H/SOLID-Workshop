// Create the necessary dependencies

using OrderModule.Core;
using OrderModule.Core.Interfaces;
using OrderModule.Core.Services;

ILogger logger = new ConsoleLogger();
IOrderValidator orderValidator = new OrderValidator();

// Setup the Strategy pattern for price calculation.
var priceStrategies = new Dictionary<HardwareType, IPriceStrategy>
{
    { HardwareType.Laptop, new LaptopPriceStrategy() },
    { HardwareType.Monitor, new MonitorPriceStrategy() },
    { HardwareType.Desk, new DeskPriceStrategy() }
};

IPriceCalculator priceCalculator = new PriceCalculator(priceStrategies);

// APICaller now depends on the PriceCalculator.
IAPICaller apiCaller = new APICaller(priceCalculator);

IEmailService emailService = new EmailService(logger);

// Instantiate the OrderModule with its dependencies
var orderModule = new OrderModule.Core.OrderModule(orderValidator, apiCaller, emailService, logger);

// Process orders
orderModule.Order(HardwareType.Laptop, 3);
orderModule.Order(HardwareType.Monitor, 6);
orderModule.Order(HardwareType.Desk, 2);