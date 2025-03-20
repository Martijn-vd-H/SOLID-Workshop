export enum HardwareType {
    Laptop = "Laptop",
    Monitor = "Monitor",
    Desk = "Desk"
}

class APICaller {
    placeOrder(type: HardwareType, number: number): boolean {
        console.log(`Placing order for ${number} ${type}(s)...`);
        return true; // Simulate successful API call
    }
}

class Email {
    to: string;
    from: string;
    header: string;
    body: string;

    constructor(to: string, from: string, header: string, body: string) {
        this.to = to;
        this.from = from;
        this.header = header;
        this.body = body;
    }
}

class Emailer {
    static sendEmail(email: Email): void {
        console.log(`Sending email to: ${email.to}`);
        console.log(`Subject: ${email.header}`);
        console.log(`Body:\n${email.body}`);
    }
}
class EmailComposer {
    static composeEmail(address: string, orderDetails: string, invoiceDetails: string, type: HardwareType): Email {
            const email = new Email(
                address,
                "Ordermodule@example.com",
                `Invoice ${type}`,
                invoiceDetails
            );
            return email;
    }
}
class PriceCalculator {
    static calculatePrice(type: HardwareType, number: number): Number {
        // Calculate price
        let price = 0;
        switch (type) {
            case HardwareType.Laptop:
                price = 1200 * number;
                break;
            case HardwareType.Monitor:
                price = 250 * number;
                break;
            case HardwareType.Desk:
                price = 550 * number;
                break;
            default:
                throw new Error("Invalid hardware type");
        }
        return price;
    }
}
class NumberValidator {
    static validateNumber(type: HardwareType, number: number): Boolean {
        // Validation
        if (number < 1 || number > 30) {
            throw new Error(`Order ${number} of type ${type} seems incorrect`);
            return false;
        }
        return true;
    }
}
export class OrderModule {
    order(type: HardwareType, number: number): void {

        let valid = NumberValidator.validateNumber(type, number);

        if(valid){
            // Order hardware with API
            const apiCaller = new APICaller();
            const result = apiCaller.placeOrder(type, number);
            if (!result) {
                throw new Error("Order failed");
            }

            let price = PriceCalculator.calculatePrice(type, number);

            // Compose and send email
            const address = "itbusiness@example.com";
            const orderDetails = `${number} of ${type}`;
            const invoiceDetails = `Customer email: ${address}\nDetails: ${orderDetails}\nPrice: ${price}`;
            Emailer.sendEmail(EmailComposer.composeEmail(address,orderDetails,invoiceDetails, type));

            console.log("Order processed");
        }
    }
}

// Example usage
const orderModule = new OrderModule();
orderModule.order(HardwareType.Laptop, 3);
orderModule.order(HardwareType.Monitor, 6);
orderModule.order(HardwareType.Desk, 2);
