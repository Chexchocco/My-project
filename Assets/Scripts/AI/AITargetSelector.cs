using UnityEngine;

// 타겟을 찾는 모든 부품의 뼈대입니다.
public abstract class AITargetSelector : ScriptableObject
{
    // 나 자신(owner)을 기준으로 목표물(CharacterMain)을 찾아서 반환합니다.
    public virtual AIConfigType ConfigType => AIConfigType.None;
    
    public abstract CharacterMain GetTarget(CharacterMain owner, AITargetConfig config);
}