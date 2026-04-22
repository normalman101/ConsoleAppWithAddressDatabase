using System;
using System.Collections.Generic;
using System.Data;
using ConsoleAppWithAddressDatabase.Builders;
using ConsoleAppWithAddressDatabase.Entities;
using ConsoleAppWithAddressDatabase.Extensions;
using Microsoft.Data.Sqlite;

namespace ConsoleAppWithAddressDatabase.Repositories;

public class PersonRepository : IRepository<Person>, IDatabaseConnectable
{
    public required SqliteConnection Connection { get; init; }

    public void Add(Person data)
    {
        if (data.Name.IsEmptyOrNull()) throw new Exception("Лицо имеет незаполненное имя");

        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             INSERT INTO table_persons(Name, Type)
             VALUES ('{data.Name}', {data.Type})
             """;

        Connection.Open();

        command.ExecuteNonQuery();

        Connection.Close();
    }

    public List<Person> GetByCriteria<TV>(TV value, string columnName)
    {
        var individuals = new List<Person>();
        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             SELECT *
             FROM table_persons
             WHERE '{value}' == (CAST ({columnName} AS TEXT))
             """;

        Connection.Open();

        var data = command.ExecuteReader();

        if (!data.HasRows) return individuals;

        while (data.Read())
        {
            var individualBuilder = new PersonBuilder();

            individualBuilder.SetId(data.GetInt32("Id"))
                .SetName(data.GetString("Name"))
                .SetType(data.GetInt32("Type"));
            
            individuals.Add(individualBuilder.Person);
            individualBuilder.Reset();
        }

        Connection.Close();

        return individuals;
    }

    public List<Person> GetAll()
    {
        var individuals = new List<Person>();
        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             SELECT *
             FROM table_persons
             """;

        Connection.Open();

        var data = command.ExecuteReader();

        if (!data.HasRows) return individuals;

        while (data.Read())
        {
            var individualBuilder = new PersonBuilder();

            individualBuilder.SetId(data.GetInt32("Id"))
                .SetName(data.GetString("Name"))
                .SetType(data.GetInt32("Type"));
            
            individuals.Add(individualBuilder.Person);
            individualBuilder.Reset();
        }

        Connection.Close();

        return individuals;
    }

    public void Update(int id, Person newData)
    {
        var existingIndividual = GetByCriteria(id, "Id");

        var checkedName = newData.Name.IsEmptyOrNull() ? existingIndividual[0].Name : newData.Name;
        var checkedType = newData.Type is 0 or 1 ? newData.Type : existingIndividual[0].Type;

        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             UPDATE table_persons
             SET Name = '{checkedName}',
                 Type = {checkedType}
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
             UPDATE table_persons
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
             UPDATE table_persons
             SET IsDeleted = 0
             WHERE ({id} == Id)
             """;

        Connection.Open();

        command.ExecuteNonQuery();

        Connection.Close();
    }
}