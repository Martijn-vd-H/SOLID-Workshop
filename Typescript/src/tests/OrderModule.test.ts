import { OrderModule, HardwareType } from '../core/OrderModule';

describe('OrderModule.test', () => {
  let orderModule: OrderModule;

  beforeEach(() => {
    orderModule = new OrderModule();
  });

  test('Order_Should_ThrowExceptionOnValidationFailed', () => {
    expect(() => orderModule.order(HardwareType.Laptop, 0)).toThrow("Order 0 of type Laptop seems incorrect");
  });

  test('Order_Should_CalculateTotalPrice', () => {
    // How do I prevent actual orders being placed or emails sent?
    orderModule.order(HardwareType.Laptop, 3);

    // How do I get the price?
    const price = 0;
    expect(price).toBe(3600);
  });
});