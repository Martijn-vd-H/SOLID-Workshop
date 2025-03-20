using OrderModule.Core.Services;

namespace OrderModule.Core.Tests;

public class OrderModuleTests
{
    private EmailService _emailService;

    [OneTimeSetUp]
    public void Setup()
    {
        _emailService = new EmailService();
    }
    
    [Test]
    public void Order_Should_ThrowExceptionOnValidationFailed()
    {
        // Arrange
        var orderModule = new OrderModule(_emailService);
        
        // If validation passes, an order wil be placed and an email will be sent... a bit risky
        // Act/Assert
        Assert.Throws<ArgumentException>(() => orderModule.Order(HardwareType.Laptop, 0));
    } 
    
    [Test]
    public void Order_Should_CalculateTotalPrice()
    {
        // Arrange
        var orderModule = new OrderModule(_emailService);
        // How do I prevent actual orders being placed or emails sent?
        
        // Act
        orderModule.Order(HardwareType.Laptop, 3);
        
        // Assert
        // How do I get the price?
        var price = OrderModule.CalculatePrice(HardwareType.Laptop, 3);
        Assert.That(price, Is.EqualTo(3600));
    }
}
