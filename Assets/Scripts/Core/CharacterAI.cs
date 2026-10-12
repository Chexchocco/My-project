using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIBehaviorRule
{
    [Header("조건부")]
    public AITargetSelector conditionTarget;
    public AITargetConfig conditionTargetConfig;
    public AICondition condition;
    public AIConditionConfig conditionConfig;

    [Header("행동부")]
    public AITargetSelector actionTarget;
    public AITargetConfig actionTargetConfig;

    public AIAction action;
    public AIActionConfig actionConfig;
}   
public class CharacterAI : MonoBehaviour
{
    public CharacterMain owner;
    public List<AIBehaviorRule> rules = new List<AIBehaviorRule>();

    private void Awake()
    {
        if (owner == null) owner = GetComponent<CharacterMain>();
    }

    private void Update()
    {
        if (owner == null || owner.currentHp <= 0) return;

        foreach (var rule in rules)
        {
            if (rule.condition == null || rule.action == null) continue;
            
            
            Debug.Log("조건 탐색");
            // 조건 타겟 탐색기를 넘겨서 체크
            if (rule.condition.CanExecute(owner, rule.conditionTarget, rule.conditionTargetConfig, rule.conditionConfig))
            {
                Debug.Log("행동 되는지 체크");
                if(rule.action.Execute(owner, rule.actionTarget,rule.actionTargetConfig,rule.actionConfig) == AIAction.AIActionResult.Failed)
                {
                    
                    Debug.Log("행동 이행 불가 다음 규칙탐색");
                    // 행동 실패 -> 다음 규칙을 위해 continue?
                    continue;
                }
                
                    Debug.Log("행동 이행");
                // 행동 중 혹은 성공 ㅇㅋ
                break; 
            }
            Debug.Log("조건 아행 불가 다음조건");
        }
    }
}