using Resources.Models;
using Resources.Services;
using Resources.Interfaces;
using System.ComponentModel.Design;

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
    public void CreateMenu(Product product)
    {
        Product product1 = new Product();

        Console.Clear();
        Console.WriteLine("CREATE NEW PRODUCT\n");
        Console.Write("Enter Productname: ");
        product.ProductName = Console.ReadLine() ?? "";

        decimal realPrice;
        while (true)
        {
            Console.Write("Enter productprice: ");
            var input = Console.ReadLine();
            if (decimal.TryParse(input, out realPrice) && realPrice > 0)  
                break;
            else
                Console.WriteLine("Invalid price. Please try again!");   
        }

        product.Price = realPrice;


        Console.Write("Enter Category: ");
        product.ProductCategory = new Category { Name = Console.ReadLine() ?? "" };

        var result = _productService.CreateProduct(product);

        if (result.Success)
            Console.WriteLine("Product was created successfully!");
        else if (result.Message!.Contains("exists"))
            Console.WriteLine("Product with the same ID already exists!");
        else
            Console.WriteLine("Something went wrong! Product was not created!");



        Console.WriteLine("Press any key to continue");
        Console.ReadKey();

    }
}
