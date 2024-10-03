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
        Console.WriteLine("== CREATE NEW PRODUCT ==\n");

        Console.Write("Enter Category: ");
        product.ProductCategory = new Category { Name = Console.ReadLine()! };

        Console.Write("Enter Productname: ");
        product.ProductName = Console.ReadLine()!;

        Console.Write("Enter a description: ");
        product.ProductDescription = Console.ReadLine()!;

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

        var result = _productService.CreateProduct(product);

        Console.WriteLine(result.Message);
        Console.WriteLine("Press any key to continue");


    }

    public void ShowAllProducts()
    {
        var response = _productService.GetAllProducts();

        Console.Clear();
        Console.WriteLine("Here is all of our Products!\n");

        if (response.Success && response.Result != null)
        {
            var productList = response.Result;

            foreach (Product product in productList)
            {
                string realPrice = product.Price.HasValue ? product.Price.Value.ToString("0.00") : "No price set";
                Console.WriteLine($"[{product.ProductCategory.Name}]\n{product.ProductName} - {product.ProductDescription}\nPrice: {realPrice}");

                Console.WriteLine($"ProductID: {product.ProductId}\n");
            }
        }
        else
            Console.WriteLine("\nNo products found or an error occured!");
            Console.WriteLine("Press any key to continue");

    }

    public void ShowOneProduct()
    {
        Console.Clear();
        Console.WriteLine("== PRODUCTLIBRARY ==\n");
        Console.WriteLine("Which product do you want to update?");
        Console.Write("Enter product ID: ");
        var productId = Console.ReadLine()!;

        var response = _productService.GetAllProducts();

        if (response.Success && response.Result != null)
        {
            var productList = response.Result; 
            var productToUpdate = productList.FirstOrDefault(x => x.ProductId == productId);

            if (productToUpdate != null)
            {

                Console.WriteLine($"Current category: {productToUpdate.ProductCategory?.Name}");
                Console.Write("Enter new category (or press Enter to keep current): ");
                var newCategory = Console.ReadLine();
                if (!string.IsNullOrEmpty(newCategory))
                    productToUpdate.ProductCategory = new Category { Name = newCategory };

                Console.WriteLine($"Current product name: {productToUpdate.ProductName}");
                Console.Write("Enter new product name (or press Enter to keep current): ");
                var newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName))
                    productToUpdate.ProductName = newName;

                Console.WriteLine($"Current product description: {productToUpdate.ProductDescription}");
                Console.Write("Enter new product description (or press Enter to keep current): ");
                var newDescription = Console.ReadLine();
                if (!string.IsNullOrEmpty(newDescription))
                    productToUpdate.ProductDescription = newDescription;


                Console.WriteLine($"Current price: {productToUpdate.Price}");
                Console.Write("Enter new price (or press Enter to keep current): ");
                var newPriceInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(newPriceInput) && decimal.TryParse(newPriceInput, out decimal newPrice))
                    productToUpdate.Price = newPrice;

                var updateResponse = _productService.UpdateProduct(productId, productToUpdate);

                Console.WriteLine(updateResponse.Message);
            }
            else
                Console.WriteLine("Product not found.");
        }
        else
            Console.WriteLine(response.Message);


        Console.WriteLine("Press any key to continue...");
    }

    public void DeleteProduct()
    {
        Console.Clear();
        Console.WriteLine("== DELETE A PRODUCT ==");
        Console.Write("Which product do you want to delete?");
        Console.Write("Enter product ID: ");
        var productId = Console.ReadLine(); 


        var response = _productService.GetAllProducts();

        if (response.Success && response.Result != null)
        {
            var productList = response.Result;
            var productToDelete = productList.FirstOrDefault(x => x.ProductId == productId);    

            if (productToDelete != null)
            {
               
            }
        }
    }
}

