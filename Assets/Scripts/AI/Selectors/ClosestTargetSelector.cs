using UnityEngine;

[CreateAssetMenu(fileName = "ClosestTarget", menuName = "AI/Selectors/Closest Target")]
public class ClosestTargetSelector : AITargetSelector
{
    //[Tooltip("적을 탐색할 반경 거리")]
    //public float searchRadius = 10f;
    public override AIConfigType ConfigType => AIConfigType.Value;
    
    [Header("Default Settings")]
    public float defaultSearchRadius = 10f;
    [Tooltip("어떤 레이어(Layer)를 적으로 인식할지 설정")]
    public LayerMask defaultTargetLayer;


    public override CharacterMain GetTarget(CharacterMain owner,  AITargetConfig config)
    {

        float searchRadius = defaultSearchRadius;
        LayerMask targetLayer = defaultTargetLayer;
        
        if (config != null)
        {
            if (config.overrideSearchRadius)
                searchRadius = config.searchRadius;

            if (config.overrideTargetLayer)
                targetLayer = config.targetLayer;
        }


        Collider[] hits = Physics.OverlapSphere(owner.transform.position, searchRadius, targetLayer);
        
        CharacterMain closestTarget = null;
        float minDistance = float.MaxValue;

        // 2. 찾은 대상들을 하나씩 검사합니다.
        foreach (var hit in hits)
        {
            // 자기 자신은 타겟에서 제외
            if (hit.gameObject == owner.gameObject) continue;

            CharacterMain targetMain = hit.GetComponent<CharacterMain>();
            
            // CharacterMain이 있고, 체력이 남아있는(살아있는) 대상만 취급
            if (targetMain != null && targetMain.currentHp > 0)
            {
                // 나와 대상 사이의 3D 거리 계산
                float dist = Vector3.Distance(owner.transform.position, targetMain.transform.position);
                
                // 더 가까운 적을 발견하면 타겟을 갱신
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closestTarget = targetMain;
                }
            }
        }

        // 가장 가까운 타겟을 반환 (없으면 null 반환)
        return closestTarget;
    }
}