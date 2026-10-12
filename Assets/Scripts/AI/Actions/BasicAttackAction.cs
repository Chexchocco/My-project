using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttack", menuName = "AI/Actions/Basic Attack")]
public class BasicAttackAction : AIAction
{
    public override AIConfigType ConfigType => AIConfigType.None;
    protected override AIActionResult PerformAction(CharacterMain owner, CharacterMain target)
    {
        // 테스트  
        if (target == null || target.currentHp <= 0)    return AIActionResult.Failed;
        float distance = Vector3.Distance(owner.transform.position, target.transform.position);
        if(distance > owner.attackRange) 
        {
            Debug.Log("공격 사정거리 밖");
            return AIActionResult.Failed;
        }
        
        Debug.Log("공격시도");
        //

        // 타겟이 없거나 이미 죽었으면 무시
        
        // 고유 ID 가져오기
        CharacterState characterState = owner.characterState;

        float  attackDelay = characterState.attackDelay;

        if (attackDelay <= 0.0f)
        {
            // 몸통(CharacterMain)에 있는 공격력을 가져와서 타겟에게 데미지를 입힘
            target.TakeDamage(owner.attackPower);
            
            Debug.Log($"[{owner.name}]가 [{target.name}]에게 데미지를 입혔습니다!");

            
            characterState.attackDelay = 1.0f;
            return AIActionResult.Success;
        }
        else
        {
            // 딜레이 대기중
            Debug.Log("공격 대기중");
            return AIActionResult.Running;
        }
    }
}