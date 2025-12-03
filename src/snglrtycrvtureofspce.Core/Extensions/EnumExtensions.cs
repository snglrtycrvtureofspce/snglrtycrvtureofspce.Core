using System;
using snglrtycrvtureofspce.Core.Enums;

namespace snglrtycrvtureofspce.Core.Extensions;

public static class EnumExtensions
{
    public static string GetCurrencySymbol(this Currency currencyCode) =>
        currencyCode switch
        {
            Currency.Usd => "$",
            Currency.Eur => "€",
            Currency.Rub => "₽",
            Currency.Byn => "Br.",
            _ => Enum.GetName(currencyCode)
        };
}