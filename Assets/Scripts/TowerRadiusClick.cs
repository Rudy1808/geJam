using UnityEngine;

public class TowerRadiusClick : MonoBehaviour
{
    private TowerRadiusDisplay display;

    private void Start()
    {
        display = GetComponentInParent<TowerRadiusDisplay>();
    }
    private void OnMouseDown()
    {
        Debug.Log("Elo brat");
        display?.ToggleRadii();
    }
}