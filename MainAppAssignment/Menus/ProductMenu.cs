using Resources.Models;
using Resources.Services;
using Resources.Interfaces;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace MainAppAssignment.Menus;

public class ProductMenu
{
    private readonly IProductService<Product, Product> _productService;
    private readonly IFileService _fileService;


    public ProductMenu()
    {
        _fileService = new FileService(); 
        _productService = new ProductService(_fileService);
    }
    public void CreateProductMenu()
    {
        Product product = new Product();

        Console.Clear();
        Console.WriteLine("CREATE NEW PRODUCT\n");
        Console.Write("Enter Productname: ");
        product.ProductName = Console.ReadLine()!;

        decimal realPrice;

        while (true)
        {
            Console.Write("Enter the price of the product: ");
            var input = Console.ReadLine()!;
            if (decimal.TryParse(input, out realPrice) && realPrice > 0)  
                break;
            else
                Console.WriteLine("Invalid price. Please try again!");   
        }

        product.Price = realPrice;


        Console.Write("Enter Category: ");
        product.ProductCategory = new Category { Name = Console.ReadLine()!};

        var result = _productService.CreateProduct(product);

        Console.WriteLine(result.Message);
        Console.WriteLine("Press any key to continue");


    }

    public void ShowAllProducts()
    {
        var content = _productService.GetAllProducts();

        Console.Clear();
        Console.WriteLine("Here is all of our Products!\n");

        if (content.Success && content.Result != null)
        {
            var productList = content.Result;

            foreach (Product product in productList)
            {

                Console.WriteLine($"<{product.ProductCategory.Name}> {product.ProductName}");
                Console.WriteLine($"Price: {product.Price}\n");
            }
        }
        else
            Console.WriteLine("\nNo products found or an error occured!");
            Console.WriteLine("Press any key to continue");

    }

}
