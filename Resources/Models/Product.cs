using System.Diagnostics;

namespace Resources.Models;

public class Product
{

    public string ProductId { get; set; } = Guid.NewGuid().ToString();
    public string ProductName { get; set; } = null!;
    public Category ProductCategory { get; set; } = null!;
    public decimal? Price { get; set; }
  
}
