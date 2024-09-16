using UnityEngine;

public class HorizontalFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private Vector3 _lastTargetPosition;

    private void Awake()
    {
        _lastTargetPosition = transform.position;
    }

    private void Update()
    {
        Vector3 targetPositionDelta = target.position - _lastTargetPosition;
        transform.position = new Vector3(transform.position.x + targetPositionDelta.x, transform.position.y, transform.position.z + targetPositionDelta.z);
        _lastTargetPosition = target.position;
    }
}
