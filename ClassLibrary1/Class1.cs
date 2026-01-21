namespace CalcApp
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
        public int Subtract(int a, int b)
        {
            return a - b;
        }

        public int Mul(int a, int b)
        {
            return a * b;
        }

        public int Divide(int a, int b)
        {
            if (b == 0) throw new DivideByZeroException("can nto divide it by zero....");
            return a / b;
        }
    }
}