using UnityEngine;

public class SplashScript : MonoBehaviour
{
    public SplashSpellSO SO;
    BoxCollider2D col;
    SpriteRenderer spriteRenderer;

    public void OnCast()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        spriteRenderer.sprite = SO.sprite;
        col.size = SO.coliderSize;
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
