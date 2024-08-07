using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerializableDictionary.Scripts;

public class Targetting : MonoBehaviour
{
    // just some dummy prefab to spawn (use default sphere for example)
    public GameObject prefab;

    // distance to search objects from
    public float searchDistance = 3;

public Camera targetCamera;

    int maxEntityCount;

    EntityData[] entities;
    CullingGroup cullGroup;
    BoundingSphere[] bounds;


    List<ushort> InRangeEntities;

    void Start()
    {
        maxEntityCount = GameManager.instance.gridDimensions.x * GameManager.instance.gridDimensions.z + GameManager.instance.maxEnemies;

        cullGroup = new CullingGroup();
        cullGroup.targetCamera = targetCamera;

        // measure distance to our transform
        cullGroup.SetDistanceReferencePoint(transform);

        // search distance "bands" starts from 0, so index=0 is from 0 to searchDistance
        cullGroup.SetBoundingDistances(new float[] { searchDistance, float.PositiveInfinity });

        bounds = new BoundingSphere[maxEntityCount];

        // spam random objects
        entities = new EntityData[maxEntityCount];
        for (int i = 0; i < maxEntityCount; i++)
        {
            var pos = Random.insideUnitSphere * 30;
            var temp = Instantiate(prefab, pos, Quaternion.identity);
            entities[i] = temp.GetComponent<EntityData>();

            // collect bounds for objects
            var b = new BoundingSphere();
            b.position = temp.transform.position;
            b.radius = 1;
            bounds[i] = b;
        }

        // set bounds that we track
        cullGroup.SetBoundingSpheres(bounds);
        cullGroup.SetBoundingSphereCount(entities.Length);

        // subscribe to event
        cullGroup.onStateChanged += StateChanged;
    }

    public EntityData[] FindTargetsInRadius(Vector3 position, bool team, float radius)
    {
        transform.position = position;

        EntityData[] genericEntities = new EntityData[InRangeEntities.Count];
        for (int i = 0; i < InRangeEntities.Count; i++)
        {
            genericEntities[i] = entities[InRangeEntities[i]];
        }

        return genericEntities;
    }


    void StateChanged(CullingGroupEvent e)
    {
        // if we are in distance band index 0, that is between 0 to searchDistance
        if (e.currentDistance == 0)
        {
            InRangeEntities.Add((ushort)e.index);
        }
        else // too far, set color to red
        {
            InRangeEntities.Remove((ushort)e.index);
        }
         Debug.Log(InRangeEntities.Count);
    }

    // cleanup
    private void OnDestroy()
    {
        cullGroup.onStateChanged -= StateChanged;
        cullGroup.Dispose();
        cullGroup = null;
    }

}

