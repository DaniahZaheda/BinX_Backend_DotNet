using System.Collections.Generic;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public double Amount { get; set; }

    public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
}