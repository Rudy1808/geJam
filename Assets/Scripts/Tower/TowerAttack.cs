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

public class PriorityQueue<T>
{
    private List<(T item, float priority)> heap = new List<(T, float)>();

    public int Count => heap.Count;

    public void Enqueue(T item, float priority)
    {
        heap.Add((item, priority));
        HeapifyUp(heap.Count - 1);
    }

    public T Dequeue()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Queue is empty");

        T root = heap[0].item;

        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        HeapifyDown(0);

        return root;
    }

    public T Peek()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Queue is empty");

        return heap[0].item;
    }

    private void HeapifyUp(int i)
    {
        while (i > 0)
        {
            int parent = (i - 1) / 2;

            if (heap[i].priority >= heap[parent].priority)
                break;

            Swap(i, parent);
            i = parent;
        }
    }

    private void HeapifyDown(int i)
    {
        int lastIndex = heap.Count - 1;

        while (true)
        {
            int left = i * 2 + 1;
            int right = i * 2 + 2;
            int smallest = i;

            if (left <= lastIndex && heap[left].priority < heap[smallest].priority)
                smallest = left;

            if (right <= lastIndex && heap[right].priority < heap[smallest].priority)
                smallest = right;

            if (smallest == i)
                break;

            Swap(i, smallest);
            i = smallest;
        }
    }

    private void Swap(int a, int b)
    {
        (heap[a], heap[b]) = (heap[b], heap[a]);
    }
}