using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Quaternion cameraVec;

    void Update()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.position.normalized);
    }
}