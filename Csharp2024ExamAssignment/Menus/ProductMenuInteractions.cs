
using Resources.Interfaces;
using Resources.Models;
using Resources.Services;



namespace Csharp2024ExamAssignment.Menus;

public class ProductMenuInteractions
{
    private ProductService _productService;



    public ProductMenuInteractions(IFileService fileService)
    {
        // Här skapar jag min ProductService och skickar in fileService
        _productService = new ProductService(fileService);
    }

    public void CreateProductMenu()
    {
        var product = new Product
        {
            ProductCategory = new Category()
        };

        CreateProductMenuDisplay(product);

        var result = _productService.CreateProduct(product);
        if (result.Success)
        {
            Console.WriteLine($"{result.Message}");
        }
        else
        {
            Console.WriteLine($"{result.Message}");
        }
        Console.ReadKey();
    }

    private void CreateProductMenuDisplay(Product product)
    {
        Console.WriteLine("Create a product!");
        Console.Write("Productname: ");
        product.ProductName = Console.ReadLine()!;

        Console.Write("Category: ");
        product.ProductCategory.Name = Console.ReadLine();

        Console.Write("Price: ");
        string? input = Console.ReadLine();

        if (decimal.TryParse(input, out decimal price))
        {
            product.Price = price;
        }
        else
        {
            Console.WriteLine("Invalid price input. Please enter a valid decimal number.");
        }



    }
}
