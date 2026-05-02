using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SplashSpellSO", menuName = "Scriptable Objects/Spells/SplashSpellSO")]
public class SplashSpellSO : SpellSO
{
    public int duration;
    public GameObject prefab;
public override void Cast(Vector2 postion)
    {
       Instantiate(prefab,new Vector3(postion.x,postion.y,0),Quaternion.identity);
    }
}
