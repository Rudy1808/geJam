using System;
using TMPro;
using UnityEngine;

public class StatsView : MonoBehaviour
{
    [SerializeField]TextMeshPro hp;
    [SerializeField]TextMeshPro money;

    private void Awake()
    {
        if(hp == null && money == null)
        {
            Debug.LogError("B??d UI");
        }
    }

    public void refresh(StatsModel model)
    {
        hp.text = Convert.ToString(model.hp);
        money.text = Convert.ToString(model.money);
    }


}
