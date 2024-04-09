using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using cmdwtf.UnityTools;
namespace BuildingTools
{
    public struct WeightedRandom
    {
        int[] runningTotals;

        Dictionary<int, int> pairs;

        public WeightedRandom(int[] weightList)
        {

            pairs = new();
            for (int i = 0; i < weightList.Length; i++)
            {
                pairs.Add(i,weightList[i]);
            }

            pairs.OrderByDescending(pair => pair.Value).ToList();

            runningTotals = new int[pairs.Count];
            int sum = 0;
            for (int i = 0; i < pairs.Count; i++)
            {
                sum += pairs[i];
                runningTotals[i] = sum;
            }
        }

        public int GetRandom()
        {
            int targetDistance = UnityEngine.Random.Range(0, runningTotals[^1]);
            int guessIndex = 0;
            while (true)
            {
                if (runningTotals[guessIndex] > targetDistance)
                {
                    return pairs.ElementAt(guessIndex).Key;
                }

                var weight = pairs.ElementAt(guessIndex).Value;
                var hopDistance = targetDistance - runningTotals[guessIndex];
                var hop_indices = guessIndex += 1 + (hopDistance / weight);
            }
        }

    }
}

