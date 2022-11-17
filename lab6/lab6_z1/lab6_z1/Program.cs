using System;

namespace lab6_z1
{
    class Program
    {
        public static void printTuple((string, string, int, double)  tuple)
        {
            Console.WriteLine($"Name: {tuple.Item1}, Surname: {tuple.Item2}, Age: {tuple.Item3}, Salary: {tuple.Item4:0.00}PLN");

            (string Name, string Surname, int Age, double Salary) tuple2 = tuple;
            Console.WriteLine($"Name: {tuple2.Name}, Surname: {tuple2.Surname}, Age: {tuple2.Age}, Salary: {tuple2.Salary:0.00}PLN");

            var (name, surname, age, salary) = tuple;
            Console.WriteLine("Name: {0}, Surname: {1}, Age: {2}, Salary: {3:0.00}PLN", name, surname, age, salary);

            Console.Write(tuple.ToString());

        }


        static void Main(string[] args)
        {
            var info = ("Szymon", "Sawczuk", 20, 4500.00);
            printTuple(info);

        }
    }
}

