using System;

namespace lab6_z5
{
    class Program
    {

        static public void DrawLineWithText(ref string line, string text, int width, int indexToStartText, int length)
        {
            line = line[0..width];
            line += (new string(' ', indexToStartText - width));
            line += text;
            line += (new string(' ', length - line.Length - width));
            line += line[0..width];
        }

        static public void DrawCard(string firstLine, string secondLine = "", char symbol = '!', int width = 2, int minLength = 20)
        {

            int height = 2 * width + (secondLine != "" ? 2 : 1);
            int currentHeight = height;

            minLength = minLength - (2 * width) <= firstLine.Length ? firstLine.Length + 2 * (width + 1) : minLength;
            minLength = minLength - (2 * width) <= secondLine.Length ? secondLine.Length + 2 * (width + 1) : minLength;

            int indexToStartFirstLine = (int)(minLength * 0.5) - (int)(firstLine.Length * 0.5);
            int indexToStartSecondLine = (int)(minLength * 0.5) - (int)(secondLine.Length * 0.5);

            minLength = firstLine.Length > secondLine.Length ?
                (minLength - width == indexToStartFirstLine + firstLine.Length ? minLength + 1 : minLength) :
                minLength - width == indexToStartSecondLine + secondLine.Length ? minLength + 1 : minLength;

            string line = "";
            while (currentHeight != 0)
            {
                if (currentHeight > height - width || currentHeight <= width) line = new string(symbol, minLength);

                if (currentHeight == height - width) DrawLineWithText(ref line, firstLine, width, indexToStartFirstLine, minLength);
                if (height % 2 == 0 && currentHeight == height - width - 1) DrawLineWithText(ref line, secondLine, width, indexToStartSecondLine, minLength);


                Console.WriteLine(line);
                currentHeight -= 1;
            }

        }

        static void Main(string[] args)
        {
            DrawCard("Szymon", "Wojciech", symbol: 'x', width: 2, minLength: 10);
        }
    }
}

