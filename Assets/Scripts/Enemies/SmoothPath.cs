using UnityEngine;

public class SmoothPath : MonoBehaviour
{
    public Transform[] wayPoints;
    public float speed = 5f;
    int i = 0;
    void Start()
    {
       
    }

    void Update()
    {
        if (i >= wayPoints.Length)
            return;
        if (wayPoints.Length == 0) return;
        Transform target = wayPoints[i];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            i++;
        }

    }
}
