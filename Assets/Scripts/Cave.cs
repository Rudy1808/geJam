using UnityEngine;

public class Cave : MonoBehaviour
{
    public int hp;
    public int money;
    public int wave;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                hp -= enemy.attack;
            }
            enemy.Despawn();
        }
    }
}
