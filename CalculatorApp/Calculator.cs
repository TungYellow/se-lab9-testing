namespace CalculatorApp
{
    /// <summary>
    /// Integer calculator used by the xUnit tests (Exercise 1 - xUnit version, Exercise 2 - CSV data).
    /// Integer arithmetic is unchecked, so int.MaxValue + 1 wraps around to int.MinValue.
    /// </summary>
    public class Calculator
    {
        public int Add(int a, int b) => unchecked(a + b);

        public int Subtract(int a, int b) => unchecked(a - b);

        public int Multiply(int a, int b) => unchecked(a * b);

        public double Divide(int a, int b)
        {
            if (b == 0)
                throw new DivideByZeroException("Division by zero is not allowed.");
            return (double)a / b;
        }
    }
}
