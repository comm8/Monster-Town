using UnityEngine;
using Unity.Collections;
using BuildingTools;
using System;
using Unity.Burst;

[BurstCompile]
public class BuildingGlobalsMono : MonoBehaviour
{
    [Serializable]
    [BurstCompile]
    public struct ResourceGlobals
    {
        public ResourceTable farmResourceTable;
        public ResourceTable lumber_YardResourceTable;
        public ResourceTable mineResourceTable;
        public ResourceTable innResourceTable;
        public ResourceTable forgeResourceTable;
        public ResourceTable necroMansionResourceTable;
        public ResourceTable fishing_DockResourceTable;
        public ResourceTable light_HouseResourceTable;
        public ResourceTable apothecaryResourceTable;
        public ResourceTable armoryResourceTable;
    }


    
    [SerializeField] public ResourceGlobals resourceGlobals;

     public int gridSize;
     public GameObject buildingPrefab;

}


