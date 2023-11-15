   using Unity.Entities;
using Unity.Burst;

namespace BuildingTools
{
    [BurstCompile]
     public struct BuildingGlobals : IComponentData
 {
   public int gridSize;
   public Entity buildingPrefab;
 }
}
