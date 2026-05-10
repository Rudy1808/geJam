using UnityEngine;
public class Enemy : MonoBehaviour
{
    [Header("Base Stats")]
    public int maxHp;
    private int _hp;
    public int Hp
    {
        get
        {
            return _hp;
        }
 
        set
        {
            _hp = Mathf.Clamp(value, 0, maxHp);
            if(_hp == 0)
            {
                Die();
            }
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

    private void Start()
    {
        _hp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        Hp-=damage;
    }

    void Die()
    {
        //Wodotryski
        Cave.Money += moneyReward;
        transform.parent.GetComponent<ObjectPooling>().DespawnObject(gameObject);
        //Debug.Log("die");

    }
    public void Despawn()
    {
        transform.parent.GetComponent<ObjectPooling>().DespawnObject(gameObject);
       // Debug.Log("despawn");
    }
}
