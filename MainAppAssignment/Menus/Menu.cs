using Resources.Models;
using Resources.Services;
using System;

namespace MainAppAssignment.Menus;

public class Menu
{
    private readonly ProductMenu _productMenu = new ProductMenu();

    public void MainMenu()
    {
        while (true) 
        {
            Console.Clear();
            Console.WriteLine("== WELCOME TO THE PRODUCTMENU == \n");
            Console.WriteLine("1. Create a product");
            Console.WriteLine("2. Show all products");
            Console.WriteLine("3. Update a product");
            Console.WriteLine("4. Delete a product");
            Console.WriteLine("0. Exit the menu\n");
            Console.Write("Make your choice (0-4): ");

            var option = Console.ReadLine();
            int choice;

      
            if (!int.TryParse(option, out choice) || choice < 0 || choice > 4)
            {
                Console.WriteLine("\nInvalid input. Please enter a number between 0 and 4.");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                continue;  
            }

            switch (choice)
            {
                case 1:
                    _productMenu.CreateProductMenu();
                    break;
                case 2:
                    _productMenu.ShowAllProducts();
                    break;
                case 3:
                    _productMenu.ShowOneProduct();
                    break;
                case 4:
                    _productMenu.DeleteProduct();
                    break;
                case 0:
                    Environment.Exit(0);
                    return;  
            }

           
            Console.ReadKey();
        }
    }
}
