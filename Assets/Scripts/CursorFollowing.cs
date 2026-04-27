using UnityEngine;
using UnityEngine.InputSystem;

public class CursorFollowing : MonoBehaviour
{
    public GameObject towerPrefab;
    public Transform cursorObject;

    private Camera mainCam;
    private SpriteRenderer cursorRenderer;

    void Start()
    {
        mainCam = Camera.main;
        Cursor.visible = false;

        cursorRenderer = cursorObject.GetComponent<SpriteRenderer>();

        Color c = cursorRenderer.color;
        c.a = 0.5f;
        cursorRenderer.color = c;
    }

    void Update()
    {
        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;

        cursorObject.position = mouseWorld;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameObject placed = Instantiate(towerPrefab, mouseWorld, Quaternion.identity);

            SpriteRenderer placedRenderer = placed.GetComponent<SpriteRenderer>();
            Color c = placedRenderer.color;
            c.a = 1f;
            placedRenderer.color = c;
        }
    }

    void OnDestroy()
    {
        Cursor.visible = true;
    }
}