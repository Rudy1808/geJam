using UnityEngine;


public class Tower : MonoBehaviour
{
    [SerializeField] private TowerUpgradeStats[] levels;
    int currentLevel = 0;
    public float occupatedSpaceRadius = 2f;

    public TowerUpgradeStats CurrentData => levels[currentLevel];

    public void Upgrade()
    {
        if (currentLevel > levels.Length) return;
        currentLevel++;
        ApplyLevel();

    }

    private void ApplyLevel()
    {
        //Tower Atack
        GetComponent<TowerAttack>().damage = CurrentData.damage;
        GetComponent<TowerAttack>().range = CurrentData.range;
        GetComponent<TowerAttack>().fireRate = CurrentData.fireRate;
    }
}