using System;
using System.Collections.Generic;

namespace War
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine()); // the number of cards for player 1
            var player1 = new Queue<int>(n);
            for (var i = 0; i < n; i++)
            {
                var cardp1 = Console.ReadLine(); // the n cards of player 1
                cardp1 = cardp1.Remove(cardp1.Length - 1);

                int value;
                if (!(int.TryParse(cardp1, out value)))
                    value = ParseCard(cardp1);

                player1.Enqueue(value);
            }

            var m = int.Parse(Console.ReadLine()); // the number of cards for player 2
            var player2 = new Queue<int>();
            for (var i = 0; i < m; i++)
            {
                var cardp2 = Console.ReadLine(); // the m cards of player 2
                cardp2 = cardp2.Remove(cardp2.Length - 1);

                int value;
                if (!(int.TryParse(cardp2, out value)))
                    value = ParseCard(cardp2);

                player2.Enqueue(value);
            }

            var steps = 0;
            while (player1.Count > 0 && player2.Count > 0)
            {
                var tempQueue1 = new Queue<int>(n);
                var tempQueue2 = new Queue<int>(m);

                var card1 = player1.Dequeue();
                var card2 = player2.Dequeue();

                steps++;
                while (card1 == card2)
                {
                    tempQueue1.Enqueue(card1);
                    tempQueue2.Enqueue(card2);

                    if (player1.Count < 4 || player2.Count < 4)
                    {
                        Console.WriteLine("PAT");
                        return;
                    }

                    for (var i = 0; i < 3; i++)
                    {
                        tempQueue1.Enqueue(player1.Dequeue());
                        tempQueue2.Enqueue(player2.Dequeue());
                    }

                    card1 = player1.Dequeue();
                    card2 = player2.Dequeue();
                }

                var winner = card1 > card2 ? player1 : player2;
                while (tempQueue1.Count > 0)
                    winner.Enqueue(tempQueue1.Dequeue());
                winner.Enqueue(card1);

                while (tempQueue2.Count > 0)
                    winner.Enqueue(tempQueue2.Dequeue());
                winner.Enqueue(card2);
            }

            var winnerNum = player1.Count > 0 ? 1 : 2;
            Console.WriteLine($"{winnerNum} {steps}");
        }

        private static int ParseCard(string card)
        {
            switch (card)
            {
                case "J":
                    return 11;
                case "Q":
                    return 12;
                case "K":
                    return 13;
                case "A":
                    return 14;
                default:
                    return -1;
            }
        }
    }
}