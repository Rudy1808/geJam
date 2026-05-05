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

    CircleCollider2D col;

    public PriorityQueue<Transform> targetList = new PriorityQueue<Transform>();

    private void Update()
    {
        Atack();
        Debug.Log(targetList.Count);
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
        Debug.Log("Strza³");
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

