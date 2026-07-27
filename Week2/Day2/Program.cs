using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "Ali" },
            new Customer { Id = 2, Name = "Sara" },
            new Customer { Id = 3, Name = "Ahmad" },
            new Customer { Id = 4, Name = "Lina" },
            new Customer { Id = 5, Name = "Omar" },
            new Customer { Id = 6, Name = "Mona" }
        };

        var orders = new List<Order>
        {
            new Order
            {
                Id = 1, CustomerId = 1, Amount = 100,
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { Product = "Mouse" },
                    new OrderLine { Product = "Keyboard" }
                }
            },
            new Order
            {
                Id = 2, CustomerId = 2, Amount = 200,
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { Product = "Screen" }
                }
            },
            new Order
            {
                Id = 3, CustomerId = 3, Amount = 150,
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { Product = "Laptop" }
                }
            },
            new Order
            {
                Id = 4, CustomerId = 1, Amount = 300,
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { Product = "Headset" }
                }
            },
            new Order
            {
                Id = 5, CustomerId = 4, Amount = 250,
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { Product = "Camera" }
                }
            },
            new Order
            {
                Id = 6, CustomerId = 5, Amount = 400,
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { Product = "Printer" }
                }
            }
        };

        // GroupBy
        Console.WriteLine("GroupBy");
        var grouped = orders
            .GroupBy(o => o.CustomerId)
            .Select(g => new
            {
                CustomerId = g.Key,
                Total = g.Sum(o => o.Amount)
            });

        foreach (var item in grouped)
        {
            Console.WriteLine($"Customer {item.CustomerId}: {item.Total}");
        }

        Console.WriteLine();

        // Join
        Console.WriteLine("Join");
        var joined = customers.Join(
            orders,
            c => c.Id,
            o => o.CustomerId,
            (c, o) => new
            {
                c.Name,
                o.Amount
            });

        foreach (var item in joined)
        {
            Console.WriteLine($"{item.Name} - {item.Amount}");
        }

        Console.WriteLine();

        // SelectMany
        Console.WriteLine("SelectMany");
        var products = orders.SelectMany(o => o.OrderLines);

        foreach (var p in products)
        {
            Console.WriteLine(p.Product);
        }

        Console.WriteLine();

        // Deferred Execution
        Console.WriteLine("Deferred Execution");

        var query = customers.Where(c => c.Id > 3);

        customers.Add(new Customer { Id = 7, Name = "Noor" });

        foreach (var c in query)
        {
            Console.WriteLine(c.Name);
        }

  
    }
}