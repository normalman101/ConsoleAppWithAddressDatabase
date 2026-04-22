using System;
using System.Collections.Generic;
using System.Data;
using ConsoleAppWithAddressDatabase.Builders;
using ConsoleAppWithAddressDatabase.Entities;
using ConsoleAppWithAddressDatabase.Extensions;
using Microsoft.Data.Sqlite;

namespace ConsoleAppWithAddressDatabase.Repositories;

public class AddressRepository : IRepository<Address>, IDatabaseConnectable
{
    public required SqliteConnection Connection { get; init; }

    public void Add(Address data)
    {
        if (HasEmptyValues(data)) throw new Exception("Адресс имеет незаполненные поля");

        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             INSERT INTO table_addresses(Region,
                                         Locality,
                                         PlanningElement,
                                         Street,
                                         Building,
                                         Room,
                                         PersonId)
             VALUES ('{data.Region}',
                     '{data.Locality}',
                     '{data.PlanningElement}',
                     '{data.Street}',
                     '{data.Building}',
                     '{data.Room}',
                     '{data.PersonId}')
             """;

        Connection.Open();

        command.ExecuteNonQuery();

        Connection.Close();
    }

    public List<Address> GetByCriteria<TV>(TV value, string columnName)
    {
        var addresses = new List<Address>();
        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             SELECT *
             FROM table_addresses
             WHERE '{value}' == (CAST ({columnName} AS TEXT))
             """;

        Connection.Open();

        var data = command.ExecuteReader();

        if (!data.HasRows) return addresses;

        while (data.Read())
        {
            var addressBuilder = new AddressBuilder();

            addressBuilder
                .SetId(data.GetInt32("Id"))
                .SetRegion(data.GetString("Region"))
                .SetLocality(data.GetString("Locality"))
                .SetPlanningElement(data.GetString("PlanningElement"))
                .SetStreet(data.GetString("Street"))
                .SetBuilding(data.GetString("Building"))
                .SetRoom(data.GetString("Room"))
                .SetPersonId(data.GetInt32("PersonId"));

            addresses.Add(addressBuilder.Address);
            addressBuilder.Reset();
        }

        Connection.Close();

        return addresses;
    }

    public List<Address> GetAll()
    {
        var addresses = new List<Address>();
        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             SELECT *
             FROM table_addresses
             """;

        Connection.Open();

        var data = command.ExecuteReader();

        if (!data.HasRows) return addresses;

        while (data.Read())
        {
            var addressBuilder = new AddressBuilder();

            addressBuilder
                .SetId(data.GetInt32("Id"))
                .SetRegion(data.GetString("Region"))
                .SetLocality(data.GetString("Locality"))
                .SetPlanningElement(data.GetString("PlanningElement"))
                .SetStreet(data.GetString("Street"))
                .SetBuilding(data.GetString("Building"))
                .SetRoom(data.GetString("Room"))
                .SetPersonId(data.GetInt32("PersonId"));

            addresses.Add(addressBuilder.Address);
            addressBuilder.Reset();
        }

        Connection.Close();

        return addresses;
    }

    public void Update(int id, Address newData)
    {
        var existingAddress = GetByCriteria(id, "Id")[0];

        var checkedRegion = newData.Region.IsEmptyOrNull() ? existingAddress.Region : newData.Region;
        var checkedLocality = newData.Locality.IsEmptyOrNull() ? existingAddress.Locality : newData.Locality;
        var checkedPlanningElement =
            newData.PlanningElement.IsEmptyOrNull() ? existingAddress.PlanningElement : newData.PlanningElement;
        var checkedStreet = newData.Street.IsEmptyOrNull() ? existingAddress.Street : newData.Street;
        var checkedBuilding = newData.Building.IsEmptyOrNull() ? existingAddress.Building : newData.Building;
        var checkedRoom = newData.Room.IsEmptyOrNull() ? existingAddress.Room : newData.Room;
        var checkedPersonId =
            newData.PersonId == existingAddress.PersonId ? existingAddress.PersonId : newData.PersonId;

        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             UPDATE table_addresses
             SET Region = '{checkedRegion}',
                 Locality = '{checkedLocality}',
                 PlanningElement = '{checkedPlanningElement}',
                 Street = '{checkedStreet}',
                 Building = '{checkedBuilding}',
                 Room = '{checkedRoom}',
                 PersonId = {checkedPersonId}
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

    private static bool HasEmptyValues(Address? data)
    {
        return !(data.Region.IsEmptyOrNull()
                 && data.Locality.IsEmptyOrNull()
                 && data.PlanningElement.IsEmptyOrNull()
                 && data.Street.IsEmptyOrNull()
                 && data.Building.IsEmptyOrNull()
                 && data.Room.IsEmptyOrNull());
    }
}