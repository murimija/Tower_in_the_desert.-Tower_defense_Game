using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.position.normalized);
    }
}