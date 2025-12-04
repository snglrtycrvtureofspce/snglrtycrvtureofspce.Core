using snglrtycrvtureofspce.Core.Enums;

namespace snglrtycrvtureofspce.Core.Extensions;

/// <summary>
/// Provides extension methods for working with enum types.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the currency symbol for a given currency code.
    /// </summary>
    /// <param name="currencyCode">The currency code to get the symbol for.</param>
    /// <returns>The currency symbol (e.g., "$" for USD, "€" for EUR).</returns>
    /// <example>
    /// <code>
    /// var symbol = Currency.Usd.GetCurrencySymbol(); // Returns "$"
    /// </code>
    /// </example>
    public static string GetCurrencySymbol(this Currency currencyCode)
        => currencyCode switch
        {
            Currency.Usd => "$",
            Currency.Eur => "€",
            Currency.Rub => "₽",
            Currency.Byn => "Br.",
            _ => Enum.GetName(currencyCode) ?? currencyCode.ToString()
        };
}
