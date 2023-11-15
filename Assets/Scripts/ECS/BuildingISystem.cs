using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using BuildingTools;

public partial struct BuildingISystem : ISystem
{
    public void OnCreate(ref SystemState state){}
    public void OnDestroy(ref SystemState state){}

    public void OnUpdate(ref SystemState state)
    {


        UpdateBuilding job = new UpdateBuilding
        {
            deltatime = SystemAPI.Time.DeltaTime,
            buildingGlobals = SystemAPI.GetSingleton<BuildingGlobals>(),
        };
        job.ScheduleParallel();
    }


}


[BurstCompile]
public partial struct UpdateBuilding : IJobEntity
{
    [ReadOnly] public BuildingGlobals buildingGlobals;
    public float deltatime;
    static readonly int maxDepth = 20;
    public void Execute()
    {

    }

    



}
