using System;

namespace zad1_l5
{
    class Program
    {

        public static (float?, float?) QuadraticFormula(float a, float b, float c)
        {

            if (a == 0 && b != 0) return (-c/b == 0 ? 0 : -c/b, null);

            if (a == 0 && b == 0) return c == 0 ? (float.PositiveInfinity, null) : (null, null);

            float delta = (b * b) - (4 * a * c);
            float sqrt_delta = (float) Math.Sqrt(delta);

            float? root1 = null;
            float? root2 = null;

            if (delta >= 0) root1 = ((-b) + sqrt_delta) / (2 * a);

            if(delta != 0) root2 = ((-b) - sqrt_delta) / (2 * a);

            return (root1 == 0 ? 0 : root1, root2 == 0 ? 0 : root2);
        }

        public static void ReadUserInput(out float value, string message)
        {
            Console.WriteLine(message);

            while(!float.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Invalid input type (Must be float)");
            }
        }

        public static void CalculateQuadraticFormula()
        {
            ReadUserInput(out float val1, "Input first value (a):");
            ReadUserInput(out float val2, "Input second value (b):");
            ReadUserInput(out float val3, "Input third value (c):");

            (float? result1, float? result2) = QuadraticFormula(val1, val2, val3);

            if (result1 == null) Console.WriteLine("There are no results");
            else if (result1 == float.PositiveInfinity) Console.WriteLine("There are infinte amount of results");
            else if (result2 == null) Console.WriteLine("There is one result of value: {0:0.00000}", result1);
            else Console.WriteLine($"There are two results of values: root1 -> {result1:0.00000}, root2 -> {result2:0.00000}");

        }

        static void Main(string[] args)
        {
            // 8 87 1, 0 0 0, 0 0 -1, -3 0 1, 0 4 1, 
            CalculateQuadraticFormula();

        }
    }
}

