using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TowerAttack : MonoBehaviour
{
    public int damage;
    public float range = 6f;
    //public float fireRate;

    bool isFacingRight;
    [SerializeField] float cooldown;
    float timer;

    CircleCollider2D col;
    [SerializeField] BulletSpellSO spell;

    

    public PriorityQueue<Transform> targetList = new PriorityQueue<Transform>();

    private void Update()
    {
        if(targetList.Count > 0)
        {
            if (timer >= cooldown)
            {
                timer = 0;
                Atack();
            }
        }
        timer += Time.deltaTime;
    }
    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.radius = range;

        timer = cooldown;

    }
    private void Start()
    {
    }

    void Atack()
    {
        if (targetList.Count == 0) return;
        Rotate();
        Debug.Log("cast");
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
        spell.Cast(new Vector2(transform.position.x,transform.position.y),target);
        Debug.Log("cast");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            targetList.Enqueue(collision.transform, 1000 - collision.transform.position.x);
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

