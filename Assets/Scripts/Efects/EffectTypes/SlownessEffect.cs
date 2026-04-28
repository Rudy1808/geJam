using UnityEngine;

[CreateAssetMenu(fileName = "SlownessEffect", menuName = "Scriptable Objects/SlownessEffect")]
public class SlownessEffect : StatusEffectSO
{
    [Range(0f, 1f)] public float slowPercent = 0.5f;
    float speed;

    public override void OnApply(EffectHandler target)
    {
        speed = target.enemy.speed;
        target.enemy.speed = target.enemy.speed * (1f-slowPercent);

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
