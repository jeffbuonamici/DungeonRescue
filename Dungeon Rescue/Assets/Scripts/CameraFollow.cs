using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform mTarget;

    float followSpeed = 5f;
    float stepOverThreshold = 0.05f;

    void Update ()
    {
        if(mTarget != null)
        {
            Vector3 targetPosition = new Vector3(mTarget.transform.position.x, transform.position.y, transform.position.z);
            Vector3 direction = targetPosition - transform.position;

            if(direction.magnitude > stepOverThreshold)
            {
                transform.Translate (direction.normalized * followSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = targetPosition;
            }
        }
    }
}
