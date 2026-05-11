using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;

    private Camera mainCam;
    private GameObject previewInstance;
    private TowerPreview preview;

    private LayerMask towerRadiusLayer;
    private bool canPlace = true;
    private bool isPlacing = false;

    private int cost;

    void Start()
    {
        mainCam = Camera.main;
        cost = towerPrefab.GetComponent<Tower>().cost;

        towerRadiusLayer = LayerMask.GetMask("TowerRadius");

        // Tworzymy instancjê prefaba jako podgl¹d
        previewInstance = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity);
        preview = previewInstance.AddComponent<TowerPreview>();

        float attackRange = towerPrefab.GetComponent<TowerAttack>().range * 2;
        preview.Initialize(attackRange);

        previewInstance.SetActive(false);

        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        TowerPlacerManager.Instance.RequestActivation(this);
    }

    public void StartPlacing()
    {
        isPlacing = true;
        previewInstance.SetActive(true);
        Cursor.visible = false;
        preview.ShowAsPreview();
    }

    public void StopPlacing()
    {
        isPlacing = false;
        previewInstance.SetActive(false);
        Cursor.visible = true;
        TowerPlacerManager.Instance.NotifyStopped(this);
    }

    void Update()
    {
        if (!isPlacing) return;

        // Pozycja podgl¹du za kursorem
        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;
        previewInstance.transform.position = mouseWorld;

        // Sprawdzenie czy mo¿na postawiæ
        canPlace = CheckCanPlace();
        preview.SetPlacementColor(canPlace);

        // Anulowanie
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            StopPlacing();
            return;
        }

        // Postawienie wie¿y
        if (Mouse.current.leftButton.wasPressedThisFrame && canPlace)
        {
            if (Cave.Money >= cost)
                Cave.Money -= cost;
            else
                return; // brak kasy – nie stawiaj


            GameObject placed = Instantiate(towerPrefab, mouseWorld, Quaternion.identity);
            placed.GetComponent<TowerPreview>()?.ShowAsPlaced();
            placed.GetComponent<TowerAttack>().enabled = true;
            // jeœli prefab nie ma TowerPreview, ukryj rêcznie:
            HidePlacedRadii(placed);

            StopPlacing();
        }
    }

    bool CheckCanPlace()
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(towerRadiusLayer);
        filter.useTriggers = true;

        List<Collider2D> hits = new List<Collider2D>();
        preview.PreviewCollider.Overlap(filter, hits);
        hits.Remove(preview.PreviewCollider);

        return hits.Count == 0;
    }

    // Prefab nie ma TowerPreview – chowamy rêcznie radiiusy po postawieniu
    void HidePlacedRadii(GameObject placed)
    {
        void SetAlpha(Transform t, float a)
        {
            if (t == null) return;
            var sr = t.GetComponent<SpriteRenderer>();
            if (sr == null) return;
            var c = sr.color; c.a = a; sr.color = c;
        }

        SetAlpha(placed.transform.Find("towerRadius"), 0f);
        SetAlpha(placed.transform.Find("attackRadius"), 0f);
    }
}