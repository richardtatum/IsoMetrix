namespace IsoMetrix.StringCalculator.Handlers.Validation;

public class NegativeNumberValidationHandler : IValidationHandler
{
    public int[] Validate(IEnumerable<int> numbers)
    {
        var numbersArray = numbers as int[] ?? numbers.ToArray();
        
        var negativeNumbers = numbersArray.Where(number => number < 0).ToArray();
        if (negativeNumbers.Length <= 0)
        {
            return numbersArray;
        }

        throw new InvalidOperationException(
            $"StringCalculator does support the addition of negative numbers. Invalid numbers: {string.Join(',', negativeNumbers)}");
    }
}