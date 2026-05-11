using UnityEngine;

public abstract class StatusEffectSO : ScriptableObject
{
    public string effectName;
    public float duration;

    public abstract void OnApply(EffectHandler target);
    public abstract void OnTick(EffectHandler target,float deltaTime);

    public abstract void OnRemove(EffectHandler target);

}
