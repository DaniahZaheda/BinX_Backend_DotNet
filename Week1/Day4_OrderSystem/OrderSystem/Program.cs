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

        // Day 4 - Collections and LINQ

        List<Product> products = new List<Product>();

        products.Add(product);
        products.Add(new Product("Mouse", 50));
        products.Add(new Product("Monitor", 500));
        products.Add(new Product("USB", 20));

        Console.WriteLine();
        Console.WriteLine("All Products");
        Console.WriteLine("------------");

        foreach (Product item in products)
        {
            Console.WriteLine(item.Name + " - " + item.Price + " NIS");
        }

        Console.WriteLine();
        Console.WriteLine("Products with price greater than 50");

        var expensiveProducts =
            products.Where(item => item.Price > 50);

        foreach (Product item in expensiveProducts)
        {
            Console.WriteLine(item.Name + " - " + item.Price + " NIS");
        }

        Console.WriteLine();
        Console.WriteLine("Products ordered by price");

        var orderedProducts =
            products.OrderBy(item => item.Price);

        foreach (Product item in orderedProducts)
        {
            Console.WriteLine(item.Name + " - " + item.Price + " NIS");
        }

        Console.WriteLine();
        Console.WriteLine("Product Names");

        var productNames =
            products.Select(item => item.Name);

        foreach (string name in productNames)
        {
            Console.WriteLine(name);
        }

        Console.WriteLine();
        Console.WriteLine("Number of products: " + products.Count());

        bool mouseExists =
            products.Any(item => item.Name == "Mouse");

        Console.WriteLine("Is Mouse available? " + mouseExists);
    }

    static void PrintInformation(IPrintable item)
    {
        item.PrintDetails();
    }
}