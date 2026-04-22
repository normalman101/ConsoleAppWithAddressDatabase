namespace ConsoleAppWithAddressDatabase.Entities;

public record Person()
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public int? Type { get; set; }
}