using Microsoft.Data.Sqlite;

namespace ConsoleAppWithAddressDatabase.Entities;

public abstract class DatabaseBase()
{
    public SqliteConnection Connection { get; init; }
}