namespace ConsoleAppWithAddressDatabase.Extensions;

public static class StringExtension
{
    public static bool IsEmptyOrNull(this string value) => string.IsNullOrWhiteSpace(value);
}