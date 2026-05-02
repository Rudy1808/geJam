using UnityEngine;

public class SmoothPath : MonoBehaviour
{
    [HideInInspector] public Path path;

    Enemy enemy;
    public int i = 0;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    void Start()
    {
        Transform[] all = path.GetComponentsInChildren<Transform>();
        path.wayPoints = new Transform[all.Length - 1];
        for (int j = 0; j < path.wayPoints.Length; j++)
            path.wayPoints[j] = all[j + 1];
    }

    void Update()
    {
        if (i >= path.wayPoints.Length){
            return;
        }
        if (path.wayPoints.Length == 0) return;
        Transform target = path.wayPoints[i];
        transform.position = Vector3.MoveTowards(transform.position, target.position, enemy.speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            i++;
        }
    }
   
}
