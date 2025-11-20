using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform FollowTarget, LookTarget;
    public float FollowSpeed = 10f;

    
    void LateUpdate()
    {
        Vector3 targetPosition = FollowTarget.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, FollowSpeed * Time.deltaTime);

        transform.LookAt(LookTarget);
    }
}
