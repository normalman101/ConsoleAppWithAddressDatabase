using System;
using System.Collections.Generic;
using System.Data;
using ConsoleAppWithAddressDatabase.Entities;
using Microsoft.Data.Sqlite;

namespace ConsoleAppWithAddressDatabase.Repositories;

public class IndividualRepository : IRepository<Individual>, IDatabaseConnectable
{
    public required SqliteConnection Connection { get; init; }

    public void Add(Individual data)
    {
        if (data.Name.IsEmpty()) throw new Exception("Лицо имеет незаполненное имя");

        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             INSERT INTO table_individuals(Name, TypeId)
             VALUES ('{data.Name}', {data.TypeId})
             """;

        Connection.Open();

        command.ExecuteNonQuery();

        Connection.Close();
    }

    public Individual? GetById(int id)
    {
        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             SELECT *
             FROM table_individuals
             WHERE ({id} == Id)
             """;

        Connection.Open();

        var data = command.ExecuteReader();

        if (!data.HasRows) throw new Exception("Лицо не найдено");

        data.Read();
        
        var individual = new Individual(
            data.GetInt32("Id"),
            data.GetString("Name"),
            data.GetInt32("TypeId"));

        Connection.Close();

        return individual;
    }

    public List<Individual> GetAll()
    {
        var individuals = new List<Individual>();
        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             SELECT *
             FROM table_individuals
             """;

        Connection.Open();

        var data = command.ExecuteReader();

        if (!data.HasRows) return individuals;

        var idIndex = data.GetOrdinal("Id");
        var nameIndex = data.GetOrdinal("Name");
        var typeIdIndex = data.GetOrdinal("TypeId");
        
        while (data.Read())
        {
            var individual = new Individual(
                data.GetInt32(idIndex),
                data.GetString(nameIndex),
                data.GetInt32(typeIdIndex));

            individuals.Add(individual);
        }

        Connection.Close();

        return individuals;
    }

    public void Update(int id, Individual newData)
    {
        var existingIndividual = GetById(id);

        var checkedName = newData.Name.IsEmpty() ? existingIndividual.Name : newData.Name;
        var checkedTypeId = newData.TypeId is 1 or 2 ? existingIndividual.TypeId : newData.TypeId;

        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             UPDATE table_individuals
             SET Name = '{checkedName}',
                 TypeId = '{checkedTypeId}'
             WHERE ({id} == Id)
             """;

        Connection.Open();

        command.ExecuteNonQuery();

        Connection.Close();
    }

    public void Delete(int id)
    {
        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             UPDATE table_addresses
             SET IsDeleted = 1
             WHERE ({id} == Id)
             """;

        Connection.Open();

        command.ExecuteNonQuery();

        Connection.Close();
    }

    public void Undelete(int id)
    {
        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             UPDATE table_addresses
             SET IsDeleted = 0
             WHERE ({id} == Id)
             """;

        Connection.Open();

        command.ExecuteNonQuery();

        Connection.Close();
    }
}