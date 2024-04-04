using UnityEngine;
using System;

namespace BuildingTools
{
    public struct WeightedRandom
    {
        int[] weights;
        int[] runningTotals;

        public WeightedRandom(int[] weightList)
        {
            weights = weightList;
            Array.Sort(weights, (x, y) => y.CompareTo(x));
            runningTotals = new int[weights.Length];

            int sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i];
                runningTotals[i] = sum;
            }
        }

        public int GetRandom()
        {
            int targetDistance = UnityEngine.Random.Range(0, runningTotals[^1]);
            var guess = 0;
            while (true)
            {
                if (runningTotals[guess] > targetDistance)
                {
                    return guess;
                }

                var weight = weights[guess];
                var hopDistance = targetDistance - runningTotals[guess];
                var hop_indices = guess += 1 + (hopDistance / weight);
            }
        }

    }
}

