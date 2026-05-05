using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TowerAttack : MonoBehaviour
{
    public int damage;
    public int range = 6;
    public float fireRate;

    bool isFacingRight;

    [SerializeField] private SpellSO spell;
    [SerializeField] private StatusEffectSO effectToApply;

    CircleCollider2D col;

    public PriorityQueue<Transform> targetList = new PriorityQueue<Transform>();

    public void Start()
    {
        spell.Cast(new Vector2(-4,-8));
    }

    private void Update()
    {
        Atack();
    }
    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.radius = range;
    }

    void Atack()
    {
        if (targetList.Count == 0) return;
        Rotate();
        Shoot(targetList.Peek());
    }
    void Rotate()
    {
        float distance = targetList.Peek().position.x - transform.position.x;
        if ((isFacingRight && distance < 0) || (!isFacingRight && distance > 0))
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
        }
    }

    void Shoot(Transform target)
    {

    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            targetList.Enqueue(collision.transform, 1000 - collision.transform.position.x);
            
        EffectHandler effect = collision.GetComponent<EffectHandler>();
        effect.AddEffect(effectToApply);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            targetList.Dequeue();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

