namespace ConsoleAppWithAddressDatabase.Entities;

public record Address()
{
    public int? Id { get; set; }
    public string? Region { get; set; }
    public string? Locality { get; set; }
    public string? PlanningElement { get; set; }
    public string? Street { get; set; }
    public string? Building { get; set; }
    public string? Room { get; set; }
    public int? IndividualId { get; set; }
}