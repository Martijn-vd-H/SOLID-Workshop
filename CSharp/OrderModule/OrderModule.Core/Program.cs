using OrderModule.Core.Services;

var emailService = new EmailService();

var orderModule = new OrderModule.Core.OrderModule(emailService);

orderModule.Order(HardwareType.Laptop, 3);
orderModule.Order(HardwareType.Monitor, 6);
orderModule.Order(HardwareType.Desk, 2);
