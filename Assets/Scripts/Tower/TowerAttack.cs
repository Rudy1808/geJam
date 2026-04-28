using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public int damage;
    public int range;
    public float fireRate;

    public Queue<Transform> targetQueue = new Queue<Transform>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.LayerToName("Name"))
    }
}
