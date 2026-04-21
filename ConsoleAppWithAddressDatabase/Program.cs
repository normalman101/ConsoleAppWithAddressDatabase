using System;
using ConsoleAppWithAddressDatabase.Entities;
using ConsoleAppWithAddressDatabase.Repositories;
using Microsoft.Data.Sqlite;

namespace ConsoleAppWithAddressDatabase;

public static partial class Program
{
    public static void Main()
    {
        const string connectionString =
            @"Data Source=C:\Projects\Rider\ConsoleAppWithAddressDatabase\ConsoleAppWithAddressDatabase\Database\addresses.sqlite";
        var addressRepository = new AddressRepository
        {
            Connection = new SqliteConnection(connectionString)
        };
        
        var addresses = addressRepository.GetAll();
        
        foreach (var element in addresses)
        {
            Console.WriteLine($"{element.Id} {element.Region} {element.Locality} {element.PlanningElement} {element.Street} {element.Building} {element.Room} {element.IndividualId}");
        }
    }
}