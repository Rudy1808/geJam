using UnityEngine;

[CreateAssetMenu(fileName = "FreezingEffect", menuName = "Scriptable Objects/FreezingEffect")]
public class FreezingEffect : StatusEffectSO
{
    float speed;
    public override void OnApply(EffectHandler target)
    {
        speed = target.enemy.speed;
        target.enemy.speed = 0; 
    }

    public override void OnRemove(EffectHandler target)
    {
        target.enemy.speed = speed;
    }

    public override void OnTick(EffectHandler target, float deltaTime)
    {
        throw new System.NotImplementedException();
    }
}
