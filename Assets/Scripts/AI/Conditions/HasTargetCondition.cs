using UnityEngine;

[CreateAssetMenu(fileName = "HasTargetCondition", menuName = "AI/Conditions/Has Target")]
public class HasTargetCondition : AICondition
{
    public override AIConfigType ConfigType => AIConfigType.None;

    protected override bool CheckCondition(CharacterMain owner, CharacterMain target)
    {
        // 타겟이 존재하고, 살아있다면(Hp > 0) 무조건 참(true)
        return target != null && target.currentHp > 0;
    }
}