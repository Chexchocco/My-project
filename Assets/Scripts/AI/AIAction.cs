using UnityEngine;

public abstract class AIAction : ScriptableObject
{
    public enum AIActionResult
    {
        Success,
        Failed,
        Running
    }
    public virtual AIConfigType ConfigType => AIConfigType.None;

    // 1. 매개변수 2개짜리 (기본형) - param이 필요 없는 행동들이 오버라이드함
     protected virtual AIActionResult PerformAction(
        CharacterMain owner,
        CharacterMain target)
    {
        return AIActionResult.Failed;
    }

    protected virtual AIActionResult PerformAction(
        CharacterMain owner,
        CharacterMain target,
        AIActionConfig actionConfig)
    {
        return PerformAction(owner, target);
    }

    public AIActionResult Execute(
        CharacterMain owner,
        AITargetSelector selector,
        AITargetConfig targetConfig,
        AIActionConfig actionConfig)
    {
        CharacterMain target =
            selector != null
            ? selector.GetTarget(owner, targetConfig)
            : owner;

        if (selector != null && target == null)
            return AIActionResult.Failed;

        return PerformAction(owner, target, actionConfig);
    }
}