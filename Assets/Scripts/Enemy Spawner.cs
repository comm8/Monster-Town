using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using BuildingTools;
using System;

public class EnemySpawner : MonoBehaviour
{

    public int wave;

    public SpawnItem[] spawnEntities;
    WeightedRandom weightedRandom;

    int GetWaveSpawnTokens(int currentWave)
    {
        return (int)(math.pow(math.E, currentWave * 0.5f) + (currentWave * 5) + 9);
    }

    void SpawnWave()
    {



    }


    void Awake()
    {

        int tokens = GetWaveSpawnTokens(wave);
        Debug.Log(tokens + " tokens on wave " + wave);

        int[] ints = new int[spawnEntities.Length];
        for (int i = 0; i < spawnEntities.Length; i++)
        {
            ints[i] = spawnEntities[i].weight;
        }

        weightedRandom = new(ints);

        while (tokens > 0)
        {
            var unit = spawnEntities[weightedRandom.GetRandom()];
            if (unit.cost > tokens) { continue; }

            tokens -= unit.cost;

            Debug.Log(unit.name);

        }

        Debug.Log(spawnEntities[weightedRandom.GetRandom()].name);
    }
}

[Serializable]
public struct SpawnItem
{
    public int cost;
    public int weight;

    public string name;
}