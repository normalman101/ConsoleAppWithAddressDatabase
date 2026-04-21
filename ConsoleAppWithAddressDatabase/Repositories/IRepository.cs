using System.Collections.Generic;

namespace ConsoleAppWithAddressDatabase.Repositories;

public interface IRepository<T>
{
    void Add(T data);
    List<T> GetByCriteria<TV>(TV value, string columnName);
    List<T> GetAll();
    void Update(int id, T newData);
    void Delete(int id);
    void Undelete(int id);
}