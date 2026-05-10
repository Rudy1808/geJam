using UnityEngine;

[CreateAssetMenu(fileName = "BulletSpellSO", menuName = "Scriptable Objects/Spells/BulletSpellSO")]
public class BulletSpellSO : SpellSO
{
    public int speed;
    public int bulletRadius;
    [HideInInspector] public ObjectPooling pool;

    public override void Cast(Vector2 position, Transform target)
    {
        GameObject bullet = pool.SpawnObject(position);
        BulletScript script = bullet.GetComponent<BulletScript>();
        script.SO = this;
        script.target = target;
        script.pool = pool;
        script.OnCast();
    }
}