using UnityEngine;
using UnityEngine.InputSystem;

public class TowerClickManager : MonoBehaviour
{
    private Camera mainCam;
    private LayerMask towerRadiusLayer;

    void Start()
    {
        mainCam = Camera.main;
        towerRadiusLayer = LayerMask.GetMask("TowerRadius");
        //Debug.Log("LayerMask value: " + towerRadiusLayer.value);
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && !TowerPlacerManager.Instance.IsPlacing)
        {
            Vector2 mousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);

            bool hitTowerRadius = false;

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.layer != LayerMask.NameToLayer("TowerRadius"))
                    continue;
                var display = hit.collider.GetComponent<TowerRadiusDisplay>();
                if (display == null)
                    display = hit.collider.transform.parent?.GetComponent<TowerRadiusDisplay>();

                if (display != null)
                {
                    display.ToggleRadii();
                    break;
                }
            }
        }
    }
}