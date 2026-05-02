using System.Collections.Generic;
using UnityEngine;

public abstract class SpellSO : ScriptableObject
{
    public int damage;
    public List<StatusEffectSO> effects;

    public abstract void Cast(Vector2 position);


}
