using UnityEngine;

public class SplashScript : MonoBehaviour
{
    [HideInInspector]
    public SplashSpellSO SO;
    BoxCollider2D col;
    SpriteRenderer spriteRenderer;
    float timer = 0;

    public void OnCast()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        spriteRenderer.sprite = SO.sprite;
        col.size = SO.coliderSize;
    }
    private void Update()
    {
        if(timer >= SO.duration)
        {
            timer = 0;
            Die();
        }
        timer += Time.deltaTime;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            EffectHandler effectHandler =  collision.gameObject.GetComponent<EffectHandler>();

            if (enemy == null) return;
            if (effectHandler == null) return; 

            enemy.TakeDamage(SO.damage);
            

            foreach (var i in SO.effects)
            {
                effectHandler.AddEffect(i);
            }
        }
    }
}
