using UnityEngine;

public abstract class AICondition : ScriptableObject
{
    public virtual AIConfigType ConfigType => AIConfigType.None;
    // 1. 매개변수 2개짜리 (기본형) - 자식들이 기본적으로 오버라이드 할 수 있음
    protected virtual bool CheckCondition(CharacterMain owner, CharacterMain target)
    {
        return false; 
    }

    // 2. 매개변수 3개짜리 (확장형) - param이 필요할 때만 오버라이드 함
    protected virtual bool CheckCondition(
        CharacterMain owner,
        CharacterMain target,
        AIConditionConfig config)
    {
        return CheckCondition(owner, target);
    }


    public bool CanExecute(
    CharacterMain owner,
    AITargetSelector selector,
    AITargetConfig targetConfig,
    AIConditionConfig config)
    {
        CharacterMain target =
            selector != null
            ? selector.GetTarget(owner, targetConfig)
            : owner;

        if (selector != null && target == null)
            return false;

        return CheckCondition(owner, target, config);
    }
}