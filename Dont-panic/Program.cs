using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Dont_panic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            int nbFloors = int.Parse(inputs[0]); // number of floors
            int width = int.Parse(inputs[1]); // width of the area

            var area = new Type[width, nbFloors];

            int exitFloor = int.Parse(inputs[3]); // floor on which the exit is found
            int exitPos = int.Parse(inputs[4]); // position of the exit on its floor
            area[exitPos, exitFloor] = Type.Exit;

            int nbElevators = int.Parse(inputs[7]); // number of elevators

            for (int i = 0; i < nbElevators; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int elevatorFloor = int.Parse(inputs[0]); // floor on which this elevator is found
                int elevatorPos = int.Parse(inputs[1]); // position of the elevator on its floor
                area[elevatorPos, elevatorFloor] = Type.Elevator;
            }

            var blockedFloors = new HashSet<int>();

            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                int cloneFloor = int.Parse(inputs[0]); // floor of the leading clone
                int clonePos = int.Parse(inputs[1]); // position of the leading clone on its floor
                Direction direction = (Direction) Enum.Parse(typeof(Direction), inputs[2]);

                var targetPos = cloneFloor == exitFloor ? exitPos : FindElevator(area, cloneFloor);
                if (blockedFloors.Contains(cloneFloor))
                {
                    Console.WriteLine("WAIT");
                    continue;
                }

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

            for (int i = 0; i < area.GetLength(1); i++)
                if (area[i, floor] == Type.Elevator)
                    return i;

            return -1;
        }

        enum Type
        {
            None,
            Elevator,
            Exit
        }

        enum Direction
        {
            NONE,
            LEFT,
            RIGHT
        }
    }
}