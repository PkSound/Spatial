using Shouldly;
using Xunit;

namespace Pk.Spatial.Tests
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class AppVeyorCheckTest
  {
    [Fact]
    public void TestShouldPass()
    {
      // Intentional failure to convince myself that appveyor will catch failing tests
      false.ShouldBeFalse();
    }
  }
}
