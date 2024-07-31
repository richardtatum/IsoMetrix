namespace IsoMetrix.StringCalculator.Handlers.Validation;

public interface IValidationHandler
{
    int[] Validate(IEnumerable<int> numbers);
}