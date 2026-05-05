using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorFollowing : MonoBehaviour
{
    public GameObject towerPrefab;
    private LayerMask towerRadiusLayer;

    [SerializeField] private float towerRadiusValue;
    private float attackRadiusValue;
    [SerializeField] private float towerRadiusOffsetY;
    [SerializeField] private float attackRadiusOffsetY;

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

    private int cost;

    void Start()
    {
        mainCam = Camera.main;
        attackRadiusValue = towerPrefab.GetComponent<TowerAttack>().range;
        cost = towerPrefab.GetComponent<Tower>().cost;
        Debug.Log(cost);
        towerRadiusLayer = LayerMask.NameToLayer("TowerRadius");
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

        towerRadius.localPosition = new Vector3(0f, towerRadiusOffsetY, 0f);
        attackRadius.localPosition = new Vector3(0f, attackRadiusOffsetY, 0f);

        cursorInstance.SetActive(false);

        GetComponent<Button>().onClick.AddListener(OnImageClicked);
    }

    private static CursorFollowing activePlacer = null;

    void OnImageClicked()
    {
        if (activePlacer == this)
        {
            StopPlacing();
            return;
        }

        if (activePlacer != null)
        {
            activePlacer.StopPlacing();
        }

        activePlacer = this;
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

        if (Keyboard.current.escapeKey.wasPressedThisFrame && isPlacing)
        {
            StopPlacing();
        }
        if (Mouse.current.leftButton.wasPressedThisFrame && canPlace)
        {
            if (Cave.Money - cost >= 0)
                Cave.Money -= cost;
            else
            {
                //coś
            }
            Debug.Log(Cave.Money);

            GameObject placed = Instantiate(towerPrefab, mouseWorld, Quaternion.identity);

            SpriteRenderer placedRenderer = placed.GetComponent<SpriteRenderer>();
            Transform placedTowerRadius = placed.transform.Find("towerRadius");
            Transform placedAttackRadius = placed.transform.Find("attackRadius");

            SetAlpha(placedRenderer, 1f);
            SetAlpha(placedTowerRadius.GetComponent<SpriteRenderer>(), 0f);
            SetAlpha(placedAttackRadius.GetComponent<SpriteRenderer>(), 0f);


            isPlacing = false;
            cursorInstance.SetActive(false);
            Cursor.visible = true;
            activePlacer = null;
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

    void StopPlacing()
    {
        isPlacing = false;
        cursorInstance.SetActive(false);
        Cursor.visible = true;

        if (activePlacer == this)
            activePlacer = null;
    }

    void SetAlpha(SpriteRenderer rend, float alpha)
    {
        if (rend == null) return;
        Color c = rend.color;
        c.a = alpha;
        rend.color = c;
    }
}