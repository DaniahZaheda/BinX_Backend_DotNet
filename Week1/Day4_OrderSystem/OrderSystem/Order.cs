public class Order
{
    private int orderId;
    private Customer customer;
    private Product product;
    private int quantity;

    public int OrderId
    {
        get { return orderId; }
    }

    public Customer Customer
    {
        get { return customer; }
    }

    public Product Product
    {
        get { return product; }
    }

    public int Quantity
    {
        get { return quantity; }
        set
        {
            if (value > 0)
            {
                quantity = value;
            }
        }
    }

    public Order(int orderId, Customer customer, Product product, int quantity)
    {
        if (orderId > 0)
        {
            this.orderId = orderId;
        }

        this.customer = customer;
        this.product = product;
        Quantity = quantity;
    }

    public double CalculateTotal()
    {
        return Product.Price * Quantity;
    }

    public void DisplayOrder()
    {
        Console.WriteLine("Order ID: " + OrderId);
        Console.WriteLine("Customer: " + Customer.Name);
        Console.WriteLine("Product: " + Product.Name);
        Console.WriteLine("Quantity: " + Quantity);
        Console.WriteLine("Total Price: " + CalculateTotal() + " NIS");
    }
}