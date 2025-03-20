using OrderModule.Core.Interfaces;
using OrderModule.Core.Services;

namespace OrderModule.Core;

public class OrderModule
    {
        private readonly IOrderValidator _orderValidator;
        private readonly IAPICaller _apiCaller;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        // Expose the last calculated price for testing purposes.
        public decimal LastCalculatedPrice { get; private set; }

        public OrderModule(IOrderValidator orderValidator,
                           IAPICaller apiCaller,
                           IEmailService emailService,
                           ILogger logger)
        {
            _orderValidator = orderValidator;
            _apiCaller = apiCaller;
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
                var apiResult = _apiCaller.PlaceOrder(type, number);
                if (!apiResult.HasSucceeded)
                {
                    _logger.LogError("API order placement failed");
                    throw new Exception("Order failed");
                }
                _logger.LogInfo("API order placement succeeded");

                // Use the API's price result
                LastCalculatedPrice = apiResult.Price;
                _logger.LogInfo($"Price received: {LastCalculatedPrice}");

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