using UnityEngine;

public class CancelOutHorizontalRootMotion : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 newPosition = transform.localPosition;

        newPosition.x = 0;
        newPosition.z = 0;

        transform.localPosition = newPosition;
    }
}
