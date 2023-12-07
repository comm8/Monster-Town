using Unity.Entities;
using Unity.Burst;
using BuildingTools;

namespace BuildingTools
{
  [BurstCompile]
  public struct BuildingGlobals : IComponentData
  {
    public int gridSize;
    public Entity buildingPrefab;

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
}
