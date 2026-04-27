using UnityEngine;

public class Cave : MonoBehaviour
{
    public int hp = 100;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Try to get attack value from enemy component
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                hp -= enemy.attack;
                Debug.Log($"Cave took {enemy.attack} damage! HP: {hp}");
            }
        }
    }
}
