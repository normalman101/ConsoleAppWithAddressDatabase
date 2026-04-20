namespace ConsoleAppWithAddressDatabase.Entities;

public record Address(
    int Id,
    string Region,
    string Locality,
    string PlanningElement,
    string Street,
    string Building,
    string Room,
    int IndividualId);