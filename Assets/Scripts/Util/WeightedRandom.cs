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

        List<KeyValuePair<int, int>> pairs;

        public WeightedRandom(int[] weightList)
        {

            pairs = new();
            for (int i = 0; i < weightList.Length; i++)
            {
                pairs.Add(new(i, weightList[i]));
            }
            pairs = pairs.OrderByDescending(pair => pair.Value).ToList();


            runningTotals = new int[pairs.Count];
            int sum = 0;
            for (int i = 0; i < pairs.Count; i++)
            {
                sum += pairs[i].Value;
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
                    return pairs[guessIndex].Key;
                }
                guessIndex += 1 + ((targetDistance - runningTotals[guessIndex]) / pairs[guessIndex].Value);
            }
        }

    }
}

