using UnityEngine;
[CreateAssetMenu(fileName = "RangedAttack", menuName = "MonsterGame/Attacks/RangedAttack", order = 1)]
public class RangedAttack : Attack
{

    [Space(20)]
    [SerializeField] byte range;
    [SerializeField] byte damage;

    [SerializeField] float projectilespeed;


    override public void SetUp() { }

    override public void TryAttack(Vector3 AttackerPosition, Vector3 TargetPosition, EntityData target)
    {

    }
    override public void Run(Vector3 AttackerPosition, Vector3 TargetPosition, EntityData target) { }


}
