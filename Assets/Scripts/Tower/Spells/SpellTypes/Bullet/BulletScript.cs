using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector] public BulletSpellSO SO;
    [HideInInspector] public Transform target;
    [HideInInspector] public ObjectPooling pool;

    CircleCollider2D col;
    SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnCast()
    {
        if (col == null) col = GetComponent<CircleCollider2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        col.radius = SO.bulletRadius;
        spriteRenderer.sprite = SO.sprite;
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            pool.DespawnObject(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            SO.speed * Time.fixedDeltaTime);


        if(target.position == transform.position)
        {
            pool.DespawnObject(gameObject);
        }
        
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

        Enemy enemy = collision.GetComponent<Enemy>();
        EffectHandler effectHandler = collision.GetComponent<EffectHandler>();

        if (enemy == null || effectHandler == null) return;

        enemy.TakeDamage(SO.damage);

        foreach (var effect in SO.effects)
            effectHandler.AddEffect(effect);

        pool.DespawnObject(gameObject);
    }
}