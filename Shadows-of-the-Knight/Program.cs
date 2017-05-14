
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.AccessControl;

namespace Shadows_of_the_Knight
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            var W = int.Parse(inputs[0]); // width of the building.
            var H = int.Parse(inputs[1]); // height of the building.
            var N = int.Parse(Console.ReadLine()); // maximum number of turns before game over.

            inputs = Console.ReadLine().Split(' ');
            var X0 = int.Parse(inputs[0]);
            var Y0 = int.Parse(inputs[1]);

            var xRange = new Range(0, W);
            var yRange = new Range(0, H);

            var x = X0;
            var y = Y0;
            // game loop
            while (true)
            {
                var bombDir = Console.ReadLine(); // the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)

                Debug.Assert(bombDir != null, $"{nameof(bombDir)} != null");
                var direction = (Direction) Enum.Parse(typeof(Direction), bombDir);

                switch (direction)
                {
                    case Direction.U:
                        yRange.Shrink(y, false);
                        break;
                    case Direction.UR:
                        xRange.Shrink(x, true);
                        yRange.Shrink(y, false);
                        break;
                    case Direction.R:
                        xRange.Shrink(x, true);
                        break;
                    case Direction.DR:
                        xRange.Shrink(x, true);
                        yRange.Shrink(y, true);
                        break;
                    case Direction.D:
                        yRange.Shrink(y, true);
                        break;
                    case Direction.DL:
                        xRange.Shrink(x, false);
                        yRange.Shrink(y, true);
                        break;
                    case Direction.L:
                        xRange.Shrink(x, false);
                        break;
                    case Direction.UL:
                        xRange.Shrink(x, false);
                        yRange.Shrink(y, false);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                x = xRange.GetMiddle();
                y = yRange.GetMiddle();
                // the location of the next window Batman should jump to.
                Console.WriteLine($"{x} {y}");
            }
        }

        enum Direction
        {
            U,
            UR,
            R,
            DR,
            D,
            DL,
            L,
            UL
        }

        class Range
        {
            private int Bottom { get; set; }
            private int Up { get; set; }

            public Range(int bottom, int up)
            {
                Bottom = bottom;
                Up = up;
            }

            public void Shrink(int coordinate, bool positiveDirection)
            {
                if (positiveDirection)
                    Bottom = coordinate;
                else
                    Up = coordinate;
            }

            public int GetMiddle()
            {
                return Bottom + (Up - Bottom) / 2;
            }

            public override string ToString() => $"[{Bottom} {Up}]";
        }
    }
}