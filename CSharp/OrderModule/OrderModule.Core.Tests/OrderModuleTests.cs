using OrderModule.Core.Interfaces;
using OrderModule.Core.Services;

namespace OrderModule.Core.Tests
{
    // --- Fake Implementations for Testing ---

    public class FakeOrderValidator : IOrderValidator
    {
        public void Validate(HardwareType type, int number)
        {
            if (number < 1 || number > 30)
            {
                throw new ArgumentException($"Order {number} of type {type} seems incorrect");
            }
        }
    }

    public class FakeAPICaller : IAPICaller
    {
        public bool PlaceOrder(HardwareType type, int number)
        {
            // Always succeed in tests.
            return true;
        }
    }

    public class FakePriceCalculator : IPriceCalculator
    {
        public decimal CalculatePrice(HardwareType type, int number)
        {
            switch (type)
            {
                case HardwareType.Laptop:
                    return 1200 * number;
                case HardwareType.Monitor:
                    return 250 * number;
                case HardwareType.Desk:
                    return 550 * number;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public class FakeEmailService : IEmailService
    {
        public void SendEmail(Email email)
        {
            // Do nothing, or record email if needed for further assertions.
        }
    }

    public class FakeLogger : ILogger
    {
        public void LogError(string message)
        {
        }

        public void LogInfo(string message)
        {
        }
    }
    
    public class OrderModuleTests
    {
        private OrderModule _orderModule;

        [SetUp]
        public void Setup()
        {
            var orderValidator = new FakeOrderValidator();
            var apiCaller = new FakeAPICaller();
            var priceCalculator = new FakePriceCalculator();
            var emailService = new FakeEmailService();
            var logger = new FakeLogger();

            _orderModule = new OrderModule(orderValidator, apiCaller, priceCalculator, emailService, logger);
        }

        [Test]
        public void Order_Should_ThrowExceptionOnValidationFailed()
        {
            // Expect an exception when the order number is invalid
            Assert.Throws<ArgumentException>(() => _orderModule.Order(HardwareType.Laptop, 0));
        }

        [Test]
        public void Order_Should_CalculateTotalPrice()
        {
            // Act
            _orderModule.Order(HardwareType.Laptop, 3);

            // Assert: For 3 Laptops, the total price should be 3600.
            Assert.That(_orderModule.LastCalculatedPrice, Is.EqualTo(3600));
        }
    }
}
