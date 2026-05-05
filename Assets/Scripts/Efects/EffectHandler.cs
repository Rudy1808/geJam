using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    [HideInInspector] public Enemy enemy;
    private readonly List<ActiveEffect> ListaKrzak = new List<ActiveEffect>();

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    } 

    private void Update()
    {
        if(ListaKrzak == null) return;

        for (int i = ListaKrzak.Count - 1; i >= 0; i--)
        {
            var effect = ListaKrzak[i];
            effect.Definition.OnTick(this, Time.deltaTime);

            effect.RemainingTime -= Time.deltaTime;
            if (effect.RemainingTime <= 0f)
                RemoveEffectAt(i);
        }
    }

    public void AddEffect(StatusEffectSO effect)
    {
        var statusEffect = new ActiveEffect(effect);
        for(int i = 0; i < ListaKrzak.Count; i++)
        {
            if (ListaKrzak[i].Definition == effect)
            {
                ListaKrzak[i].RemainingTime = effect.duration;
                return;
            }
        }

        ListaKrzak.Add(statusEffect);
        ListaKrzak[ListaKrzak.Count-1].Definition.OnApply(this);
    }

    public void RemoveEffectAt(int index)
    {
        var effect = ListaKrzak[index];
        effect.Definition.OnRemove(this);

        if (effect.VisualGO != null)
            Destroy(effect.VisualGO);

        ListaKrzak.RemoveAt(index);
    }
}
