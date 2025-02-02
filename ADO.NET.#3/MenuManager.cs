using System;
using System.Data.SqlClient;

public class MenuManager
{
    private readonly DatabaseManager dbManager;

    public MenuManager(DatabaseManager databaseManager)
    {
        dbManager = databaseManager;
    }

    public void ShowMenu()
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Connect to the database");
            Console.WriteLine("2. Disconnect from the database");
            Console.WriteLine("3. Enter your own SQL query");
            Console.WriteLine("4. Insert new product");
            Console.WriteLine("5. Insert new product type");
            Console.WriteLine("6. Insert new sales manager");
            Console.WriteLine("7. Insert new customer company");
            Console.WriteLine("8. Update existing product");
            Console.WriteLine("9. Update existing product type");
            Console.WriteLine("10. Update existing sales manager");
            Console.WriteLine("11. Update existing customer company");
            Console.WriteLine("12. Delete product");
            Console.WriteLine("13. Delete product type");
            Console.WriteLine("14. Delete sales manager");
            Console.WriteLine("15. Delete customer company");
            Console.WriteLine("16. Show top sales manager by units sold");
            Console.WriteLine("17. Show top sales manager by profit");
            Console.WriteLine("18. Show top sales manager by profit in given period");
            Console.WriteLine("19. Show top customer by spending");
            Console.WriteLine("20. Show top selling product type");
            Console.WriteLine("21. Show most profitable product type");
            Console.WriteLine("22. Show most popular products");
            Console.WriteLine("23. Show unsold products for specified days");
            Console.WriteLine("24. Exit");
            Console.Write("Your choice: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    dbManager.Connect();
                    break;
                case "2":
                    dbManager.Disconnect();
                    break;
                case "3":
                    dbManager.ExecuteUserQuery();
                    break;
                case "4":
                    Console.Write("Enter product name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter type ID: ");
                    int typeId = int.Parse(Console.ReadLine());
                    Console.Write("Enter supplier ID: ");
                    int supplierId = int.Parse(Console.ReadLine());
                    Console.Write("Enter quantity: ");
                    int quantity = int.Parse(Console.ReadLine());
                    Console.Write("Enter price: ");
                    decimal price = decimal.Parse(Console.ReadLine());
                    dbManager.InsertProduct(name, typeId, supplierId, quantity, price);
                    break;
                case "5":
                    Console.Write("Enter product type name: ");
                    string typeName = Console.ReadLine();
                    dbManager.InsertProductType(typeName);
                    break;
                case "6":
                    Console.Write("Enter sales manager name: ");
                    string managerName = Console.ReadLine();
                    Console.Write("Enter email: ");
                    string email = Console.ReadLine();
                    dbManager.InsertSalesManager(managerName, email);
                    break;
                case "7":
                    Console.Write("Enter customer company name: ");
                    string companyName = Console.ReadLine();
                    dbManager.InsertCustomerCompany(companyName);
                    break;
                case "12":
                    Console.Write("Enter product ID to delete: ");
                    int productId = int.Parse(Console.ReadLine());
                    dbManager.DeleteProduct(productId);
                    break;
                case "13":
                    Console.Write("Enter product type ID to delete: ");
                    typeId = int.Parse(Console.ReadLine());
                    dbManager.DeleteProductType(typeId);
                    break;
                case "14":
                    Console.Write("Enter sales manager ID to delete: ");
                    int managerId = int.Parse(Console.ReadLine());
                    dbManager.DeleteSalesManager(managerId);
                    break;
                case "15":
                    Console.Write("Enter customer company ID to delete: ");
                    int companyId = int.Parse(Console.ReadLine());
                    dbManager.DeleteCustomerCompany(companyId);
                    break;
                case "16":
                    dbManager.ShowTopSalesManagerByUnits();
                    break;
                case "17":
                    dbManager.ShowTopSalesManagerByProfit();
                    break;
                case "18":
                    Console.Write("Enter start date (yyyy-MM-dd): ");
                    DateTime startDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Enter end date (yyyy-MM-dd): ");
                    DateTime endDate = DateTime.Parse(Console.ReadLine());
                    dbManager.ShowTopSalesManagerByProfitInPeriod(startDate, endDate);
                    break;
                case "19":
                    dbManager.ShowTopCustomerBySpending();
                    break;
                case "20":
                    dbManager.ShowTopSellingProductType();
                    break;
                case "21":
                    dbManager.ShowMostProfitableProductType();
                    break;
                case "22":
                    dbManager.ShowMostPopularProducts();
                    break;
                case "23":
                    Console.Write("Enter number of days: ");
                    int days = int.Parse(Console.ReadLine());
                    dbManager.ShowUnsoldProducts(days);
                    break;
                case "24":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
