using CalcApp;

namespace TestProject1;

[TestClass]
public sealed class CalculatorTests
{
    private Calculator _calculator;

    [TestInitialize]
    public void Setup()
    {
        _calculator = new Calculator();
    }

    [TestMethod]
    public void Add_TwoNumbers_ReturnsSum()
    {
        int result = _calculator.Add(5, 3);
        Assert.AreEqual(8, result);
    }

    [TestMethod]
    public void Sub_TwoNumbers_ReturnsSub()
    {
        int result = _calculator.Subtract(10, 4);
        Assert.AreEqual(6, result);
    }
}

