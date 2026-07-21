public record OrderRequest(
    int OrderId,
    string CustomerName,
    string CustomerEmail,
    string ProductName,
    double ProductPrice,
    int Quantity
);