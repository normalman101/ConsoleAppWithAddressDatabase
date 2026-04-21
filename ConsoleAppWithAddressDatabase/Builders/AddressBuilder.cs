using ConsoleAppWithAddressDatabase.Entities;

namespace ConsoleAppWithAddressDatabase.Builders;

public class AddressBuilder : IBuilder<AddressBuilder>
{
    public Address Address { get; private set; } = new Address();

    public AddressBuilder Reset()
    {
        Address = new Address();
        return this;
    }
    public AddressBuilder SetId(int id)
    {
        Address.Id = id;
        return this;
    }

    public AddressBuilder SetRegion(string region)
    {
        Address.Region = region;
        return this;
    }

    public AddressBuilder SetLocality(string locality)
    {
        Address.Locality = locality;
        return this;
    }

    public AddressBuilder SetPlanningElement(string planningElement)
    {
        Address.PlanningElement = planningElement;
        return this;
    }

    public AddressBuilder SetStreet(string street)
    {
        Address.Street = street;
        return this;
    }

    public AddressBuilder SetBuilding(string building)
    {
        Address.Building = building;
        return this;
    }

    public AddressBuilder SetRoom(string room)
    {
        Address.Room = room;
        return this;
    }

    public AddressBuilder SetIndividualId(int individualId)
    {
        Address.IndividualId = individualId;
        return this;
    }
}