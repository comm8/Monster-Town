using Unity.Mathematics;
using Unity.Burst;
using System;

namespace BuildingTools
{

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

        public static int2 PositionToTile(float3 InputPos)
        {
            return new((int)((InputPos.x + 5) / 10), (int)((InputPos.z + 5) / 10));
        }

    }

    

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
            Armory,
            None
        }
}

