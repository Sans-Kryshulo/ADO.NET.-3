using System;
using System.Data.SqlClient;

public class DatabaseManager
{
    private SqlConnection connection;
    private readonly string connectionString;

    public DatabaseManager(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void Connect()
    {
        if (connection == null)
        {
            connection = new SqlConnection(connectionString);
        }
        if (connection.State == System.Data.ConnectionState.Closed)
        {
            try
            {
                connection.Open();
                Console.WriteLine("Successfully connected to the database!");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Already connected.");
        }
    }

    public void Disconnect()
    {
        if (connection != null && connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
            Console.WriteLine("Disconnected from database.");
        }
    }

    public void ExecuteAndPrintQuery(string query)
    {
        using (var cmd = new SqlCommand(query, connection))
        using (var reader = cmd.ExecuteReader())
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i] + " ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
        }
    }

    public void ExecuteUserQuery()
    {
        Console.Write("Enter your SQL query: ");
        string query = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(query))
        {
            try
            {
                ExecuteAndPrintQuery(query);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL execution error: " + ex.Message);
            }
        }
    }

    public void InsertProduct(string name, int typeId, int supplierId, int quantity, decimal price)
    {
        string query = "INSERT INTO Products (Name, TypeID, SupplierID, Quantity, CostPrice) VALUES (@name, @typeId, @supplierId, @quantity, @price)";
        using (var cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@typeId", typeId);
            cmd.Parameters.AddWithValue("@supplierId", supplierId);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Product inserted successfully.");
        }
    }

    public void InsertProductType(string typeName)
    {
        string query = "INSERT INTO ProductTypes (TypeName) VALUES (@typeName)";
        using (var cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@typeName", typeName);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Product type inserted successfully.");
        }
    }

    public void InsertSalesManager(string name, string email)
    {
        string query = "INSERT INTO SalesManagers (Name, Email) VALUES (@name, @email)";
        using (var cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Sales manager inserted successfully.");
        }
    }

    public void InsertCustomerCompany(string companyName)
    {
        string query = "INSERT INTO CustomerCompanies (CompanyName) VALUES (@companyName)";
        using (var cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@companyName", companyName);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Customer company inserted successfully.");
        }
    }

    public void UpdateProduct(int productId, string name, int typeId, int supplierId, int quantity, decimal price)
    {
        string query = "UPDATE Products SET Name=@name, TypeID=@typeId, SupplierID=@supplierId, Quantity=@quantity, CostPrice=@price WHERE ProductID=@productId";
        using (var cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@productId", productId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@typeId", typeId);
            cmd.Parameters.AddWithValue("@supplierId", supplierId);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Product updated successfully.");
        }
    }

    public void UpdateCustomerCompany(int companyId, string companyName)
    {
        string query = "UPDATE CustomerCompanies SET CompanyName=@companyName WHERE CompanyID=@companyId";
        using (var cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@companyId", companyId);
            cmd.Parameters.AddWithValue("@companyName", companyName);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Customer company updated successfully.");
        }
    }

    public void UpdateSalesManager(int managerId, string name, string email)
    {
        string query = "UPDATE SalesManagers SET Name=@name, Email=@email WHERE ManagerID=@managerId";
        using (var cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@managerId", managerId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Sales manager updated successfully.");
        }
    }

    public void UpdateProductType(int typeId, string typeName)
    {
        string query = "UPDATE ProductTypes SET TypeName=@typeName WHERE TypeID=@typeId";
        using (var cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@typeId", typeId);
            cmd.Parameters.AddWithValue("@typeName", typeName);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Product type updated successfully.");
        }
    }

    public void DeleteProduct(int productId)
    {
        string query = "INSERT INTO ArchivedProducts SELECT * FROM Products WHERE ProductID=@productId; DELETE FROM Products WHERE ProductID=@productId";
        using (var cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@productId", productId);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Product archived and deleted successfully.");
        }
    }

    public void DeleteProductType(int typeId)
    {
        string query = "INSERT INTO ArchivedProductTypes SELECT * FROM ProductTypes WHERE TypeID=@typeId; DELETE FROM ProductTypes WHERE TypeID=@typeId";
        using (var cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@typeId", typeId);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Product type archived and deleted successfully.");
        }
    }

    public void DeleteSalesManager(int managerId)
    {
        string query = "INSERT INTO ArchivedSalesManagers SELECT * FROM SalesManagers WHERE ManagerID=@managerId; DELETE FROM SalesManagers WHERE ManagerID=@managerId";
        using (var cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@managerId", managerId);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Sales manager archived and deleted successfully.");
        }
    }

    public void DeleteCustomerCompany(int companyId)
    {
        string query = "INSERT INTO ArchivedCustomerCompanies SELECT * FROM CustomerCompanies WHERE CompanyID=@companyId; DELETE FROM CustomerCompanies WHERE CompanyID=@companyId";
        using (var cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@companyId", companyId);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Customer company archived and deleted successfully.");
        }
    }

    public void ShowTopSalesManagerByUnits()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 ManagerID, Name, SUM(Quantity) AS TotalUnitsSold FROM Sales JOIN SalesManagers ON Sales.ManagerID = SalesManagers.ManagerID GROUP BY ManagerID, Name ORDER BY TotalUnitsSold DESC");
    }

    public void ShowTopSalesManagerByProfit()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 ManagerID, Name, SUM(Quantity * CostPrice) AS TotalProfit FROM Sales JOIN SalesManagers ON Sales.ManagerID = SalesManagers.ManagerID JOIN Products ON Sales.ProductID = Products.ProductID GROUP BY ManagerID, Name ORDER BY TotalProfit DESC");
    }

    public void ShowTopSalesManagerByProfitInPeriod(DateTime startDate, DateTime endDate)
    {
        string query = "SELECT TOP 1 ManagerID, Name, SUM(Quantity * CostPrice) AS TotalProfit " +
                       "FROM Sales JOIN SalesManagers ON Sales.ManagerID = SalesManagers.ManagerID " +
                       "JOIN Products ON Sales.ProductID = Products.ProductID " +
                       "WHERE SaleDate BETWEEN @startDate AND @endDate " +
                       "GROUP BY ManagerID, Name ORDER BY TotalProfit DESC";

        using (var cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader[i] + " ");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No data found.");
                }
            }
        }
    }

    public void ShowTopCustomerBySpending()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 CompanyID, CompanyName, SUM(Quantity * CostPrice) AS TotalSpent FROM Sales JOIN CustomerCompanies ON Sales.CompanyID = CustomerCompanies.CompanyID JOIN Products ON Sales.ProductID = Products.ProductID GROUP BY CompanyID, CompanyName ORDER BY TotalSpent DESC");
    }

    public void ShowTopSellingProductType()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 TypeID, TypeName, SUM(Quantity) AS TotalUnitsSold FROM Sales JOIN Products ON Sales.ProductID = Products.ProductID JOIN ProductTypes ON Products.TypeID = ProductTypes.TypeID GROUP BY TypeID, TypeName ORDER BY TotalUnitsSold DESC");
    }

    public void ShowMostProfitableProductType()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 TypeID, TypeName, SUM(Quantity * CostPrice) AS TotalProfit FROM Sales JOIN Products ON Sales.ProductID = Products.ProductID JOIN ProductTypes ON Products.TypeID = ProductTypes.TypeID GROUP BY TypeID, TypeName ORDER BY TotalProfit DESC");
    }

    public void ShowMostPopularProducts()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 ProductID, Name, SUM(Quantity) AS TotalUnitsSold FROM Sales JOIN Products ON Sales.ProductID = Products.ProductID GROUP BY ProductID, Name ORDER BY TotalUnitsSold DESC");
    }

    public void ShowUnsoldProducts(int days)
    {
        string query = "SELECT ProductID, Name FROM Products " +
                       "WHERE ProductID NOT IN (SELECT DISTINCT ProductID FROM Sales " +
                       "WHERE SaleDate >= DATEADD(DAY, -@days, GETDATE()))";

        using (var cmd = new SqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@days", days);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader[i] + " ");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No unsold products found.");
                }
            }
        }
    }
}
