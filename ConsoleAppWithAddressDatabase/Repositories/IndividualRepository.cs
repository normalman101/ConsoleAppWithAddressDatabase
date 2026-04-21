using System;
using System.Collections.Generic;
using System.Data;
using ConsoleAppWithAddressDatabase.Builders;
using ConsoleAppWithAddressDatabase.Entities;
using ConsoleAppWithAddressDatabase.Extensions;
using Microsoft.Data.Sqlite;

namespace ConsoleAppWithAddressDatabase.Repositories;

public class IndividualRepository : IRepository<Individual>, IDatabaseConnectable
{
    public required SqliteConnection Connection { get; init; }

    public void Add(Individual data)
    {
        if (data.Name.IsEmptyOrNull()) throw new Exception("Лицо имеет незаполненное имя");

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

    public List<Individual> GetByCriteria<TV>(TV value, string columnName)
    {
        var individuals = new List<Individual>();
        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             SELECT *
             FROM table_individuals
             WHERE '{value}' == (CAST ({columnName} AS TEXT))
             """;

        Connection.Open();

        var data = command.ExecuteReader();

        if (!data.HasRows) return individuals;

        while (data.Read())
        {
            var individualBuilder = new IndividualBuilder();

            individualBuilder.SetId(data.GetInt32("Id"))
                .SetName(data.GetString("Name"))
                .SetTypeId(data.GetInt32("TypeId"));
            
            individuals.Add(individualBuilder.Individual);
            individualBuilder.Reset();
        }

        Connection.Close();

        return individuals;
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

        while (data.Read())
        {
            var individualBuilder = new IndividualBuilder();

            individualBuilder.SetId(data.GetInt32("Id"))
                .SetName(data.GetString("Name"))
                .SetTypeId(data.GetInt32("TypeId"));
            
            individuals.Add(individualBuilder.Individual);
            individualBuilder.Reset();
        }

        Connection.Close();

        return individuals;
    }

    public void Update(int id, Individual newData)
    {
        var existingIndividual = GetByCriteria(id, "Id");

        var checkedName = newData.Name.IsEmptyOrNull() ? existingIndividual[0].Name : newData.Name;
        var checkedTypeId = newData.TypeId is 1 or 2 ? newData.TypeId : existingIndividual[0].TypeId;

        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             UPDATE table_individuals
             SET Name = '{checkedName}',
                 TypeId = {checkedTypeId}
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
             UPDATE table_individuals
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
             UPDATE table_individuals
             SET IsDeleted = 0
             WHERE ({id} == Id)
             """;

        Connection.Open();

        command.ExecuteNonQuery();

        Connection.Close();
    }
}