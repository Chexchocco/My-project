using UnityEngine;

// 1. 어떤 종류의 데이터를 쓸 건지 정의
public enum AIParamType
{
    FixedFloat,     // 고정된 숫자 (예: 5m)
    Stat_AttackRange, // 유닛의 공격 사거리 스탯 참조
    Stat_MaxHp,       // 유닛의 최대 체력 스탯 참조
    StringValue       // 문자열 (스킬 이름, 애니메이션 이름 등)
}
[System.Serializable]
public class AIParameter
{
    public AIParamType paramType;
    
    [Tooltip("체크하면 내 스탯이 아니라 타겟의 스탯을 가져옵니다.")]
    public bool useTargetStat; // ★ 추가됨

    public float floatValue;
    public string stringValue;

    // 인자로 target도 함께 받도록 수정
    public float GetFloatValue(CharacterMain owner, CharacterMain target)
    {
        // 체크박스 상태에 따라 참조할 주체(Subject)를 결정
        CharacterMain subject = useTargetStat ? target : owner;
        
        // 타겟의 스탯을 원했는데 타겟이 없는 경우 예외 처리
        if (subject == null) return 0f;

        switch (paramType)
        {
            case AIParamType.FixedFloat:
                return floatValue;
            case AIParamType.Stat_AttackRange:
                return subject.attackRange; // subject가 누구냐에 따라 내 사거리 or 적 사거리가 됨
            case AIParamType.Stat_MaxHp:
                return subject.maxHp;
            default:
                return 0f;
        }
    }
}