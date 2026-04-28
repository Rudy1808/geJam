//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class CursorFollowing : MonoBehaviour
//{
//    public GameObject towerPrefab;
//    public Transform cursorObject;

//    public GameObject towerRadiusPrefab;
//    public Transform towerRadius;
//    private float towerRadiusValue = 3.5f;

//    public GameObject attackRadiusPrefab;
//    public Transform attackRadius;
//    private float attackRadiusValue = 6f;

//    public LayerMask towerRadiusLayer;

//    private Camera mainCam;
//    private SpriteRenderer cursorRenderer;
//    private SpriteRenderer towerRadiusRenderer;
//    private SpriteRenderer attackRadiusRenderer;
//    private CircleCollider2D previewCollider;

//    private bool canPlace = true;

//    void Start()
//    {
//        mainCam = Camera.main;
//        Cursor.visible = false;

//        cursorRenderer = cursorObject.GetComponent<SpriteRenderer>();
//        towerRadiusRenderer = towerRadius.GetComponent<SpriteRenderer>();
//        attackRadiusRenderer = attackRadius.GetComponent<SpriteRenderer>();

//        SetAlpha(cursorRenderer, 0.8f);
//        SetAlpha(towerRadiusRenderer, 0.7f);
//        SetAlpha(attackRadiusRenderer, 0.25f);

//        previewCollider = towerRadius.GetComponent<CircleCollider2D>();
//        towerRadiusLayer = LayerMask.GetMask("TowerRadius");

//        towerRadius.localScale = new Vector3(towerRadiusValue, towerRadiusValue, 1f);
//        attackRadius.localScale = new Vector3(attackRadiusValue, attackRadiusValue, 1f);
//    }

//    void Update()
//    {
//        Vector2 mouseScreen = Mouse.current.position.ReadValue();
//        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(mouseScreen);
//        mouseWorld.z = 0f;

//        cursorObject.position = mouseWorld;
//        towerRadius.position = mouseWorld;
//        attackRadius.position = mouseWorld;

//        CheckCanPlace(mouseWorld);

//        towerRadiusRenderer.color = canPlace
//            ? new Color(0.2f, 1f, 0.2f, towerRadiusRenderer.color.a)
//            : new Color(1f, 0.2f, 0.2f, towerRadiusRenderer.color.a);
//        if (Mouse.current.leftButton.wasPressedThisFrame && canPlace)
//        {
//            GameObject placed = Instantiate(towerPrefab, mouseWorld, Quaternion.identity);

//            SpriteRenderer placedTowerRenderer = placed.GetComponent<SpriteRenderer>();

//            foreach (SpriteRenderer childRenderer in placed.GetComponentsInChildren<SpriteRenderer>())
//            {
//                if (childRenderer.gameObject != placed)
//                {
//                    SetAlpha(childRenderer, 0f);
//                }
//            }

//            SetAlpha(placedTowerRenderer, 1f);
//        }
//    }
//    void CheckCanPlace(Vector3 position)
//    {
//        ContactFilter2D filter = new ContactFilter2D();
//        filter.SetLayerMask(towerRadiusLayer);
//        filter.useTriggers = true;

//        List<Collider2D> hits = new List<Collider2D>();
//        previewCollider.Overlap(filter, hits);

//        hits.Remove(previewCollider);

//        canPlace = hits.Count == 0;
//    }

//    void SetAlpha(SpriteRenderer rend, float alpha)
//    {
//        if (rend == null) return;
//        Color c = rend.color;
//        c.a = alpha;
//        rend.color = c;
//    }

//    void OnDestroy()
//    {
//        Cursor.visible = true;
//    }
//}