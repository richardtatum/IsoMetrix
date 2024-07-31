namespace IsoMetrix.StringCalculator.Handlers.Delimiters;

public class BaseDelimiterHandler : IDelimiterHandler
{
    private readonly string[] _baseDelimiters = [",", "\n"];

    public virtual string[] ExtractDelimiters(string input, out string numbers)
    {
        numbers = input;
        return _baseDelimiters;
    }
}