using UnityEngine;

public class TowerPreview : MonoBehaviour
{
    private float towerRadiusValue = 4f;
    private float towerRadiusOffsetY = -1f;
    private float attackRadiusOffsetY = -1f;

    private Transform towerRadiusTransform;
    private Transform attackRadiusTransform;

    private SpriteRenderer mainRenderer;
    private SpriteRenderer towerRadiusRenderer;
    private SpriteRenderer attackRadiusRenderer;

    public CircleCollider2D PreviewCollider { get; private set; }

    public void Initialize(float attackRadiusValue)
    {
        towerRadiusTransform = transform.Find("towerRadius");
        attackRadiusTransform = transform.Find("attackRadius");

        mainRenderer = GetComponent<SpriteRenderer>();
        towerRadiusRenderer = towerRadiusTransform.GetComponent<SpriteRenderer>();
        attackRadiusRenderer = attackRadiusTransform.GetComponent<SpriteRenderer>();
        PreviewCollider = towerRadiusTransform.GetComponentInChildren<CircleCollider2D>();

        transform.localScale = Vector3.one;
        towerRadiusTransform.localScale = new Vector3(towerRadiusValue, towerRadiusValue, 1f);
        attackRadiusTransform.localScale = new Vector3(attackRadiusValue, attackRadiusValue, 1f);

        towerRadiusTransform.localPosition = new Vector3(0f, towerRadiusOffsetY, 0f);
        attackRadiusTransform.localPosition = new Vector3(0f, attackRadiusOffsetY, 0f);
    }

    public void ShowAsPreview()
    {
        SetAlpha(mainRenderer, 0.8f);
        SetAlpha(towerRadiusRenderer, 0.7f);
        SetAlpha(attackRadiusRenderer, 0.25f);
    }

    public void ShowAsPlaced()
    {
        SetAlpha(mainRenderer, 1f);
        SetAlpha(towerRadiusRenderer, 0f);
        SetAlpha(attackRadiusRenderer, 0f);
    }

    public void SetPlacementColor(bool canPlace)
    {
        towerRadiusRenderer.color = canPlace
            ? new Color(0.2f, 1f, 0.2f, towerRadiusRenderer.color.a)
            : new Color(1f, 0.2f, 0.2f, towerRadiusRenderer.color.a);
    }

    private void SetAlpha(SpriteRenderer rend, float alpha)
    {
        if (rend == null) return;
        Color c = rend.color;
        c.a = alpha;
        rend.color = c;
    }
}