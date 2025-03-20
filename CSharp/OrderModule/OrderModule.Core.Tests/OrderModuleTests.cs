using System;
using System.Collections.Generic;
using NUnit.Framework;
using OrderModule.Core;
using OrderModule.Core.Interfaces;
using OrderModule.Core.Services;

namespace OrderModule.Core.Tests
{
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

    public class FakeEmailService : IEmailService
    {
        public void SendEmail(Email email)
        {
            // Do nothing (or record the email details if needed for further assertions)
        }
    }

    public class FakeLogger : ILogger
    {
        public void LogError(string message)
        {
            // Optionally record log messages for verification.
        }

        public void LogInfo(string message)
        {
            // Optionally record log messages for verification.
        }
    }

    // --- Unit Tests ---

    public class OrderModuleTests
    {
        private OrderModule _orderModule;

        [SetUp]
        public void Setup()
        {
            IOrderValidator orderValidator = new FakeOrderValidator();

            // Setup price strategies for tests.
            var priceStrategies = new Dictionary<HardwareType, IPriceStrategy>
            {
                { HardwareType.Laptop, new LaptopPriceStrategy() },
                { HardwareType.Monitor, new MonitorPriceStrategy() },
                { HardwareType.Desk, new DeskPriceStrategy() }
            };

            IPriceCalculator priceCalculator = new PriceCalculator(priceStrategies);
            IAPICaller apiCaller = new APICaller(priceCalculator);
            IEmailService emailService = new FakeEmailService();
            ILogger logger = new FakeLogger();

            _orderModule = new OrderModule(orderValidator, apiCaller, emailService, logger);
        }

        [Test]
        public void Order_Should_ThrowExceptionOnValidationFailed()
        {
            // Expect an exception when the order number is invalid.
            Assert.Throws<ArgumentException>(() => _orderModule.Order(HardwareType.Laptop, 0));
        }

        [Test]
        public void Order_Should_CalculateTotalPrice_ForLaptop()
        {
            // Act: Order 3 laptops.
            _orderModule.Order(HardwareType.Laptop, 3);

            // Assert: For 3 laptops, the price should be 3600 (3 * 1200).
            Assert.That(_orderModule.LastCalculatedPrice, Is.EqualTo(3600));
        }

        [Test]
        public void Order_Should_CalculateTotalPrice_ForMonitor()
        {
            // Act: Order 6 monitors.
            _orderModule.Order(HardwareType.Monitor, 6);

            // Assert: For 6 monitors, the price should be 1500 (6 * 250).
            Assert.That(_orderModule.LastCalculatedPrice, Is.EqualTo(1500));
        }

        [Test]
        public void Order_Should_CalculateTotalPrice_ForDesk()
        {
            // Act: Order 2 desks.
            _orderModule.Order(HardwareType.Desk, 2);

            // Assert: For 2 desks, the price should be 1100 (2 * 550).
            Assert.That(_orderModule.LastCalculatedPrice, Is.EqualTo(1100));
        }

        [TestCase(HardwareType.Laptop, 5, 6000)]
        [TestCase(HardwareType.Monitor, 4, 1000)]
        [TestCase(HardwareType.Desk, 3, 1650)]
        public void Order_Should_CalculateTotalPrice_ForVariousHardwareTypes(HardwareType type, int quantity, decimal expectedPrice)
        {
            // Act: Order the specified hardware type.
            _orderModule.Order(type, quantity);

            // Assert: The calculated price matches the expected price.
            Assert.That(_orderModule.LastCalculatedPrice, Is.EqualTo(expectedPrice));
        }
    }
}
