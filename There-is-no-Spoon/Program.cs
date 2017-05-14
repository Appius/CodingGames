using System;

namespace There_is_no_Spoon
{
    public static class Player
    {
        static void Main()
        {
            var width = int.Parse(Console.ReadLine()); // the number of cells on the X axis
            var height = int.Parse(Console.ReadLine()); // the number of cells on the Y axis
            var matrix = new string[height];

            for (var i = 0; i < height; i++)
                matrix[i] = Console.ReadLine(); // width characters, each either 0 or .

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    if (matrix[y][x] == '.')
                        continue;

                    var right = x + 1;
                    while (right < width && matrix[y][right] != '0')
                        right++;
                    if (right == width)
                        right = -1;

                    var bottom = y + 1;
                    while (bottom < height && matrix[bottom][x] != '0')
                        bottom++;
                    if (bottom == height)
                        bottom = -1;

                    var yRight = right == -1 ? -1 : y;
                    var xBottom = bottom == -1 ? -1 : x;
                    Console.WriteLine($"{x} {y} {right} {yRight} {xBottom} {bottom}");
                }
            }
        }
    }
}