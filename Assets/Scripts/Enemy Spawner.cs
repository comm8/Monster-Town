using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using SerializableDictionary.Scripts;
using System;
using System.Linq;


public class EnemySpawner : MonoBehaviour
{

    [SerializeField] SelectionWeight[] weights;

    [SerializeField] int[] runningTotals;
    int GetWaveSpawnTokens(int currentWave)
    {
        return (int)(math.pow(math.E, currentWave * -0.21f) + currentWave + 9);
    }

    void SpawnWave(int tokens)
    {
        //take tokens and return weighed results

    }


    void Awake()
    {
        SetUpHopscotch_selection();
        Debug.Log(WeightedRandom());
    }

    void SetUpHopscotch_selection()
    {
        Array.Sort(weights, (x, y) => y.chance.CompareTo(x.chance));
        Debug.Log(weights);
        runningTotals = new int[weights.Length];

        int sum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i].chance;
            runningTotals[i] = sum;
        }

    }

    SelectionWeight WeightedRandom()
    {
        int targetDistance = UnityEngine.Random.Range(0, runningTotals[^1]);
        Debug.Log(targetDistance);
        var guess = 0;
        while (true)
        {
            if (runningTotals[guess] > targetDistance)
            {
                return weights[guess];
            }

            var weight = weights[guess];
            var hopDistance = targetDistance - runningTotals[guess];
            var hop_indices =
            guess += 1 + (int)(hopDistance / weight.chance);
        }
    }

    int[] AccumulatedSums(int[] array)
    {
        var accumulatedSums = array.Select((n, i) => array.Take(i + 1).Sum());

        return array;
    }


}

[Serializable]
public struct SelectionWeight
{
    public int chance;
    public string name;

    public override string ToString()
    {
        return name + "" + chance; 
    }

}