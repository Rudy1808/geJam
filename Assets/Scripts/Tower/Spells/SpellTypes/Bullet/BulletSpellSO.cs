using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletSpellSO", menuName = "Scriptable Objects/Spells/BulletSpellSO")]
public class BulletSpellSO : SpellSO
{
    public GameObject prefab;
    [HideInInspector] public Transform target;
    public int speed;
    public int bulletRadius;



    public override void Cast(Vector2 position, Transform target)
    {
        GameObject spell =Instantiate(prefab, new Vector3(position.x, position.y), Quaternion.identity);
        BulletScript script = spell.GetComponent<BulletScript>();
        this.target = target;
        script.SO = this;
        script.OnCast();
    }
   
}
