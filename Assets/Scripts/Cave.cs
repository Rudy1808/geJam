using UnityEngine;

public class Cave : MonoBehaviour
{
    public int hp;
    public static int money;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered");
        Debug.Log(collision);
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                hp -= enemy.attack;
            }
            enemy.Despawn();
        }
    }
}
