//using UnityEngine;

//public class StatsController : MonoBehaviour
//{
//    public static StatsController instance;
//    StatsModel model;
//    StatsView view;

//    private void Awake()
//    {
//        instance = this; 
//        view = GetComponent<StatsView>();
//        model = new StatsModel();
//    }

//    public void SetHp(int hp)
//    {
//        model.hp = hp;
//        view.refresh(model);
//    }

//    public static void SetMoney(int money)
//    {
//        Debug.Log(instance.view == null? "view to null" : "view git");
//        Debug.Log(instance == null? "instance do dupy": "jednak nie");
//        instance.model.money = money;
//        instance.view.refresh(instance.model);
//    }
//}