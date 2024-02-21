using Unity.Mathematics;
using Unity.Burst;
using System;
using UnityEngine;

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

        public static String toNumeralString(bool input)
        {
            return input ? "1" : "0";
        }


    }

    public static class VectorExtensions
    {
        public static Vector3 X(float x)
        {
            return new Vector3(x, 0, 0);
        }

        public static Vector3 Y(float y)
        {
            return new Vector3(0, y, 0);
        }

        public static Vector3 Z(float z)
        {
            return new Vector3(0, 0, z);
        }

        public static Vector3 XY(float x, float y) { return new Vector3(x, y, 0); }
        public static Vector3 XZ(float x, float z) { return new Vector3(x, 0, z); }

        public static Vector3 YZ(float y, float z) { return new Vector3(0, y, z); }

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
        Kobold = 0,
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
        None,
        Road
    }

    [Serializable]
    public class RoadTable
    {
        public bool left, right, up, down;
    }

    [Serializable]
    public class BuildingStats
    {
        public MonsterType monsterType;
        public BuildingType buildingType;
    }

    [Serializable]
    public class MonsterStats
    {
        public MonsterType type;
        public string name;
        public int2 tile;
        public Sprite icon;
    }
}

