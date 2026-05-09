using UnityEditor.Build;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector] public BulletSpellSO SO;
    CircleCollider2D Col;
    SpriteRenderer spriteRenderer;
    public void OnCast()
    {
        Col = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Col.radius = SO.bulletRadius;
        spriteRenderer.sprite = SO.sprite;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(
        transform.position,
        SO.target.position,
        SO.speed);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            EffectHandler effectHandler = collision.gameObject.GetComponent<EffectHandler>();

            if (enemy == null) return;
            if (effectHandler == null) return;

            enemy.TakeDamage(SO.damage);


            foreach (var i in SO.effects)
            {
                effectHandler.AddEffect(i);
            }

            Destroy(this);
        }
    }


}
