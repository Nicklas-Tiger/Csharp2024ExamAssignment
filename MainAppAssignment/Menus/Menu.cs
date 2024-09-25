namespace MainAppAssignment.Menus;

public class Menu
{
    private readonly ProductMenu _customerMenu = new ProductMenu();

    public void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("WELCOME TO THE PRODUCTMENU\n");
        Console.WriteLine("1. Create a product");
        Console.WriteLine("2. Show all products");
        Console.WriteLine("3. Update a product");
        Console.WriteLine("4. Delete a product");
        Console.WriteLine("0. Exit the productmenu\n");
        Console.Write("Make your choice (0-4): ");

        var option = Console.ReadLine();
        int choice;
        if (int.TryParse(option, out choice))
            Console.WriteLine("Invalid input. Please enter a number between 0 and 4.");

        switch (choice)
        {
            case 1:
                _customerMenu.CreateMenu();
                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 0:
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid number. Please enter a number between 0 and 4.");
                Console.ReadKey();
                break;
        }

    }
}
