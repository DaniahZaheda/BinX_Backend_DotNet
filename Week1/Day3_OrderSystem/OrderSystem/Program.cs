class Program
{
    static void Main(string[] args)
    {
        OrderRequest request = new OrderRequest(
            1,
            "Sondos",
            "sondos@email.com",
            "Keyboard",
            100,
            2
        );

        Customer customer = new Customer(
            request.CustomerName,
            request.CustomerEmail
        );

        Product product = new Product(
            request.ProductName,
            request.ProductPrice
        );

        Order order = new Order(
            request.OrderId,
            customer,
            product,
            request.Quantity
        );

        Console.WriteLine("Order Details");
        Console.WriteLine("-------------");

        order.DisplayOrder();

        Console.WriteLine();
        Console.WriteLine("Using Polymorphism");
        Console.WriteLine("------------------");

        PrintInformation(customer);
        Console.WriteLine();
        PrintInformation(product);
    }

    static void PrintInformation(IPrintable item)
    {
        item.PrintDetails();
    }
}