using UnityEngine;


[CreateAssetMenu(fileName = "DirectAttack", menuName = "MonsterGame/Attacks/DirectAttack", order = 1)]
public class DirectAttack : Attack
{
    [Space(20)]
    [SerializeField] byte range;

    [SerializeField] byte damage;
    override public void SetUp() { }
    override public void TryAttack(Vector3 AttackerPosition, Vector3 TargetPosition, GenericEntity target)
    {

        //if(!Physics.Raycast(AttackerPosition, TargetPosition - AttackerPosition, out RaycastHit hit, range, mask)) {return;}
        Run(AttackerPosition, TargetPosition, target);



    }
    override public void Run(Vector3 AttackerPosition, Vector3 TargetPosition, GenericEntity target)
    {
        target.Damage(type, damage);
    }

    public override void DrawGizmos(Vector3 position)
    {
                // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(position, range);
    }

    public override GenericEntity TryGetTarget(Vector3 position)
    {
       RaycastHit[] hits =  Physics.SphereCastAll(position, range, Vector3.forward * 0.001f , 0, mask, QueryTriggerInteraction.Collide);
        if(hits.Length > 0)
        {
        RaycastHit best = hits[0];
        foreach (RaycastHit hit in hits)
        {
            if (Vector3.SqrMagnitude(hit.point - position) > Vector3.SqrMagnitude(best.point - position))
            {
                best = hit;
            }
        }

        return best.collider.GetComponent<GenericEntity>();
        }
        else { return null; }

    }
}
