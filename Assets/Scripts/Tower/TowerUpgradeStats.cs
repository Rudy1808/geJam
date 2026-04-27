using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerUpgradeStats", menuName = "Scriptable Objects/TowerUpgradeStats")]
public class TowerUpgradeStats : ScriptableObject
{
    [Header("Stats")]
    public int damage;
    public int range;
    public int cost;
    public float fireRate;
    public Sprite sprite;
    [SerializeField] public GameObject Prefab;
}
