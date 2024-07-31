namespace IsoMetrix.StringCalculator.Tests;

public class StringCalculatorTests
{
    [Fact]
    public void GivenAnEmptyString_AddReturnsZero()
    {
        var result = StringCalculator.Add(string.Empty);
        
        Assert.Equal(0, result);
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(2, "2")]
    [InlineData(3, "3")]
    [InlineData(4, "4")]
    public void GivenASingleNumber_AddReturnsTheInputAsInt(int expected, string input)
    {
        var result = StringCalculator.Add(input);
        
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(3, "1,2")]
    [InlineData(5, "2,3")]
    [InlineData(7, "3,4")]
    [InlineData(9, "4,5")]
    public void GivenTwoNumbersSeparatedByAComma_AddReturnsTheInputsSummedAsInt(int expected, string input)
    {
        var result = StringCalculator.Add(input);
        
        Assert.Equal(expected, result);
    }

    
    [Theory]
    [InlineData(6, "1,2,3")]
    [InlineData(10, "1,2,3,4")]
    [InlineData(15, "1,2,3,4,5")]
    [InlineData(21, "1,2,3,4,5,6")]
    public void GivenAnyAmountOfNumbersSeparatedByAComma_AddReturnsTheInputsSummedAsInt(int expected, string input)
    {
        var result = StringCalculator.Add(input);
        
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(6, "1,2\n3")]
    [InlineData(10, "1,2\n3,4")]
    [InlineData(15, "1,2\n3,4\n5")]
    [InlineData(21, "1,2\n3,4\n5,6")]
    public void GivenAnyAmountOfNumbersSeparatedByCommaOrNewLine_AddReturnsTheInputsSummedAsInt(int expected, string input)
    {
        var result = StringCalculator.Add(input);
        
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(3, ":", "1:2")]
    [InlineData(6, "-", "1-2-3")]
    [InlineData(10, ";", "1;2;3;4")]
    [InlineData(15, "'", "1'2'3'4'5")]
    public void GivenACustomDelimiterAndAnyAmountOfNumbersSeparatedByIt_AddReturnsTheInputsSummedAsInt(int expected, string customDelimiter, string input)
    {
        var inputWithDelimiter = $"//{customDelimiter}\n{input}";

        var result = StringCalculator.Add(inputWithDelimiter);
        
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GivenACustomDelimiterAndNoNumbers_AddReturnsZero()
    {
        var input = "//:\n";

        var result = StringCalculator.Add(input);
        
        Assert.Equal(0, result);
    }

    [Theory]
    [InlineData(6, "1,2\n3")]
    [InlineData(10, "1,2\n3,4")]
    [InlineData(15, "1,2\n3,4\n5")]
    [InlineData(21, "1,2\n3,4\n5,6")]
    public void GivenACustomDelimiterAndAnyAmountOfNumbersSeparatedByCommaOrNewLine_AddReturnsTheInputsSummedAsInt(int expected, string input)
    {
        var customDelimiter = ":";
        var inputWithDelimiter = $"//{customDelimiter}\n{input}";

        var result = StringCalculator.Add(inputWithDelimiter);
        
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("-1,-2,-3", "-1,-2,-3")]
    [InlineData("-1,-2,-3", "-1,-2\n-3")]
    [InlineData("-1,-2,-3,-4", "//:\n-1,-2\n-3:-4")]
    public void GivenANegativeNumber_AddReturnThrowsAnInvalidOperationExceptionWithTheNegativeNumbers(string exceptionMessageContains, string input)
    {
        var exception = Assert.Throws<InvalidOperationException>(() => StringCalculator.Add(input));
        Assert.Contains(exceptionMessageContains, exception.Message);
    }

    [Theory]
    [InlineData(6, "1,2,3,1001")]
    [InlineData(6, "1,2,3,1001,2000")]
    [InlineData(0, "1001")]
    [InlineData(0, "1001,1002,1003")]
    public void GivenANumberLargerThan1000_AddSkipsTheNumberAndReturnsTheInputsSummedAsInt(int expected, string input)
    {
        var result = StringCalculator.Add(input);
        Assert.Equal(expected, result);
    }
}