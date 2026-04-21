using ConsoleAppWithAddressDatabase.Entities;

namespace ConsoleAppWithAddressDatabase.Builders;

public class IndividualBuilder : IBuilder<IndividualBuilder>
{
    public Individual Individual { get; private set; } = new Individual();
    
    public IndividualBuilder Reset()
    {
        Individual = new Individual();
        return this;
    }

    public IndividualBuilder SetId(int id)
    {
        Individual.Id = id;
        return this;
    }

    public IndividualBuilder SetName(string name)
    {
        Individual.Name = name;
        return this;
    }

    public IndividualBuilder SetTypeId(int typeId)
    {
        Individual.TypeId = typeId;
        return this;
    }
}