using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private void Update()
    {
        Quaternion.LookRotation(-Camera.main.transform.position.normalized);
    }
}