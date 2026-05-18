using Microsoft.AspNetCore.Components.Forms;
using System.Globalization;

namespace AirHorn.App.Components
{
    // Code-only component inheriting the real InputNumber renderer.
    // Displays without decimals; leaves rounding to the parent wrapper.
    public class CurrencyInputNumber : InputNumber<decimal?>
    {
        /// Assumes decimal is already rounded
        /// Formatting is all that's required
        protected override string FormatValueAsString(decimal? value)
        {
            if (value is null)
            {
                return string.Empty;
            }

            return value.Value.ToString("F0", CultureInfo.CurrentCulture);
        }

        // Parse on change/blur; DO NOT round here.
        // Rounding/flooring happens in SignedCurrencyInput after applying sign.
        protected override bool TryParseValueFromString(string? value, out decimal? result, out string? validationErrorMessage)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = null;                 // lets [Required] fire only when truly empty
                validationErrorMessage = null;
                return true;
            }

            if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out var parsed))
            {
                result = parsed;               // keep raw; formatting will show integer
                validationErrorMessage = null;
                return true;
            }

            result = null;
            validationErrorMessage = "Enter a valid number.";
            return false;
        }
    }
}
