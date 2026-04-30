using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorFollowing : MonoBehaviour
{
    public GameObject towerPrefab;
    public LayerMask towerRadiusLayer;

    private float towerRadiusValue = 3.5f;
    private float attackRadiusValue = 6f;

    private Camera mainCam;
    private GameObject cursorInstance;
    private Transform cursorObject;
    private Transform towerRadius;
    private Transform attackRadius;
    private SpriteRenderer cursorRenderer;
    private SpriteRenderer towerRadiusRenderer;
    private SpriteRenderer attackRadiusRenderer;
    private CircleCollider2D previewCollider;

    private bool canPlace = true;
    private bool isPlacing = false;

    void Start()
    {
        mainCam = Camera.main;

        cursorInstance = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity);
        cursorObject = cursorInstance.transform;
        towerRadius = cursorObject.Find("towerRadius");
        attackRadius = cursorObject.Find("attackRadius");

        cursorRenderer = cursorObject.GetComponent<SpriteRenderer>();
        towerRadiusRenderer = towerRadius.GetComponent<SpriteRenderer>();
        attackRadiusRenderer = attackRadius.GetComponent<SpriteRenderer>();
        previewCollider = towerRadius.GetComponentInChildren<CircleCollider2D>();
        towerRadiusLayer = LayerMask.GetMask("TowerRadius");

        cursorObject.localScale = new Vector3(1f, 1f, 1f);
        towerRadius.localScale = new Vector3(towerRadiusValue, towerRadiusValue, 1f);
        attackRadius.localScale = new Vector3(attackRadiusValue, attackRadiusValue, 1f);

        cursorInstance.SetActive(false);

        GetComponent<Button>().onClick.AddListener(OnImageClicked);
    }

    void OnImageClicked()
    {
        isPlacing = true;
        cursorInstance.SetActive(true);
        Cursor.visible = false;

        SetAlpha(cursorRenderer, 0.8f);
        SetAlpha(towerRadiusRenderer, 0.7f);
        SetAlpha(attackRadiusRenderer, 0.25f);
    }

    void Update()
    {
        if (!isPlacing) return;

        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;

        cursorInstance.transform.position = mouseWorld;

        CheckCanPlace(mouseWorld);

        towerRadiusRenderer.color = canPlace
            ? new Color(0.2f, 1f, 0.2f, towerRadiusRenderer.color.a)
            : new Color(1f, 0.2f, 0.2f, towerRadiusRenderer.color.a);

        if (Mouse.current.leftButton.wasPressedThisFrame && canPlace)
        {
            GameObject placed = Instantiate(towerPrefab, mouseWorld, Quaternion.identity);

            // Pobierz komponenty postawionej wieży
            SpriteRenderer placedRenderer = placed.GetComponent<SpriteRenderer>();
            Transform placedTowerRadius = placed.transform.Find("towerRadius");
            Transform placedAttackRadius = placed.transform.Find("attackRadius");

            // Smok 100% widoczny
            SetAlpha(placedRenderer, 1f);

            // Okręgi niewidoczne
            SetAlpha(placedTowerRadius.GetComponent<SpriteRenderer>(), 0f);
            SetAlpha(placedAttackRadius.GetComponent<SpriteRenderer>(), 0f);

            isPlacing = false;
            cursorInstance.SetActive(false);
            Cursor.visible = true;
        }
    }

    void CheckCanPlace(Vector3 position)
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(towerRadiusLayer);
        filter.useTriggers = true;

        List<Collider2D> hits = new List<Collider2D>();
        previewCollider.Overlap(filter, hits);
        hits.Remove(previewCollider);

        canPlace = hits.Count == 0;
    }

    void SetAlpha(SpriteRenderer rend, float alpha)
    {
        if (rend == null) return;
        Color c = rend.color;
        c.a = alpha;
        rend.color = c;
    }
}