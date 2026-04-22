using ConsoleAppWithAddressDatabase.Entities;

namespace ConsoleAppWithAddressDatabase.Builders;

public class PersonBuilder : IBuilder<PersonBuilder>
{
    public Person Person { get; private set; } = new Person();
    
    public PersonBuilder Reset()
    {
        Person = new Person();
        return this;
    }

    public PersonBuilder SetId(int id)
    {
        Person.Id = id;
        return this;
    }

    public PersonBuilder SetName(string name)
    {
        Person.Name = name;
        return this;
    }

    public PersonBuilder SetType(int type)
    {
        Person.Type = type;
        return this;
    }
}