namespace ConsoleAppWithAddressDatabase.Entities;

public static class StringExtension
{
    public static bool IsEmpty(this string value) => string.IsNullOrWhiteSpace(value);
}