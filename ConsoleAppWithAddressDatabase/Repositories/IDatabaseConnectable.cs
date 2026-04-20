using Microsoft.Data.Sqlite;

namespace ConsoleAppWithAddressDatabase.Repositories;

public interface IDatabaseConnectable
{
    public SqliteConnection Connection { get; init; }
}