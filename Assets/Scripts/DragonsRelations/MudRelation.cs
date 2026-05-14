using System.Collections.Generic;
using UnityEngine;

public class MudRelation : MonoBehaviour
{
    public float range;
    [SerializeField] float cooldown;
    [SerializeField] SplashSpellSO spell;

    float timer;
    bool isFacingRight;
    CircleCollider2D col;

    public List<Transform> targetList = new();

    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.radius = range;
        timer = cooldown;

        spell.pool = GetComponent<ObjectPooling>();
    }

    private void Update()
    {
        if (timer < cooldown)
        {
            timer += Time.deltaTime;
            return;
        }

        Transform target = GetTarget();
        if (target == null) return;

        timer = 0;
        Rotate(target);
        spell.Cast(target.transform.position);
    }

    Transform GetTarget()
    {
        targetList.RemoveAll(x => x == null);
        if (targetList.Count == 0) return null;

        targetList.Sort((a, b) => a.position.x.CompareTo(b.position.x));
        return targetList[targetList.Count - 1];
    }

    void Rotate(Transform target)
    {
        float distance = target.position.x - transform.position.x;

        if ((isFacingRight && distance < 0) || (!isFacingRight && distance > 0))
        {
            isFacingRight = !isFacingRight;
            Vector3 s = transform.localScale;
            transform.localScale = new Vector3(s.x * -1, s.y, s.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
        if (!targetList.Contains(collision.transform))
            targetList.Add(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
        targetList.Remove(collision.transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
