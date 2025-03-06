namespace OrderModule.Core.Tests;

public class OrderModuleTests
{
    [Test]
    public void Order_Should_ThrowExceptionOnValidationFailed()
    {
        // Arrange
        var orderModule = new OrderModule();
        
        // If validation passes, an order wil be placed and an email will be sent... a bit risky
        // Act/Assert
        Assert.Throws<ArgumentException>(() => orderModule.Order(HardwareType.Laptop, 0));
    } 
    
    [Test]
    public void Order_Should_CalculateTotalPrice()
    {
        // Arrange
        var orderModule = new OrderModule();
        // How do I prevent actual orders being placed or emails sent?
        
        // Act
        orderModule.Order(HardwareType.Laptop, 3);
        
        // Assert
        // How do I get the price?
        var price = 0;
        Assert.That(price, Is.EqualTo(3600));
    }
}
