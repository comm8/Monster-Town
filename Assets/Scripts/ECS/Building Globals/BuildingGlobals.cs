using Unity.Entities;
using Unity.Burst;
namespace BuildingTools
{
  [BurstCompile]
  public struct BuildingGlobals : IComponentData
  {
        public bool finishedEntityCreation;

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

    public readonly partial struct BuildingGlobalsAspect : IAspect
    {
        public readonly DynamicBuffer<BuildingAspectListBuffer> buildingAspectListBuffers;
        public readonly RefRW<BuildingGlobals> globals;

        public int gridSize
        {
            get => globals.ValueRO.gridSize;
            set => globals.ValueRW.gridSize = value;
        }

    }

    // This describes the number of buffer elements that should be reserved
    // in chunk data for each instance of a buffer. In this case, 8 integers
    // will be reserved (32 bytes) along with the size of the buffer header
    // (currently 16 bytes on 64-bit targets)
    [InternalBufferCapacity(3)]
    public struct BuildingAspectListBuffer : IBufferElementData
    {
        // These implicit conversions are optional, but can help reduce typing.
        public static implicit operator BuildingAspect(BuildingAspectListBuffer e) { return e.Value; }
        public static implicit operator BuildingAspectListBuffer(BuildingAspect e) { return new BuildingAspectListBuffer { Value = e }; }

        // Actual value each buffer element will store.
        public BuildingAspect Value;
    }
}







