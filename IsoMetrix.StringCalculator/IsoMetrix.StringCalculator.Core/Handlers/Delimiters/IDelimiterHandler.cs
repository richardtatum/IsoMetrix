namespace IsoMetrix.StringCalculator.Handlers.Delimiters;

public interface IDelimiterHandler
{
    string[] ExtractDelimiters(string input, out string numbers);
}