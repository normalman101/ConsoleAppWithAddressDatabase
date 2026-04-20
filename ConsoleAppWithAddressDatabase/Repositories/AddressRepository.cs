using System;
using System.Collections.Generic;
using ConsoleAppWithAddressDatabase.Entities;
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
                                         IndividualId)
             VALUES ('{data.Region}',
                     '{data.Locality}',
                     '{data.PlanningElement}',
                     '{data.Street}',
                     '{data.Building}',
                     '{data.Room}',
                     '{data.IndividualId}')
             """;

        Connection.Open();

        command.ExecuteNonQuery();

        Connection.Close();
    }

    public Address GetById(int id)
    {
        var command = new SqliteCommand();
        command.Connection = Connection;
        command.CommandText =
            $"""
             SELECT *
             FROM table_addresses
             WHERE ({id} == Id)
             """;

        Connection.Open();

        var data = command.ExecuteReader();

        if (!data.HasRows) throw new Exception("Адресс не найден");

        var address = new Address(
            data.GetInt32(0),
            data.GetString(1),
            data.GetString(2),
            data.GetString(3),
            data.GetString(4),
            data.GetString(5),
            data.GetString(6),
            data.GetInt32(7));

        Connection.Close();

        return address;
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
            var address = new Address(
                data.GetInt32(0),
                data.GetString(1),
                data.GetString(2),
                data.GetString(3),
                data.GetString(4),
                data.GetString(5),
                data.GetString(6),
                data.GetInt32(7));

            addresses.Add(address);
        }

        Connection.Close();

        return addresses;
    }

    public void Update(int id, Address newData)
    {
        var existingAddress = GetById(id);
        
        var checkedRegion = newData.Region.IsEmpty() ? existingAddress.Region : newData.Region;
        var checkedLocality = newData.Locality.IsEmpty() ? existingAddress.Locality : newData.Locality;
        var checkedPlanningElement =
            newData.PlanningElement.IsEmpty() ? existingAddress.PlanningElement : newData.PlanningElement;
        var checkedStreet = newData.Street.IsEmpty() ? existingAddress.Street : newData.Street;
        var checkedBuilding = newData.Building.IsEmpty() ? existingAddress.Building : newData.Building;
        var checkedRoom = newData.Room.IsEmpty() ? existingAddress.Room : newData.Room;

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
                 Room = '{checkedRoom}'
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

    public bool HasEmptyValues(Address data)
    {
        if (data.Region.IsEmpty()
            || data.Locality.IsEmpty()
            || data.PlanningElement.IsEmpty()
            || data.Street.IsEmpty()
            || data.Building.IsEmpty()
            || data.Room.IsEmpty()) return false;
        // TODO: Реализовать проверку существующего лица по идентификатору
        return true;
    }
}