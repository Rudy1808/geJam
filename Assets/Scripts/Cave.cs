using System;
using UnityEngine;

public class Cave : MonoBehaviour
{
    public static int _money = 150;
    public static event Action<int> OnMoneyChanged;
    public static int Money
    {
        get => _money;
        set
        {
            _money = value;
            OnMoneyChanged?.Invoke(value);
            //StatsController.SetMoney(value);
        }
    }
    public static int _hp = 50;
    public static event Action<int> OnHPChanged;
    public static int HP
    {
        get => _hp;
        set
        {
            _hp = value;
            OnHPChanged?.Invoke(value);
            //StatsController.SetMoney(value);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Entered");
        //Debug.Log(collision);
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                HP -= enemy.attack;
                enemy.Despawn();
            }
        }
    }
}
