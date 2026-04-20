using System;
using ConsoleAppWithAddressDatabase.Entities;

namespace ConsoleAppWithAddressDatabase;

public static partial class Program
{
    public static void Main()
    {
        const string connectionString =
            @"Data Source=C:\Projects\Rider\ConsoleAppWithAddressDatabase\ConsoleAppWithAddressDatabase\Database\addresses.sqlite";
        var database = new AddressDatabase(connectionString);
        
        database.Add("Voronezh", "A", "B", "Ulitsa62g", "Building", "24", TODO);
        var addresses = database.GetAll();
        foreach (var address in addresses)
        {
            Console.WriteLine($"{address.Region} {address.Locality}, {address.PlanningElement}, {address.Street}, {address.Building}, {address.Room}");
        }
    }
}