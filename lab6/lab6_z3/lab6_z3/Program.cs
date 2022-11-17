using System;

namespace lab6_z3
{
    class Program
    {
        static private void PrintValue<T>(T value)
        {
            Console.Write($"{value}, ");
        }

        static void Main(string[] args)
        {
            int[] arr_int = new int[] { 2, 5, 7, 1, 9 };
            int index_found = Array.BinarySearch(arr_int, 0, arr_int.Length, 5);
            Console.WriteLine($"5 is indexed by number: {index_found}");

            Action<int> printValueInt = new Action<int>(PrintValue);
            Action<string> printValueString = new Action<string>(PrintValue);

            Array.ForEach(arr_int, printValueInt);
            Array.Sort(arr_int);

            Console.WriteLine();
            Array.ForEach(arr_int, printValueInt);

            Console.WriteLine();
            Array.Reverse(arr_int);
            Array.ForEach(arr_int, printValueInt);


            Console.WriteLine();
            string[] arr_string = Array.Empty<string>();
            Console.WriteLine($"Is array empty: {arr_string.Length == 0}");

            Console.WriteLine();
            arr_string = (string[])Array.CreateInstance(typeof(string), 15);
            Console.WriteLine($"Is array empty: {arr_string.Length == 0}");
            Array.ForEach(arr_string, printValueString);


        }
    }
}

