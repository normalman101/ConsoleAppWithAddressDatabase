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
        var addressDatabase = new AddressRepository
        {
            Connection = new SqliteConnection(connectionString)
        };

        var individualDatabase = new IndividualRepository
        {
            Connection = new SqliteConnection(connectionString)
        };

        var individual = new Individual(1, "Alice", 1);

        individualDatabase.Add(individual);

        var address = new Address(null, "TestA", "TestB", "TestC", "TestD", "TestE", "TestF", 1);
        addressDatabase.Add(address);

        var dbIndividual = individualDatabase.GetById(1);
        
        var dbAddress = addressDatabase.GetById(1);
        Console.WriteLine($"{dbIndividual.Id} {dbIndividual.Name} {dbIndividual.TypeId}");
    }
}