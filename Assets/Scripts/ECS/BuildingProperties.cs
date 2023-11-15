using Unity.Entities;
using Unity.Mathematics;
using Unity.Burst;
using BuildingTools;

[BurstCompile]
    public struct BuildingProperties : IComponentData
    {
        public BuildingType buildingType;
       public MonsterType monsterType;
        public int slotID;

    }


namespace BuildingTools
{
    
[BurstCompile]

    public struct ResourceValue
    {
        public int Amount;
        public ResourceType Type;
    }



            public enum ResourceType 
        {
            Lumber,
            Charcoal,
            Stone,
            Metal,
            Rations,
            Refined_Alloy,
            Cursed_Alloy
        }

        public enum MonsterType
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

        public enum BuildingType
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
            int mod = slotID%tileSize;
            return new int2(mod,slotID - mod);
        }

        public static int CoordsToSlotID(int2 coords, int tileSize)
        {
            return coords.x + (coords.y * tileSize);
        }
    }




}
