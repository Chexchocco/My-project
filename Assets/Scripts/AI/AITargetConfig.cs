using UnityEngine;

[System.Serializable]
public class AITargetConfig
{
    public bool overrideSearchRadius;
    public float searchRadius;

    public bool overrideTargetLayer;
    public LayerMask targetLayer;
}