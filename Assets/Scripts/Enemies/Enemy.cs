using UnityEngine;
public class Enemy : MonoBehaviour
{
    [Header("Base Stats")]
    public int maxHp;
    private int _hp;
    public int hp
    {
        get
        {
            return _hp;
        }

        set
        {
            _hp = Mathf.Clamp(_hp, 0, maxHp);

            if(_hp == 0)
            {
                Die();
            }
            value = _hp;
        }
    }


    public int attack;
    public float speed;
    public int moneyReward;
    public EnemyType enemyType;
    public Animation animationMove;

    [Header("Armor Effect")]
    public int fireArmor;
    public int waterArmor;
    public int airArmor;
    public int earthArmor;
    
    void TakeDamage(int damage)
    {
        hp-=damage;
    }

    void Die()
    {
        //Wodotryski
        Cave.money += moneyReward;
        transform.parent.GetComponent<ObjectPooling>().DespawnObject(gameObject);

    }
    public void Despawn()
    {
        transform.parent.GetComponent<ObjectPooling>().DespawnObject(gameObject);
    }
}
