public class Customer : IPrintable
{
    private string name;
    private string email;

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

    public string Email
    {
        get { return email; }
        set
        {
            if (value != "")
            {
                email = value;
            }
        }
    }

    public Customer(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public void PrintDetails()
    {
        Console.WriteLine("Customer Name: " + Name);
        Console.WriteLine("Customer Email: " + Email);
    }
}