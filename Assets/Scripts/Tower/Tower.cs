using UnityEngine;


public class Tower : MonoBehaviour
{
    [SerializeField] private TowerUpgradeStats[] levels;
    int currentLevel = 0;
    [SerializeField] public int cost;

    public TowerUpgradeStats CurrentData => levels[currentLevel];

    public void Upgrade()
    {
        if (currentLevel > levels.Length) return;
        currentLevel++;
        ApplyLevel();

    }

    private void ApplyLevel()
    {
    }
}