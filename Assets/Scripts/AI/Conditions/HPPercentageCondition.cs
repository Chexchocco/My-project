using UnityEngine;

[CreateAssetMenu(fileName = "HPCondition", menuName = "AI/Conditions/HP Percentage")]
public class HPPercentageCondition : AICondition
{
    [Tooltip("이 비율(0.0~1.0) 이하일 때 조건이 참(true)이 됩니다. (예: 0.3 = 30%)")]
    public override AIConfigType ConfigType => AIConfigType.Value;
    public float threshold = 0.3f;
    protected override bool CheckCondition(CharacterMain owner, CharacterMain target,  AIConditionConfig config)
    {
        // 내용은 건드릴 필요 없이 그대로 둡니다. (원한다면 나중에 threshold 대신 param을 쓰게 바꿀 수 있습니다)
        return target.GetHPPercentage() <= threshold;
    }

    
}
