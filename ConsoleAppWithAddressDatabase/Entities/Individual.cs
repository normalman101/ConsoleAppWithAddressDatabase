namespace ConsoleAppWithAddressDatabase.Entities;

public record Individual()
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public int? TypeId { get; set; }
}