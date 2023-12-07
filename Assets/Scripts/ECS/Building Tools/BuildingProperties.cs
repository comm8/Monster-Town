using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;
using BuildingTools;
using Unity.Transforms;
using Unity.Collections;
using System;

[BurstCompile]
public struct BuildingProperties : IComponentData
{
    public BuildingType buildingType;
    public MonsterType monsterType;
    public int slotID;


}


[BurstCompile]
public readonly partial struct BuildingAspect : IAspect
{


    public readonly RefRW<BuildingProperties> buildingProperties;
    public readonly RefRW<LocalTransform> transform;

    public BuildingType buildingType => buildingProperties.ValueRW.buildingType;
    public MonsterType monsterType => buildingProperties.ValueRW.monsterType;

    public int slotID => buildingProperties.ValueRO.slotID;

    public float3 Position
    {
        get => transform.ValueRO.Position;
        set => transform.ValueRW.Position = value;
    }
}


namespace BuildingTools
{

    [BurstCompile]
    [Serializable]
    public class ResourceValue
    {
        public int Amount;
        public ResourceType Type;
    }



    public enum ResourceType : byte
    {
        Lumber,
        Charcoal,
        Stone,
        Metal,
        Rations,
        Refined_Alloy,
        Cursed_Alloy
    }

    public enum MonsterType : byte
    {
        Kobold,
        Orc,
        Mimic,
        Clown,
        Wyvern,
        Gargoyle,
        Gorgon,
        Mindflayer,
        Plantoid,
        Skeleton,
        NoUnit
    }


    public struct ResourceTable
    {
        public FixedList32Bytes<ResourceTable> KoboldProduction;
        public FixedList32Bytes<ResourceTable> OrcProduction;
        public FixedList32Bytes<ResourceTable> MimicProduction;
        public FixedList32Bytes<ResourceTable> ClownProduction;
        public FixedList32Bytes<ResourceTable> WyvernProduction;
        public FixedList32Bytes<ResourceTable> GargoyleProduction;
        public FixedList32Bytes<ResourceTable> GorgonProduction;
        public FixedList32Bytes<ResourceTable> MindflayerProduction;
        public FixedList32Bytes<ResourceTable> PlantoidProduction;
        public FixedList32Bytes<ResourceTable> SkeletonProduction;
    }

    public enum BuildingType : byte
    {
        Farm,
        Lumber_Yard,
        Mine,
        Inn,
        Forge,
        NecroMansion,
        Fishing_Dock,
        Light_House,
        Apothecary,
        Armory
    }

    [BurstCompile]
    public static class BuildingUtils
    {
        public static int2 SlotIDToCoords(int slotID, int tileSize)
        {
            int mod = slotID % tileSize;
            return new int2(mod, slotID - mod);
        }

        public static int CoordsToSlotID(int2 coords, int tileSize)
        {
            return coords.x + (coords.y * tileSize);
        }
    }




}
