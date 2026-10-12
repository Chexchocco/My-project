using UnityEngine;

[CreateAssetMenu(fileName = "MoveToTarget", menuName = "AI/Actions/Move To Target")]
public class MoveToTargetAction : AIAction
{
    /*
    [Tooltip("타겟에게 얼마나 가까이 다가갈 것인가 (정지 거리)")]
    public float stoppingDistance = 1.5f;
    */
    public override AIConfigType ConfigType => AIConfigType.None;
    protected override AIActionResult PerformAction(CharacterMain owner, CharacterMain target)
    {
        // 1. 타겟이 없으면 이동 취소
        if (target == null) return AIActionResult.Failed;

        Vector3 currentPos = owner.transform.position;
        Vector3 targetPos = target.transform.position;
        float distance = Vector3.Distance(currentPos, targetPos);

         // 타겟을 향한 방향(Direction) 계산
        Vector3 direction = (targetPos - currentPos).normalized;
        
        // CharacterMain에 있는 moveSpeed를 사용하여 이동
        owner.transform.position += direction * owner.moveSpeed * Time.deltaTime;
        
        // 타겟을 바라보도록 회전 (y축 기준)
        owner.transform.LookAt(new Vector3(targetPos.x, currentPos.y, targetPos.z));
        
        return AIActionResult.Running;
    
    }
}