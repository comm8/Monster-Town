using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using BuildingTools;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct BuildingGridInit : ISystem
{

    [BurstCompile]
    public void OnCreate(ref SystemState state) { state.RequireForUpdate<BuildingGlobals>(); }
    [BurstCompile]
    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {

        var buildingGlobals = SystemAPI.GetSingleton<BuildingGlobals>();
        EntityCommandBuffer commandBuffer = new EntityCommandBuffer(Allocator.Temp);

        for (int i = 0; i < math.pow(buildingGlobals.gridSize, 2); i++)
        {
            int k = i%buildingGlobals.gridSize;
            var entity = commandBuffer.Instantiate(buildingGlobals.buildingPrefab);
            commandBuffer.SetComponent<LocalTransform>(entity, new LocalTransform
            {
                Position = new float3(k, 0, i / buildingGlobals.gridSize) * 10,
                Rotation = quaternion.identity,
                Scale = 1f
            });
        }

        commandBuffer.Playback(state.EntityManager);
        commandBuffer.Dispose();

        state.Enabled = false;
    }
}




