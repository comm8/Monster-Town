using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using BuildingTools;


public class Pathfinding : MonoBehaviour
{
    // Start is called before the first frame update

     public  Pathfinding instance;
    public BlockerGrid blockerGrid;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }


   bool Pathfind(int2 start, int2 end, out int2[] path)
   {
    path = new int2[4];
    return false;
   }


    



}

public class BlockerGrid
{

    private BitArray blockerMask = new BitArray( new bool[400]);
    
    public void SetBlocker(int2 pos, bool value)
    {
        blockerMask[BuildingUtils.CoordsToSlotID(pos, 20) ] = value;
    }
    public bool GetBlocker(int2 pos, bool value)
    {
       return blockerMask[BuildingUtils.CoordsToSlotID(pos, 20)];
    }
    

}