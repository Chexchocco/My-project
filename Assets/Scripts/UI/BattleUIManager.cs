using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private HealthBarView healthBarPrefab;
    [SerializeField] private RectTransform healthBarContainer;

    private Dictionary<CharacterMain, HealthBarView> healthBars
        = new Dictionary<CharacterMain, HealthBarView>();

    [SerializeField] private CharacterMain testCharacter;

    private void Start()
    {
        RegisterCharacter(testCharacter);
    }

    public void RegisterCharacter(CharacterMain character)
    {
        if (character == null || healthBars.ContainsKey(character))
            return;

        HealthBarView bar =
            Instantiate(healthBarPrefab, healthBarContainer);

        bar.Bind(character);

        healthBars.Add(character, bar);
    }

    public void UnregisterCharacter(CharacterMain character)
    {
        if (!healthBars.TryGetValue(character, out HealthBarView bar))
            return;

        healthBars.Remove(character);

        if (bar != null)
            Destroy(bar.gameObject);
    }
}