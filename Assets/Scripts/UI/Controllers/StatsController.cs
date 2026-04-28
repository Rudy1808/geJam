using UnityEngine;

public class StatsController : MonoBehaviour
{
    StatsModel model;
    StatsView view;

    private void Awake()
    {
        view = GetComponent<StatsView>();

        model = new StatsModel();
    }

    public void SetHp(int hp)
    {
        model.hp = hp;
        view.refresh(model);
    }

    public void SetStats(int money)
    {
        model.money = money;
        view.refresh(model);
    }
}
