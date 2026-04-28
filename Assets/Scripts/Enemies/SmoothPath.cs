using UnityEngine;

public class SmoothPath : MonoBehaviour
{
    [HideInInspector] public Path path;
    [HideInInspector] public float speed;
    int i = 0;
    void Start()
    {
       
    }

    void Update()
    {
        if (i >= path.wayPoints.Length)
            return;
        if (path.wayPoints.Length == 0) return;
        Transform target = path.wayPoints[i];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            i++;
        }
    }
    private void OnEnable()
    {
        speed = GetComponent<Enemy>().speed;
    }
}
