using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Image healthFill;

    private CharacterMain target;

    public void Bind(CharacterMain character)
    {
        // 이전 대상이 있다면 이벤트 연결 해제
        if (target != null)
            target.OnHealthChanged -= UpdateHealth;

        // 새 대상 연결
        target = character;

        // 체력 변화 이벤트 구독
        target.OnHealthChanged += UpdateHealth;

        // 최초 체력 표시
        UpdateHealth(target.GetCurrentHP(), target.GetMaxHP());
    }

    private void UpdateHealth(float current, float max)
    {
        Debug.Log($"hp 수정.");
        healthFill.fillAmount = max > 0f
            ? Mathf.Clamp01(current / max)
            : 0f;
    }

    private void OnDestroy()
    {
        if (target != null)
            target.OnHealthChanged -= UpdateHealth;
    }
}