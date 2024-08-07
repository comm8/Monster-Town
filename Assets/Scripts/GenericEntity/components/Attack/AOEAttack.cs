using UnityEngine;

[CreateAssetMenu(fileName = "AOEAttack", menuName = "MonsterGame/Attacks/AOEAttack", order = 1)]
public class AOEAttack : Attack
{

    [Space(20)]
    [SerializeField] float radius;
    [SerializeField] byte damage;


    [SerializeField] private GameObject AOERangeVisual;

    override public void SetUp()
    {
        //AOERangeVisual = GameObject.Instantiate()
    }

    override public void TryAttack(Vector3 AttackerPosition, Vector3 TargetPosition, EntityData target)
    {

    }
    override public void Run(Vector3 AttackerPosition, Vector3 TargetPosition, EntityData target)
    {

    }
}