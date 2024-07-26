using Unity.Mathematics;
using Unity.Burst;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace BuildingTools
{

    [BurstCompile]
    public static class BuildingUtils
    {
        public static int3 SlotIDToCoords(int ID, int3 gridDimensions)
        {
            int x = ID % gridDimensions.x;
            int y = ID / gridDimensions.x % gridDimensions.y;
            int z = ID / (gridDimensions.x * gridDimensions.y);
            return new int3(x,y,z);
        }

        public static int CoordsToSlotID(int3 coords, int3 gridDimensions)
        {
            return coords.x + (coords.y * gridDimensions.x) + (coords.z * gridDimensions.x * gridDimensions.y);
        }

        public static int3 PositionToTile(float3 InputPos)
        {
            return new((int)((InputPos.x + 5) / 10), (int)((InputPos.y + 5) / 10), (int)((InputPos.z + 5) / 10));
        }

        public static string toNumeralString(bool input)
        {
            return input ? "1" : "0";
        }


        public static int3[] GetAdjacentTiles(int3 centerTile)
        {
            int3[] offsets = {
            new int3(1, 0, 0),    // Right
            new int3(-1, 0, 0),   // Left
            new int3(0, 0, 1),    // Up
            new int3(0, 0, -1)    // Down
        };

            for (int i = 0; i < 4; i++)
            {
                offsets[i] = offsets[i] + centerTile;
            }

            return offsets;
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





    [Serializable]
    public class ResourceValue
    {
        public int Amount;
        public ResourceType Type;
    }


    [Serializable]
    public class MonsterProduction
    {
        public MonsterType monsterType;
        public ResourceValue[] production;
        public ResourceValue[] cost;

        public override string ToString()
        {
            string myString = "";
            int iterator = 0;

            foreach (ResourceValue resource in production)
            {
                if (iterator != 0)
                {
                    myString += ", ";
                }
                iterator++;
                myString += resource.Amount + " " + resource.Type;
            }
            if (myString.Equals(""))
            {
                return "nothing";
            }
            return myString;
        }
    }

    public static class Inventory
    {
        public static void AddToInventory(ResourceValue[] inventory, ResourceValue[] toAdd)
        {
            foreach (ResourceValue item in toAdd)
            {
                bool foundMatch = false;
                foreach (ResourceValue invItem in inventory)
                {
                    if (item.Type == invItem.Type)
                    {
                        invItem.Amount += item.Amount;
                        foundMatch = true;
                        break;
                    }
                }
                if (!foundMatch)
                {
                    inventory[inventory.Length] = item;
                }
            }
        }

        public static bool TryChargeCost(BuildingType desired, bool chargeOnSuccess)
        {
            return TryChargeCost(GameManager.instance.inventory, GameManager.instance.buildings.GetBuilding((int)desired).cost, chargeOnSuccess);
        }

        public static bool TryChargeCost(ResourceValue[] inventory, ResourceValue[] cost, bool chargeOnSuccess)
        {
            List<int> cache = new();

            foreach (ResourceValue debt in cost)
            {
                for (int i = 0; i < inventory.Length; i++)
                {
                    var resource = inventory[i];

                    if (resource.Type == debt.Type)
                    {
                        if (resource.Amount >= debt.Amount)
                        {
                            cache.Add(i);
                        }
                        else
                        {
                            //CANT AFFORD
                            return false;
                        }
                    }
                }
            }
            //We can afford it!! 

            if(!chargeOnSuccess)
            {
                return true;
            }

            for (int i = 0; i < cache.Count; i++)
            {
                inventory[cache[i]].Amount -= cost[i].Amount;
            }

            return true;
        }

    }





    public enum DamageType : byte
    {
        blunt,
        magic,
        fire,
        poison,
        freezing
    }


    public enum BehaviorState : byte
    {
        Idle,
        Pathfinding,
        Stunned,
        Attacking,
        Grouped
    }





    public enum ResourceType : byte
    {
        Lumber,
        Charcoal,
        Stone,
        Metal,
        Rations,
        Refined_Alloy,
        Cursed_Alloy,
        Influence
    }

    public enum MonsterType : byte
    {
        Kobold = 0,
        Orc,
        Wyvern,
        Mindflayer,
        Plantoid,
        Skeleton,
        NoUnit
    }


    [Serializable]
    public enum BuildingType : byte
    {
        Farm = 0,
        Lumber_Yard,
        Mine,
        Inn,
        Forge,
        Graveyard,
        Fishing_Dock,
        Light_House,
        Apothecary,
        Armory,
        None,
        Road,
        Tower
    }


    [Serializable]
    public class BuildingList
    {
        public BuildingType GetBuildingType(int iD)
        {
            return (BuildingType)iD;
        }

        public Building GetBuilding(int iD)
        {
            return buildings[iD];
        }

        public Building GetBuilding(BuildingType type)
        {
            return buildings[(int)type];
        }

        public Building[] buildings;
    }


    [Serializable]
    public class RoadTable
    {
        public override string ToString()
        {
            return BuildingUtils.toNumeralString(up) + BuildingUtils.toNumeralString(down) + BuildingUtils.toNumeralString(left) + BuildingUtils.toNumeralString(right);
        }

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
        public TileProperties tile;
        public Sprite icon;
        public ushort ID;
    }
}

