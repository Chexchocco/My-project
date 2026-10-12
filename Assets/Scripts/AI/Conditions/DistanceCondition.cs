using UnityEngine;

// DistanceCondition.cs
[CreateAssetMenu(fileName = "DistanceCondition", menuName = "AI/Conditions/Distance")]
public class DistanceCondition : AICondition
{
    public override AIConfigType ConfigType => AIConfigType.Comparison;
   
    protected override bool CheckCondition(CharacterMain owner, CharacterMain target,  AIConditionConfig conditionConfig)
    {
        
        Debug.Log("거리 조건 탐색 시작");
        if (target == null) return false;
        float distance = Vector3.Distance(owner.transform.position, target.transform.position);
        
        // param.GetFloatValue()를 호출하면, 인스펙터 설정에 따라
        // 유저가 입력한 '5'가 나올 수도 있고, 내 유닛의 'attackRange' 값이 나올 수도 있습니다!
        float compareDistance = conditionConfig.valueParam.GetFloatValue(owner, target);
        
        switch (conditionConfig.compareType)
        {
            case CompareType.Less:
                Debug.Log("less");
                return distance < compareDistance;

            case CompareType.LessOrEqual:
                Debug.Log("less or equal");
                return distance <= compareDistance;

            case CompareType.Greater:
            
                Debug.Log("Greater");
                return distance > compareDistance;

            case CompareType.GreaterOrEqual:
            
                Debug.Log("Greater or equal");
                return distance >= compareDistance;
        }
        Debug.Log("거리 조건 오류");
        return false;
    }
}