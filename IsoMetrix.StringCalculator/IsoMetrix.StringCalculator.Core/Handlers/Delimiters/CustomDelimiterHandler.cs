using System.Text.RegularExpressions;

namespace IsoMetrix.StringCalculator.Handlers.Delimiters;

public class CustomDelimiterHandler : BaseDelimiterHandler
{
    private const string CustomDelimiterStartMarker = "//";
    private const string CustomDelimiterEndMarker = "\n";
    private const string MultiDelimiterPattern = @"(?<=\[)[^\[\]]+(?=\])|^[^\[\]]+$";
    
    public override string[] ExtractDelimiters(string input, out string numbers)
    {
        var delimiters = base.ExtractDelimiters(input, out numbers);
        if (!input.StartsWith(CustomDelimiterStartMarker))
        {
            return delimiters;
        }
        
        var customDelimiterStart = CustomDelimiterStartMarker.Length;
        var customDelimiterEnd = numbers.IndexOf(CustomDelimiterEndMarker, StringComparison.Ordinal);
        var customDelimiterLength = customDelimiterEnd - CustomDelimiterStartMarker.Length;
        var customDelimiter = numbers.Substring(customDelimiterStart, customDelimiterLength);
        
        // Split the numbers from the delimiters
        numbers = numbers.Substring(customDelimiterEnd + 1);
        
        // Multiple provided delimiters are separated by square brackets. I.e. [delim1][delim2], this regex pattern extracts them
        var customDelimiters = Regex.Matches(customDelimiter, MultiDelimiterPattern)
            .Select(x => x.Value)
            .ToArray();

        return delimiters.Union(customDelimiters).ToArray();
    }
}