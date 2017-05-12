using System;
using System.Collections.Generic;

namespace Dontpanic
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var inputs = Console.ReadLine().Split(' ');
            var nbFloors = int.Parse(inputs[0]); // number of floors
            var width = int.Parse(inputs[1]); // width of the area

            var area = new Type[width, nbFloors];

            var exitFloor = int.Parse(inputs[3]); // floor on which the exit is found
            var exitPos = int.Parse(inputs[4]); // position of the exit on its floor
            area[exitPos, exitFloor] = Type.Exit;

            var nbElevators = int.Parse(inputs[7]); // number of elevators

            for (var i = 0; i < nbElevators; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                var elevatorFloor = int.Parse(inputs[0]); // floor on which this elevator is found
                var elevatorPos = int.Parse(inputs[1]); // position of the elevator on its floor
                area[elevatorPos, elevatorFloor] = Type.Elevator;
            }

            var blockedFloors = new HashSet<int>();

            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                var cloneFloor = int.Parse(inputs[0]); // floor of the leading clone
                var clonePos = int.Parse(inputs[1]); // position of the leading clone on its floor
                var direction = (Direction) Enum.Parse(typeof(Direction), inputs[2]);

                if (direction == Direction.NONE)
                {
                    Console.WriteLine("WAIT");
                    continue;
                }

                if (blockedFloors.Contains(cloneFloor))
                {
                    Console.WriteLine("WAIT");
                    continue;
                }

                var targetPos = cloneFloor == exitFloor ? exitPos : FindElevator(area, cloneFloor);
                if ((direction == Direction.LEFT && targetPos > clonePos) ||
                    (direction == Direction.RIGHT && targetPos < clonePos))
                {
                    blockedFloors.Add(cloneFloor);
                    Console.WriteLine("BLOCK");
                    continue;
                }

                Console.WriteLine("WAIT");
            }
        }

        private static int FindElevator(Type[,] area, int floor)
        {
            for (var i = 0; i < area.GetLength(0); i++)
                if (area[i, floor] == Type.Elevator)
                    return i;

            return -1;
        }

        private enum Type
        {
            None,
            Elevator,
            Exit
        }

        private enum Direction
        {
            NONE,
            LEFT,
            RIGHT
        }
    }
}