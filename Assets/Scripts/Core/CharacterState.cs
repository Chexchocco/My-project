using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [Header("Action State")]
    public float attackDelay { get; set; }

    public bool isAttacking { get; set; }
    public bool isCasting { get; set; }

    [Header("Target State")]
    public CharacterMain currentTarget { get; set; }


}