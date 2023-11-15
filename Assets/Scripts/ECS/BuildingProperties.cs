using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;
using BuildingTools;

[BurstCompile]
    public struct BuildingProperties : IComponentData
    {
       public MonsterType monsterType;







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

    [BurstCompile]
    public static class BuildingUtils
    {
        public static int GetListPos()
        {
            return 0;
        }



    } 
}
