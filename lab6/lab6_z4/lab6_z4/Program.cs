using System;

namespace lab6_z1
{
    class Program
    {
        public static void printTuple(dynamic tuple)
        {
            Console.WriteLine($"{tuple}");

            Console.WriteLine($"Name: {tuple.Name}, Surname: {tuple.Surname}, Age: {tuple.Age}, Salary: {tuple.Salary:0.00}PLN");

            var (name, surname, age, salary) = (tuple.Name, tuple.Surname, tuple.Age, tuple.Salary);
            Console.WriteLine("Name: {0}, Surname: {1}, Age: {2}, Salary: {3:0.00}PLN", name, surname, age, salary);



        }


        static void Main(string[] args)
        {
            var info = new { Name = "Szymon", Surname = "Sawczuk", Age = 20, Salary = 4500.00 };
            printTuple(info);

        }
    }
}

