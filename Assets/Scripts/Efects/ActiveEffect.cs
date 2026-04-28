using UnityEngine;

public class ActiveEffect
{
    public StatusEffectSO Definition { get; }
    public float RemainingTime { get; set; }
    public GameObject VisualGO { get; set; }

    public ActiveEffect(StatusEffectSO definition)
    {
        Definition = definition;
        RemainingTime = definition.duration;
    }
}