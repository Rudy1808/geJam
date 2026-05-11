using UnityEngine;

public class TowerRadiusDisplay : MonoBehaviour
{
    private SpriteRenderer towerRadiusRenderer;
    private SpriteRenderer attackRadiusRenderer;
    private bool isShowing = false;

    void Start()
    {
        towerRadiusRenderer = transform.Find("towerRadius").GetComponent<SpriteRenderer>();
        attackRadiusRenderer = transform.Find("attackRadius").GetComponent<SpriteRenderer>();
    }

    public void ToggleRadii()
    {
        isShowing = !isShowing;

        SetAlpha(towerRadiusRenderer, isShowing ? 0.7f : 0f);
        SetAlpha(attackRadiusRenderer, isShowing ? 0.25f : 0f);
    }

    private void SetAlpha(SpriteRenderer rend, float alpha)
    {
        if (rend == null) return;
        Color c = rend.color;
        c.a = alpha;
        rend.color = c;
    }
}