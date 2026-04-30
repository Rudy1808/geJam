using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    public Enemy enemy;
    private readonly List<ActiveEffect> _activeEffects;

    private void Awake() => enemy = GetComponent<Enemy>();

    private void Update()
    {
        for (int i = _activeEffects.Count - 1; i >= 0; i--)
        {
            var effect = _activeEffects[i];
            effect.Definition.OnTick(this, Time.deltaTime);

            effect.RemainingTime -= Time.deltaTime;
            if (effect.RemainingTime <= 0f)
                RemoveEffectAt(i);
        }
    }

    public void AddEffect(StatusEffectSO effect)
    {
        var StatuEffect = new ActiveEffect(effect);
        _activeEffects.Add(StatuEffect);
        effect.OnApply(this);
    }

    public void RemoveEffectAt(int index)
    {
        var effect = _activeEffects[index];
        effect.Definition.OnRemove(this);

        if (effect.VisualGO != null)
            Destroy(effect.VisualGO);

        _activeEffects.RemoveAt(index);
    }
}
