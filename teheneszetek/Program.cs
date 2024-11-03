using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teheneszetek
{
    internal class Program
    {
        static int N, M;
        static int[] milkProduced;
        static int[] processingCapacity;
        static int[,] transportCost;
        static int minCost = int.MaxValue;
        static int[] bestAssignment;

        static void Main()
        {
            string[] firstLine = Console.ReadLine().Split();
            N = int.Parse(firstLine[0]);
            M = int.Parse(firstLine[1]);

            milkProduced = Console.ReadLine().Split().Select(int.Parse).ToArray();
            processingCapacity = Console.ReadLine().Split().Select(int.Parse).ToArray();

            transportCost = new int[N, M];
            for (int i = 0; i < N; i++)
            {
                int[] costs = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (int j = 0; j < M; j++)
                {
                    transportCost[i, j] = costs[j];
                }
            }

            bestAssignment = new int[N];
            int[] currentAssignment = new int[N];
            int[] currentCapacity = (int[])processingCapacity.Clone();

            Backtrack(0, 0, currentAssignment, currentCapacity);

            Console.WriteLine(minCost);
            for (int i = 0; i < N; i++)
            {
                Console.Write($"{bestAssignment[i] + 1} ");
            }
        }

        static void Backtrack(int farmIndex, int currentCost, int[] currentAssignment, int[] currentCapacity)
        {
            if (farmIndex == N)
            {
                if (currentCost < minCost)
                {
                    minCost = currentCost;
                    Array.Copy(currentAssignment, bestAssignment, N);
                }
                return;
            }

            for (int j = 0; j < M; j++)
            {
                int milkNeeded = milkProduced[farmIndex];
                int transportPerLiterCost = transportCost[farmIndex, j];

                if (currentCapacity[j] >= milkNeeded)
                {
                    currentAssignment[farmIndex] = j;
                    currentCapacity[j] -= milkNeeded;
                    Backtrack(farmIndex + 1, currentCost + milkNeeded * transportPerLiterCost, currentAssignment, currentCapacity);

                    currentCapacity[j] += milkNeeded;
                }
            }
        }
    }
}
