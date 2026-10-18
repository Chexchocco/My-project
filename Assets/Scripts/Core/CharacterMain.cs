using System;
using UnityEngine;
public class CharacterMain : MonoBehaviour
{
    [Header("Components")]
    public CharacterState characterState;
    public CharacterAI characeterAI;
    
    
    [Header("Base Stats")]
    public float maxHp = 100f;
    public float currentHp;
    public float moveSpeed = 5f;
    public float attackPower = 10f;
    public float attackSpeed = 1.0f; // 초당공격회수로 가자 , 쿨다운은 그러면 역수로
    public float attackRange = 5.0f; // 공격 사정거리

    [Header("Cached Components")]
    public Rigidbody rb;
    public Animator anim;


    public event Action<float, float> OnHealthChanged;


    private void Awake()
    {
        // 1. 시작할 때 체력을 최대로 채워줍니다.
        currentHp = maxHp;

        // 2. 컴포넌트 자동 할당 (인스펙터에서 안 넣었을 경우를 대비)
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (anim == null) anim = GetComponent<Animator>();

        // 부속품할당
        if (characterState == null) characterState = GetComponent<CharacterState>();
        if (characeterAI == null) characeterAI = GetComponent<CharacterAI>();


    }

    #region AI가 읽어갈 정보들 (Read-Only)
    
    public float GetCurrentHP() => currentHp;
    public float GetMaxHP() => maxHp;
    
    // 체력 비율 (0.0 ~ 1.0) 반환 - "체력이 30% 이하인가?" 조건 체크할 때 유용합니다.
    public float GetHPPercentage() => maxHp == 0f ? 0f : currentHp / maxHp;
    
    #endregion

    #region 외부에서 호출할 상호작용 (Actions)

    private void Update()
    {   // 프레임 별 업데이트

        // 임시
        if (characterState.attackDelay > 0f)
        {
            characterState.attackDelay -= attackSpeed * Time.deltaTime;
        }
    }


    // 피격 로직
    
    public void TakeDamage(float amount)
    {
        if (amount <= 0f || currentHp <= 0f)
            return;

        currentHp = Mathf.Max(0f, currentHp - amount);

        // HP가 변경되었음을 외부에 알림
        OnHealthChanged?.Invoke(currentHp, maxHp);

        if (currentHp <= 0f)
            Die();
    }

    private void Die()
    {
        // 임시 사망 처리
        Debug.Log($"{gameObject.name}가(이) 사망했습니다.");
        gameObject.SetActive(false); 
    }

    #endregion
}