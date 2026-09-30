using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
    }
}
