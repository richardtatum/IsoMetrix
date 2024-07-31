namespace IsoMetrix.StringCalculator.Handlers.Validation;

public class HighestValueValidationHandler(int highestValue) : IValidationHandler
{
    public int[] Validate(IEnumerable<int> numbers) => numbers.Where(number => number <= highestValue).ToArray();
}