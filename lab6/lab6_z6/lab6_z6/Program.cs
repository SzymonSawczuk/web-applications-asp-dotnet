using System;

namespace lab6_z6
{
    class Program
    {
        public static (int, int, int, int) CountMyTypes(params dynamic[] args)
        {
            int amountInt = 0;
            int amountDouble = 0;
            int amountString = 0;
            int amountOther = 0;

            foreach (var arg in args)
            {
                switch(arg)
                {
                    case int arg_int when arg_int % 2 == 0:
                        amountInt += 1;
                        break;
                    case double arg_double when arg_double > 0:
                        amountDouble += 1;
                        break;
                    case string arg_string when arg_string.Length > 4:
                        amountString += 1;
                        break;
                    default:
                        amountOther += 1;
                        break;
                }
            }

            return (amountInt, amountDouble, amountString, amountOther);

        }

        static void Main(string[] args)
        {
            var (amountInt, amountDouble, amountString, amountOther) = CountMyTypes(2, 2.0, 3, -1.0, "testow", "Ala", 4, null, 'X');
            Console.WriteLine($"Ilosc parzystych int:{amountInt}\nIlosc dodatnich double:{amountDouble}\nIlosc string o dlugosci min 5:{amountDouble}\nIlosc pozostalych:{amountOther}");
        }
    }
}

