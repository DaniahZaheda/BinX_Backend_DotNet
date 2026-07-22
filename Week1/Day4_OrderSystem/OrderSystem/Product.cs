public class Product : IPrintable
{
    private string name;
    private double price;

    public string Name
    {
        get { return name; }
        set
        {
            if (value != "")
            {
                name = value;
            }
        }
    }

    public double Price
    {
        get { return price; }
        set
        {
            if (value > 0)
            {
                price = value;
            }
        }
    }

    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public void PrintDetails()
    {
        Console.WriteLine("Product Name: " + Name);
        Console.WriteLine("Product Price: " + Price + " NIS");
    }
}