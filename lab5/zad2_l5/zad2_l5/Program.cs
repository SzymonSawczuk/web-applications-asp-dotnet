using System;

namespace zad2_l5
{
    class Program
    {
        public static void ReadInput(ref int? value)
        {
            int temp_input;
            bool is_neg = false;

            temp_input = Console.Read();
            while (!Char.IsWhiteSpace((char)temp_input))
            {
                if (value != null && ((value != 0 && (char) temp_input == '-') ||
                                     ((char)temp_input != '-' && !Char.IsDigit((char)temp_input))))
                {
                    Console.WriteLine("Invalid char");
                    value = null;
                }

                if ((char)temp_input == '-') is_neg = true;

                else if (value != null) value = value * 10 + (((char)temp_input) - '0');
                temp_input = Console.Read();
            }

            if (is_neg) value *= -1;
        }

        public static void ReadAmount(out int amount)
        {
            while (!Int32.TryParse(Console.ReadLine(), out amount) || amount < 1)
            {
                Console.WriteLine("Invalid input");
            }
        }

        public static int FindSecondMax(int amount, ref bool is_diff)
        {
            int max_value = Int32.MinValue, second_max_value = Int32.MinValue;

            int? current_value;
            int? prev_input = null;

            while (amount > 0)
            {

                current_value = 0;
                ReadInput(ref current_value);


                if (current_value == null) continue;


                if (current_value > max_value)
                {
                    second_max_value = max_value;
                    max_value = (int)current_value;
                }

                if (second_max_value < current_value && current_value < max_value) second_max_value = (int)current_value;
                if (prev_input != null && current_value != prev_input) is_diff = true;

                amount -= 1;
                prev_input = current_value;
            }

            return second_max_value;
        }

        static void Main(string[] args)
        {
            ReadAmount(out int amount);

            bool is_diff = false;
            int second_max_value = FindSecondMax(amount, ref is_diff);


            if (!is_diff) Console.WriteLine("No results");
            else Console.WriteLine($"Second biggest number => {second_max_value}");

        }
    }
}

