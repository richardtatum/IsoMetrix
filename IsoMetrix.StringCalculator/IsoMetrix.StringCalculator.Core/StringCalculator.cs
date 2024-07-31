using IsoMetrix.StringCalculator.Handlers.Delimiters;
using IsoMetrix.StringCalculator.Handlers.Validation;

namespace IsoMetrix.StringCalculator;

public class StringCalculator(IDelimiterHandler delimiterHandler, IEnumerable<IValidationHandler> validationHandlers)
{
    /// <summary>
    /// Adds the numbers in the given string, using delimiters as defined in the delimiter handler
    /// and the delimiter string prefix.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if negative numbers are encountered.</exception>
    /// <param name="numbers">The string containing numbers and delimiters.</param>
    /// <returns>The sum of the numbers in the string, or 0 if the string is empty or contains only delimiters.</returns>
    public int Add(string numbers)
    {
        var delimiters = delimiterHandler.ExtractDelimiters(numbers, out var extractedNumbers);
        if (string.IsNullOrWhiteSpace(extractedNumbers))
        {
            return 0;
        }

        var numberArray = extractedNumbers
            .Split(delimiters, StringSplitOptions.None)
            .Select(int.Parse)
            .ToArray();

        foreach (var validator in validationHandlers)
        {
            numberArray = validator.Validate(numberArray);
        }
        
        return numberArray.Sum();
    }
}