namespace IsoMetrix.StringCalculator;

public static class StringCalculator
{
    private static readonly string CustomDelimiterStartMarker = "//";
    private static readonly string CustomDelimiterEndMarker = "\n";
    private static readonly string[] BaseDelimiters = [",", "\n"];

    // TODO: Annotate the functions, what they return (including exceptions)
    public static int Add(string numbers)
    {
        // Ensure we always start with the base delimiters
        var delimiters = BaseDelimiters;

        // TODO: Move this to a delimiter handler
        var (extractedDelimiters, extractedNumbers) = ExtractCustomDelimiter(numbers);
        if (extractedDelimiters.Any())
        {
            numbers = extractedNumbers;
            delimiters = delimiters
                .Union(extractedDelimiters)
                .ToArray();
        }

        if (string.IsNullOrWhiteSpace(numbers))
        {
            return 0;
        }

        var numberArray = numbers
            .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        // TODO: Move this to a ValidationHandler? Should handle any provided validation
        var (valid, exception) = Validate(numberArray);
        if (!valid)
        {
            throw exception!;
        }

        return numberArray
            .RemoveInvalidNumbers()
            .Sum();
    }

    // TODO: Move to DelimiterHandler, attempt to instantiate the static class with a base one, or accept one for custom start and end markers
    private static (string[] delimiter, string numbers) ExtractCustomDelimiter(string numbers)
    {
        if (!numbers.StartsWith(CustomDelimiterStartMarker))
        {
            return ([], numbers);
        }

        var customDelimiterStart = CustomDelimiterStartMarker.Length;
        var customDelimiterEnd = numbers.IndexOf(CustomDelimiterEndMarker, StringComparison.Ordinal);
        var customDelimiterLength = customDelimiterEnd - CustomDelimiterStartMarker.Length;

        var customDelimiter = numbers.Substring(customDelimiterStart, customDelimiterLength);
        var extractedNumbers = numbers.Substring(customDelimiterEnd + 1);

        var customDelimiters = customDelimiter
            .Replace("[", " ")
            .Replace("]", " ")
            .Split(" ", StringSplitOptions.RemoveEmptyEntries);

        return (customDelimiters, extractedNumbers);
    }

    // TODO: A `-` delimiter would cause a lot of problems here!
    private static (bool success, Exception? exception) Validate(IEnumerable<int> numbers)
    {
        var negativeNumbers = numbers.Where(number => number < 0).ToArray();
        if (negativeNumbers.Length <= 0)
        {
            return (true, null);
        }

        return (
            false,
            new InvalidOperationException($"StringCalculator does support the addition of negative numbers. Invalid numbers: {string.Join(',', negativeNumbers)}")
            );
    }

    private static int[] RemoveInvalidNumbers(this IEnumerable<int> numbers)
    {
        return numbers.Where(number => number <= 1000).ToArray();
    }
}