using UnityEngine;

// 이 속성을 달면 유니티 프로젝트 창에서 우클릭 [Create -> AI -> Selectors -> Self]로 생성할 수 있습니다.
[CreateAssetMenu(fileName = "SelfSelector", menuName = "AI/Selectors/Self")]
public class SelfSelector : AITargetSelector
{
    public override AIConfigType ConfigType => AIConfigType.Value;
    public override CharacterMain GetTarget(CharacterMain owner, AITargetConfig config
)
    {
        // 타겟은 나 자신
        return owner;
    }
}
