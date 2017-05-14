using System;
using System.Collections.Generic;

namespace Skynet_Revolution
{
    internal class Program
    {
        private class Item
        {
            public int Value { get; set; }
            public int Parent { get; set; } = -1;
            public int Generation { get; set; }
        }

        private static Path FindPath(int[,] matrix, int startIndex, int endIndex)
        {
            var queue = new Queue<Item>(matrix.Length);
            var set = new HashSet<int>();
            var generations = new Dictionary<int, int>();

            queue.Enqueue(new Item {Value = startIndex, Generation = 0});
            set.Add(startIndex);
            generations.Add(startIndex, 0);

            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                if (item.Value == endIndex)
                    return new Path(new Tuple<int, int>(item.Parent, item.Value), item.Generation);

                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    if (matrix[i, item.Value] == 0 || set.Contains(i))
                        continue;

                    set.Add(i);
                    generations.Add(i, generations[item.Value] + 1);

                    queue.Enqueue(new Item
                    {
                        Parent = item.Value,
                        Value = i,
                        Generation = generations[i]
                    });
                }
            }

            return new Path(new Tuple<int, int>(-1, -1), -1);
        }

        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            var N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
            var matrix = new int[N, N];

            var L = int.Parse(inputs[1]); // the number of links
            var E = int.Parse(inputs[2]); // the number of exit gateways
            for (var i = 0; i < L; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                var N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
                var N2 = int.Parse(inputs[1]);
                matrix[N1, N2] = matrix[N2, N1] = 1;
            }

            var gateways = new int[E];
            for (var i = 0; i < E; i++)
                gateways[i] = int.Parse(Console.ReadLine()); // the index of a gateway node

            // game loop
            while (true)
            {
                var SI = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");
                var min = FindPath(matrix, SI, gateways[0]);
                for (int i = 1; i < E; i++)
                {
                    var candidate = FindPath(matrix, SI, gateways[i]);
                    if (candidate < min)
                        min = candidate;
                }

                // Example: 0 1 are the indices of the nodes you wish to sever the link between
                Console.WriteLine($"{min.LinkCoordinates.Item1} {min.LinkCoordinates.Item2}");
            }
        }

        class Path
        {
            public Tuple<int, int> LinkCoordinates { get; set; }
            private int Count { get; set; }

            public Path(Tuple<int, int> linkCoordinates, int count)
            {
                LinkCoordinates = linkCoordinates;
                Count = count;
            }

            public static bool operator <(Path a, Path b) => a.Count < b.Count;

            public static bool operator >(Path a, Path b) => a.Count > b.Count;
        }
    }
}