namespace Resources.Models;

public class Product
{

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProductName { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public decimal? Price { get; set; }

    public string FullProductName => $"{ProductName} {Category} {Price}";

}
