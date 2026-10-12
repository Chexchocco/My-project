using UnityEngine;

[CreateAssetMenu(fileName = "DebugAction", menuName = "AI/Actions/Debug Log")]
public class DebugAction : AIAction
{
    public override AIConfigType ConfigType => AIConfigType.Value;
    [Tooltip("콘솔창에 출력할 메시지")]
    public string message = "행동 실행!";

    protected override AIActionResult PerformAction(CharacterMain owner, CharacterMain target)
    {
        Debug.Log($"[{owner.name}] 님이 [{target.name}] 을(를) 향해: {message}");
        return AIActionResult.Success;
    }
} 
