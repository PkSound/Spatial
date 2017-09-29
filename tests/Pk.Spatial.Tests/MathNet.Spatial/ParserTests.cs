using System.Globalization;
using System.Text.RegularExpressions;
using Pk.Spatial.Tests;
using Shouldly;
using Xunit;

namespace MathNet.Spatial.UnitTests
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class ParserTests
  {
    [Theory]
    [InlineData("1", 1)]
    [InlineData(".1", .1)]
    [InlineData("1.2", 1.2)]
    [InlineData("1.2E+3", 1.2E+3)]
    [InlineData("1.2e+3", 1.2E+3)]
    [InlineData("1.2E3", 1.2E3)]
    [InlineData("1.2e3", 1.2E3)]
    [InlineData("1.2E-3", 1.2E-3)]
    [InlineData("1.2e-3", 1.2E-3)]
    public void DoublePattern(string s, double expected)
    {
      Regex.IsMatch(s, Parser.DoublePattern).ShouldBeTrue();
      expected.ShouldBe(double.Parse(s, CultureInfo.InvariantCulture));
    }
  }
}
