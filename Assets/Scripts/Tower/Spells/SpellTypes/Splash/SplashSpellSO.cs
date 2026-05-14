using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SplashSpellSO", menuName = "Scriptable Objects/Spells/SplashSpellSO")]
public class SplashSpellSO : SpellSO
{
    public bool isDurable;
    public int duration;
    public Vector2 coliderSize;
    public GameObject prefab;
    [HideInInspector] public ObjectPooling pool;
    public override void Cast(Vector2 postion, Transform transform=null)
    {
        GameObject spellObject = Instantiate(prefab,new Vector3(postion.x,postion.y,0),Quaternion.identity);
        SplashScript script = spellObject.GetComponent<SplashScript>();
        script.SO = this;
        script.pool = pool;
        script.OnCast();
    }
}
